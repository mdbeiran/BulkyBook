using BulkyBook.Business.StaticTools;
using BulkyBook.DomainClass.Order;
using BulkyBook.DomainClass.User;
using BulkyBook.Services.Context;
using BulkyBook.Utility.Convertor;
using BulkyBook.Utility.Order;
using BulkyBook.ViewModel.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BulkyBook.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        #region Ctor

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;

        public CartController(IUnitOfWork unitOfWork,
            UserManager<IdentityUser> userManager,
            IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        #endregion

        [BindProperty]
        public ShoppingCartVM ShoppingCartVM { get; set; }

        #region Shopping Cart

        #region Index

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            #region Get id logged in user

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            #endregion                

            ShoppingCartVM = new ShoppingCartVM()
            {
                CartList = await _unitOfWork.ShoppingCartRepository.
                    GetCartsByUserId(claim.Value),
                OrderHeader = new OrderHeader()
            };

            ShoppingCartVM.OrderHeader.OrderTotal = 0;
            ShoppingCartVM.OrderHeader.ApplicationUser = await _unitOfWork.
                ApplicationUserRepository.GetUserById(claim.Value);

            foreach (ShoppingCart cart in ShoppingCartVM.CartList)
            {
                cart.Price = BookPrice.GetPriceBasedOnQuantity(cart.Count,
                    cart.Book.Price, cart.Book.Price50, cart.Book.Price100);
                ShoppingCartVM.OrderHeader.OrderTotal += (
                    cart.Price * cart.Count);
                cart.Book.Description = RawHtml.ConvertToRawHtml(
                    cart.Book.Description);

                if (cart.Book.Description.Length > 100)
                {
                    cart.Book.Description = cart.Book.Description
                        .Substring(0, 99) + "...";
                }
            }

            return View(ShoppingCartVM);
        }


        [HttpPost]
        [ActionName("Index")]
        public async Task<IActionResult> IndexPost()
        {
            #region get id logged in user

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            #endregion

            ApplicationUser user = await _unitOfWork.ApplicationUserRepository.
                GetUserById(claim.Value);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Verification Email is empty!");
            }

            #region Confirm Email

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = user.Id, code = code },
                protocol: Request.Scheme);

            await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            #endregion

            ModelState.AddModelError(string.Empty, "Verification email sent! Please check your email");

            return RedirectToAction(nameof(Index), "Home", new { area = "Customer" });
        }

        #endregion

        #region Increement count itme to Cart

        [HttpGet]
        public async Task<IActionResult> Plus(int cartId)
        {
            ShoppingCart cart = await _unitOfWork.ShoppingCartRepository.
                GetShoppingCartById(cartId);

            cart.Count += 1;
            await _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Decreement count itme from Cart

        [HttpGet]
        public async Task<IActionResult> Minus(int cartId)
        {
            ShoppingCart cart = await _unitOfWork.ShoppingCartRepository.
                GetShoppingCartById(cartId);

            if (cart.Count == 1)
            {
                // Delete item from Cart
                await _unitOfWork.ShoppingCartRepository.Delete(cart);
                await _unitOfWork.Save();

                // Update session
                int count = await _unitOfWork.ShoppingCartRepository.
                    GetCountShoppingCartByUserId(cart.ApplicationUserId);
                HttpContext.Session.SetInt32(SD.ssShoppingCart, count);
            }
            else
            {
                cart.Count -= 1;
                await _unitOfWork.Save();
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete item from cart

        [HttpGet]
        public async Task<IActionResult> Delete(int cartId)
        {
            ShoppingCart cart = await _unitOfWork.ShoppingCartRepository.
                GetShoppingCartById(cartId);

            // Delete Item
            await _unitOfWork.ShoppingCartRepository.Delete(cart);
            await _unitOfWork.Save();

            // update session
            int count = await _unitOfWork.ShoppingCartRepository.
                GetCountShoppingCartByUserId(cart.ApplicationUserId);
            HttpContext.Session.SetInt32(SD.ssShoppingCart, count);

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #endregion

        #region Summary

        [HttpGet]
        public async Task<IActionResult> Summary()
        {
            #region Get id logged in user

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            #endregion

            ShoppingCartVM = new ShoppingCartVM()
            {
                OrderHeader = new OrderHeader(),
                CartList = await _unitOfWork.
                    ShoppingCartRepository.GetCartsByUserId(claim.Value)
            };

            foreach (ShoppingCart cart in ShoppingCartVM.CartList)
            {
                cart.Price = BookPrice.GetPriceBasedOnQuantity(cart.Count,
                    cart.Book.Price, cart.Book.Price50, cart.Book.Price100);
                ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            #region Populate orderHeader Properties

            ShoppingCartVM.OrderHeader.ApplicationUser = await _unitOfWork.
                ApplicationUserRepository.GetUserById(claim.Value);
            ShoppingCartVM.OrderHeader.FullName = ShoppingCartVM.OrderHeader.ApplicationUser.FullName;
            ShoppingCartVM.OrderHeader.PhoneNumber = ShoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            ShoppingCartVM.OrderHeader.StreetAddress = ShoppingCartVM.OrderHeader.ApplicationUser.StreetAddress;
            ShoppingCartVM.OrderHeader.City = ShoppingCartVM.OrderHeader.ApplicationUser.City;
            ShoppingCartVM.OrderHeader.State = ShoppingCartVM.OrderHeader.ApplicationUser.State;
            ShoppingCartVM.OrderHeader.PostalCode = ShoppingCartVM.OrderHeader.ApplicationUser.PostalCode;

            #endregion

            return View(ShoppingCartVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPost(string stripeToken)
        {
            //if (ModelState.IsValid)
            //{
            #region Get id logged in user

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            #endregion

            ShoppingCartVM.CartList = await _unitOfWork.ShoppingCartRepository.
                GetCartsByUserId(claim.Value);

            #region Populate orderHeader Properties

            ShoppingCartVM.OrderHeader.ApplicationUser = await _unitOfWork.
                ApplicationUserRepository.GetUserById(claim.Value);
            ShoppingCartVM.OrderHeader.ApplicationUserId = claim.Value;
            ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
            ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusPending;
            ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;

            #endregion

            #region Add orderHeader object to Db

            await _unitOfWork.OrderHeaderRepository.Insert(ShoppingCartVM.OrderHeader);
            await _unitOfWork.Save();

            #endregion

            foreach (ShoppingCart cart in ShoppingCartVM.CartList)
            {
                cart.Price = BookPrice.GetPriceBasedOnQuantity(cart.Count,
                    cart.Book.Price, cart.Book.Price50, cart.Book.Price100);

                OrderDetails orderDetails = new OrderDetails()
                {
                    OrderHeaderId = ShoppingCartVM.OrderHeader.Id,
                    BookId = cart.BookId,
                    Count = cart.Count,
                    Price = cart.Price
                };
                ShoppingCartVM.OrderHeader.OrderTotal += (
                    orderDetails.Price * orderDetails.Count);

                #region Add orderDetail object to Db

                await _unitOfWork.OrderDetailsRepository.Insert(orderDetails);

                #endregion
            }

            #region Remove User Shopping Cart And Update Session

            await _unitOfWork.ShoppingCartRepository.DeleteCarts(ShoppingCartVM.CartList);
            await _unitOfWork.Save();

            HttpContext.Session.SetInt32(SD.ssShoppingCart, 0);

            #endregion

            #region Stripe Portal

            if (stripeToken == null)
            {
                // order will be created for delayed payment for authorized company
                ShoppingCartVM.OrderHeader.PaymentDueDate = DateTime.Now.AddDays(30);
                ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
                ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusApproved;
            }
            else
            {
                // proccess the payment
                ChargeCreateOptions options = new ChargeCreateOptions()
                {
                    Amount = Convert.ToInt32(ShoppingCartVM.OrderHeader.OrderTotal * 100),
                    Currency = "usd",
                    Description = "Order Id : " + ShoppingCartVM.OrderHeader.Id,
                    Source = stripeToken
                };

                ChargeService service = new ChargeService();
                Charge charge = await service.CreateAsync(options);

                if (charge.BalanceTransactionId == null)
                {
                    ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusRejected;
                }
                else
                {
                    ShoppingCartVM.OrderHeader.TransactionId = charge.BalanceTransactionId;
                }
                if (charge.Status.ToLower() == "succeeded")
                {
                    ShoppingCartVM.OrderHeader.PaymentStatus = SD.PaymentStatusApproved;
                    ShoppingCartVM.OrderHeader.OrderStatus = SD.StatusApproved;
                    ShoppingCartVM.OrderHeader.PaymentDate = DateTime.Now;
                }
            }

            await _unitOfWork.Save();

            #endregion

            return RedirectToAction(nameof(OrderConfirmation), "Cart",
                    new { id = ShoppingCartVM.OrderHeader.Id });
            //}
            //else
            //{
            //    #region Get id logged in user

            //    var claimsIdentity = (ClaimsIdentity)User.Identity;
            //    var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            //    #endregion

            //    ShoppingCartVM.CartList = await _unitOfWork.ShoppingCartRepository.
            //        GetCartsByUserId(claim.Value);

            //    foreach (ShoppingCart cart in ShoppingCartVM.CartList)
            //    {
            //        cart.Price = BookPrice.GetPriceBasedOnQuantity(cart.Count,
            //            cart.Book.Price, cart.Book.Price50, cart.Book.Price100);
            //        ShoppingCartVM.OrderHeader.OrderTotal += (cart.Price * cart.Count);
            //    }

            //    ShoppingCartVM.OrderHeader.ApplicationUser = await _unitOfWork.
            //        ApplicationUserRepository.GetUserById(claim.Value);

            //    return View(ShoppingCartVM);
            //}

        }

        #endregion

        #region Order Confirmation

        [HttpGet]
        public async Task<IActionResult> OrderConfirmation(int id)
        {
            return View(id);
        }

        #endregion
    }
}
