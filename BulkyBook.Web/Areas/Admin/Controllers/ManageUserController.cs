using BulkyBook.Business.StaticTools;
using BulkyBook.DataAccess.Data;
using BulkyBook.DomainClass.User;
using BulkyBook.Services.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class ManageUserController : Controller
    {
        #region Ctor

        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public ManageUserController(IUnitOfWork unitOfWork,ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        #endregion

        #region Index

        public IActionResult Index()
        {
            return View(); ;
        }

        #endregion

        //#region Upsert

        //[HttpGet]
        //public async Task<IActionResult> Upsert(int? id)
        //{
        //    Category category = new Category();

        //    if (id == null)
        //    {
        //        //Create
        //        return View(category);
        //    }

        //    //Update
        //    category = await _unitOfWork.CategoryRepository.
        //                GetCategoryById(id.Value);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(category);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Upsert(Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (category.Id == 0)
        //        {
        //            //Create
        //            await _unitOfWork.CategoryRepository.Insert(category);
        //        }
        //        else
        //        {
        //            //Update
        //            _unitOfWork.CategoryRepository.Update(category);
        //        }
        //        await _unitOfWork.Save();

        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(category);
        //}

        //#endregion

        #region Api Call

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            IEnumerable<ApplicationUser> users = await _unitOfWork.
                ApplicationUserRepository.GetUsers();

            var userRoles = _context.UserRoles.ToList();
            var roles = _context.Roles.ToList();

            foreach (ApplicationUser user in users)
            {
                var roleId = userRoles.FirstOrDefault(
                    u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(
                    r => r.Id == roleId).Name;

                if (user.Company == null)
                {
                    user.Company = new Company()
                    {
                        Name = ""
                    };
                }
            }

            return Json(new { data = users });
        }


        [HttpPost]
        public async Task<IActionResult> LockUnlock([FromBody] string id)
        {
            ApplicationUser user = await _unitOfWork.ApplicationUserRepository.
                GetUserById(id);
                
            if (user == null)
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });
            }

            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
                // user is currently locked, we will unlock them
                user.LockoutEnd = DateTime.Now;
            }
            else
            {
                // user is currently Unlock, we will Lock them
                user.LockoutEnd = DateTime.Now.AddYears(1000);
            }

            await _unitOfWork.Save();

            return Json(new { success = true, message = "Operation Successful" });
        }

        #endregion
    }
}
