using BulkyBook.DomainClass.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BulkyBook.DomainClass.Order
{
    public class ShoppingCart
    {
        #region Ctor

        public ShoppingCart()
        {
            Count = 1;
        }

        #endregion

        #region Properties

        [Key]
        public int Id { get; set; }


        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }


        [Required]
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book.Book Book { get; set; }


        [Range(1,1000,ErrorMessage ="Please Enter a value between 1 to 1000")]
        public int Count { get; set; }


        [NotMapped]
        public double Price { get; set; }

        #endregion
    }
}
