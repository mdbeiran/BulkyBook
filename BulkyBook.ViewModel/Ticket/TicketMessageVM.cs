using BulkyBook.DomainClass.Ticketing;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.ViewModel.Ticket
{
    public class TicketMessageVM
    {
        public List<TicketMessageWithRoleVM> TicketMessageWithRoleVMs { get; set; }
        public TicketMessage TicketMessage { get; set; }
        public string Subject { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
