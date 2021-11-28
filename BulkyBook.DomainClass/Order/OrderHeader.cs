using BulkyBook.DomainClass.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BulkyBook.DomainClass.Order
{
    public class OrderHeader
    {
        #region Properties

        [Key]
        public int Id { get; set; }


        [Required]
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }


        [Required]
        public DateTime OrderDate { get; set; }


        [Required]
        public DateTime ShippingDate { get; set; }


        [Required]
        public double OrderTotal { get; set; }



        public string TrackingNumber { get; set; }
        public string Carrier { get; set; }
        public string OrderStatus { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime PaymentDueDate { get; set; }
        public string TransactionId { get; set; }


        [Required]
        public string FullName { get; set; }


        [Required]
        public string PhoneNumber { get; set; }


        [Required]
        public string StreetAddress { get; set; }


        [Required]
        public string City { get; set; }


        [Required]
        public string State { get; set; }


        [Required]
        public string PostalCode { get; set; }

        public bool IsViewByAdmin { get; set; }

        #endregion
    }
}
