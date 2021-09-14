using BulkyBook.Services.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Web.ViewComponents
{
    public class ShowSliderViewComponent : ViewComponent
    {
        #region Ctor

        private readonly IUnitOfWork _unitOfWork;
        public ShowSliderViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
