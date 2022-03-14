using BulkyBook.DataAccess.Data;
using BulkyBook.DomainClass.Book;
using BulkyBook.DomainClass.Ticketing;
using BulkyBook.Services.Repositories;
using BulkyBook.Services.Repositories.Ticketing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Services.Ticketing
{
    public class TicketAttachmentRepository : ITicketAttachmentRepository
    {
        #region Ctor

        private readonly ApplicationDbContext _context;
        public TicketAttachmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task Insert(TicketAttachment ticketAttachment)
        {
            await _context.TicketAttachments.AddAsync(ticketAttachment);
        }

        public void Update(TicketAttachment ticketAttachment)
        {
            _context.TicketAttachments.Update(ticketAttachment);
        }

        public async Task Delete(TicketAttachment ticketAttachment)
        {
              _context.TicketAttachments.Remove(ticketAttachment);
        }

        public async Task Delete(int ticketAttachmentId)
        {
            TicketAttachment ticketAttachment = await _context.TicketAttachments.FindAsync(ticketAttachmentId);
            await Delete(ticketAttachment);
        }

        public async Task<IEnumerable<TicketAttachment>> GetTicketAttachments()
        {
            return  await _context.TicketAttachments.ToListAsync();
        }

        public async Task<TicketAttachment> GetTicketAttachmentById(int ticketAttachmentIdId)
        {
            return await _context.TicketAttachments.FindAsync(ticketAttachmentIdId);
        }

        

        #endregion
    }
}
