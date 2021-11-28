using BulkyBook.Services.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Web.ViewComponents
{
    public class MessagesViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IUnitOfWork _unitOfWork;
        public MessagesViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var contacts = await _unitOfWork.ContactUsRepository.
                GetNewestContactUs();

            return View(contacts);
        }
    }
}
