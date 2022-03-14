using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using BulkyBook.DomainClass.Ticketing;

namespace BulkyBook.ViewModel.Ticket
{
    public class TicketMessageWithRoleVM
    {
        public TicketMessage TicketMessage { get; set; }

        public string RoleName { get; set; }
    }
}
