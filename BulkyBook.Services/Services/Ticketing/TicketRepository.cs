using BulkyBook.DataAccess.Data;
using BulkyBook.DomainClass.Book;
using BulkyBook.DomainClass.Ticketing;
using BulkyBook.Services.Repositories;
using BulkyBook.Services.Repositories.Ticketing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Services.Ticketing
{
    public class TicketRepository : ITicketRepository
    {
        #region Ctor

        private readonly ApplicationDbContext _context;
        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task Insert(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
        }

        public void Update(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
        }

        public async Task Delete(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);
        }

        public async Task Delete(int ticketId)
        {
            Ticket ticket = await _context.Tickets.FindAsync(ticketId);
            await Delete(ticket);
        }

        public async Task<IEnumerable<Ticket>> GetTickets()
        {
            return await _context.Tickets.Include(t => t.ApplicationUser).OrderByDescending(t => t.Id).ToListAsync();
        }

        public async Task<Ticket> GetTicketById(int ticketId)
        {
            return await _context.Tickets.FindAsync(ticketId);
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByUserId(string UserId)
        {
            return await _context.Tickets.Include(t => t.ApplicationUser).Where(t => t.ApplicationUserId == UserId).ToListAsync();
        }



        #endregion
    }
}
