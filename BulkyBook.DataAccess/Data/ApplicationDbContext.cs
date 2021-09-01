using BulkyBook.DomainClass.Book;
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

        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
