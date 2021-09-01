using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BulkyBook.DomainClass.Book
{
    public class Book
    {
        #region Properties

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        [Range(1,1000)]
        public int ListPrice { get; set; }

        [Required]
        [Range(1,1000)]
        public int Price { get; set; }

        [Required]
        [Range(1,1000)]
        public int Price50 { get; set; }

        [Required]
        [Range(1,1000)]
        public int Price100 { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Required]
        public int CoverTypeId { get; set; }
        [ForeignKey("CoverTypeId")]
        public CoverType CoverType { get; set; }

        #endregion
    }
}
