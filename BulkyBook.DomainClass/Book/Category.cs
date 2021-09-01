using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BulkyBook.DomainClass.Book
{
    public class Category
    {
        #region Properties

        [Key]
        public int Id { get; set; }


        [Display(Name ="Category Name")]
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        #endregion
    }
}
