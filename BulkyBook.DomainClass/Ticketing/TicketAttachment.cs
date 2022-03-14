using BulkyBook.DomainClass.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BulkyBook.DomainClass.Ticketing
{
    public class TicketAttachment
    {
        [Key]
        public int Id { get; set; }


        public int TicketMessageId { get; set; }
        [ForeignKey("TicketMessageId")]
        public TicketMessage TicketMessage { get; set; }


        public string AttachmentUrl { get; set; }

    }
}
