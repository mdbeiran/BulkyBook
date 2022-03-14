using BulkyBook.DomainClass.Book;
using BulkyBook.DomainClass.Ticketing;
using BulkyBook.ViewModel.Ticket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Repositories.Ticketing
{
    public interface ITicketMessageRepository
    {
        Task Insert(TicketMessage ticketMessage);
        void Update(TicketMessage ticketMessage);
        Task Delete(TicketMessage ticketMessage);
        Task Delete(int ticketMessageId);
        Task<List<TicketMessage>> GetTicketMessages();
        Task<TicketMessage> GetTicketMessageById(int ticketMessageId);
        Task<List<TicketMessageWithRoleVM>> GetTicketMessagesByTicketId(int ticketId);
    }
}
