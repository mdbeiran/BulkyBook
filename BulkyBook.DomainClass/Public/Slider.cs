using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BulkyBook.DomainClass.Public
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [MaxLength(200)]
        public string Title { get; set; }


        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        public string ImageName { get; set; }


        [Required]
        [Url]
        public string Url { get; set; }


        public int Visit { get; set; }
        public bool IsActive { get; set; }
    }
}
