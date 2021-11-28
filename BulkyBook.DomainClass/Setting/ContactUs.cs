using BulkyBook.DomainClass.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BulkyBook.DomainClass.Setting
{
    public class ContactUs
    {
        [Key]
        public int Id { get; set; }


        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }


        [MaxLength(20)]
        public string UserIp { get; set; }


        [Required]
        [MaxLength(256)]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [MaxLength(200)]
        public string FullName { get; set; }


        [Required]
        [MaxLength(300)]
        public string Subject { get; set; }


        [Required]
        [MaxLength(2000)]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }


        public string Answer { get; set; }


        [DisplayFormat(DataFormatString = "{0: yyyy/MM/dd}")]
        public DateTime CreateDate { get; set; }

        public bool IsRead { get; set; }

    }
}
