using BulkyBook.DomainClass.Book;
using BulkyBook.DomainClass.Ticketing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Repositories.Ticketing
{
    public interface ITicketRepository
    {
        Task Insert(Ticket ticket);
        void Update(Ticket ticket);
        Task Delete(Ticket ticket);
        Task Delete(int ticketId);
        Task<IEnumerable<Ticket>> GetTickets();
        Task<Ticket> GetTicketById(int ticketId);
        Task<IEnumerable<Ticket>> GetTicketsByUserId(string UserId);
    }
}
