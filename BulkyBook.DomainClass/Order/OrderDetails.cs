using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BulkyBook.DomainClass.Order
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public int OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        public OrderHeader OrderHeader { get; set; }


        [Required]
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public Book.Book Book { get; set; }


        public int Count { get; set; }
        public double Price { get; set; }
    }
}
