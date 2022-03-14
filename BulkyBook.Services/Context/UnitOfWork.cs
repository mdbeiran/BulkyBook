using BulkyBook.DataAccess.Data;
using BulkyBook.Services.Repositories;
using BulkyBook.Services.Repositories.Public;
using BulkyBook.Services.Repositories.Setting;
using BulkyBook.Services.Repositories.Ticketing;
using BulkyBook.Services.Repositories.User;
using BulkyBook.Services.Services;
using BulkyBook.Services.Services.Public;
using BulkyBook.Services.Services.Setting;
using BulkyBook.Services.Services.Ticketing;
using BulkyBook.Services.Services.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Context
{
    public class UnitOfWork : IUnitOfWork
    {

        #region Ctor

        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            CategoryRepository = new CategoryRepository(context);
            CoverTypeRepository = new CoverTypeRepository(context);
            BookRepository = new BookRepository(context);
            CompanyRepository = new CompanyRepository(context);
            ApplicationUserRepository = new ApplicationUserRepository(context);
            SliderRepository = new SliderRepository(context);
            ShoppingCartRepository = new ShoppingCartRepository(context);
            OrderHeaderRepository = new OrderHeaderRepository(context);
            OrderDetailsRepository = new OrderDetailsRepository(context);
            SiteSettingRepository = new SiteSettingRepository(context);
            ContactUsRepository = new ContactUsRepository(context);
            TicketRepository = new TicketRepository(context);
            TicketMessageRepository = new TicketMessageRepository(context);
            TicketAttachmentRepository = new TicketAttachmentRepository(context);
        }

        #endregion

        #region Book

        public ICategoryRepository CategoryRepository { get; private set; }
        public ICoverTypeRepository CoverTypeRepository { get; private set; }
        public IBookRepository BookRepository { get; private set; }

        #endregion

        #region User

        public ICompanyRepository CompanyRepository { get; private set; }
        public IApplicationUserRepository ApplicationUserRepository { get; private set; }

        #endregion

        #region Public

        public ISliderRepository SliderRepository { get; private set; }

        #endregion

        #region Order

        public IShoppingCartRepository ShoppingCartRepository { get; private set; }
        public IOrderHeaderRepository OrderHeaderRepository { get; private set; }
        public IOrderDetailsRepository OrderDetailsRepository { get; private set; }

        #endregion

        #region Setting

        public ISiteSettingRepository SiteSettingRepository { get; private set; }

        public IContactUsRepository ContactUsRepository { get; private set; }

        #endregion

        #region Ticketing

        public ITicketRepository TicketRepository { get; private set; }
        public ITicketMessageRepository TicketMessageRepository { get; private set; }
        public ITicketAttachmentRepository TicketAttachmentRepository { get; private set; }

        #endregion

        #region Save

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
             _context.Dispose();
        }

        #endregion
    }
}
