using BulkyBook.DomainClass.Book;
using BulkyBook.DomainClass.Order;
using BulkyBook.DomainClass.Public;
using BulkyBook.DomainClass.Setting;
using BulkyBook.DomainClass.Ticketing;
using BulkyBook.DomainClass.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        #region Ctor

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        #endregion

        #region User

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Company> Companies { get; set; }

        #endregion

        #region Book

        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }
        public DbSet<Book> Books { get; set; }

        #endregion

        #region Public

        public DbSet<Slider> Sliders { get; set; }

        #endregion

        #region Order

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        #endregion

        #region Setting

        public DbSet<SiteSetting> SiteSettings { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }

        #endregion

        #region Ticketing

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketMessage> TicketMessages { get; set; }
        public DbSet<TicketAttachment> TicketAttachments { get; set; }

        #endregion

    }
}
