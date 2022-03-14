using BulkyBook.Services.Repositories;
using BulkyBook.Services.Repositories.Public;
using BulkyBook.Services.Repositories.Setting;
using BulkyBook.Services.Repositories.Ticketing;
using BulkyBook.Services.Repositories.User;
using BulkyBook.Services.Services.Ticketing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Context
{
    public interface IUnitOfWork : IDisposable
    {
        #region Book

        ICategoryRepository CategoryRepository { get; }
        ICoverTypeRepository CoverTypeRepository { get; }
        IBookRepository BookRepository { get; }

        #endregion

        #region User

        ICompanyRepository CompanyRepository { get; }
        IApplicationUserRepository ApplicationUserRepository { get; }

        #endregion

        #region Public

        ISliderRepository SliderRepository { get; }

        #endregion

        #region Order

        IShoppingCartRepository ShoppingCartRepository { get; }
        IOrderHeaderRepository OrderHeaderRepository { get; }
        IOrderDetailsRepository OrderDetailsRepository { get; }


        #endregion

        #region Setting

        public ISiteSettingRepository SiteSettingRepository { get; }
        public IContactUsRepository ContactUsRepository { get; }

        #endregion

        #region Ticketing

        public ITicketRepository TicketRepository { get; }
        public ITicketMessageRepository TicketMessageRepository { get; }
        public ITicketAttachmentRepository TicketAttachmentRepository { get; }


        #endregion

        #region Save

        Task Save();

        #endregion
    }
}
