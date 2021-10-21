using BulkyBook.DomainClass.User;
using BulkyBook.Services.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BulkyBook.Web.ViewComponents
{
    public class UserNameViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IUnitOfWork _unitOfWork;
        public UserNameViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            #region Get id logged in user

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            #endregion

            ApplicationUser user = await _unitOfWork.ApplicationUserRepository.
                GetUserById(claim.Value);

            return View(user);
        }
    }
}
