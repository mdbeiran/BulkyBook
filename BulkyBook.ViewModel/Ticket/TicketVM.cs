using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BulkyBook.ViewModel.Ticket
{
    public class TicketVM
    {
        public string FullName { get; set; }

        //public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Description { get; set; }

        public string AttachmetntUrl { get; set; }
    }
}
