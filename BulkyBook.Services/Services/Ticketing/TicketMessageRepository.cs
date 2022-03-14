using BulkyBook.DataAccess.Data;
using BulkyBook.DomainClass.Book;
using BulkyBook.DomainClass.Ticketing;
using BulkyBook.Services.Repositories;
using BulkyBook.Services.Repositories.Ticketing;
using BulkyBook.ViewModel.Ticket;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Services.Ticketing
{
    public class TicketMessageRepository : ITicketMessageRepository
    {
        #region Ctor

        private readonly ApplicationDbContext _context;
        public TicketMessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        public async Task Insert(TicketMessage ticketMessage)
        {
            await _context.TicketMessages.AddAsync(ticketMessage);
        }

        public void Update(TicketMessage ticketMessage)
        {
            _context.TicketMessages.Update(ticketMessage);
        }

        public async Task Delete(TicketMessage ticketMessage)
        {
            _context.TicketMessages.Remove(ticketMessage);
        }

        public async Task Delete(int ticketMessageId)
        {
            TicketMessage ticketMessage = await _context.TicketMessages.FindAsync(ticketMessageId);
            await Delete(ticketMessage);
        }

        public async Task<List<TicketMessage>> GetTicketMessages()
        {
            return await _context.TicketMessages.ToListAsync();
        }

        public async Task<TicketMessage> GetTicketMessageById(int ticketMessageId)
        {
            return await _context.TicketMessages.FindAsync(ticketMessageId);
        }

        public async Task<List<TicketMessageWithRoleVM>> GetTicketMessagesByTicketId(int ticketId)
        {
            List<TicketMessageWithRoleVM> ticketMessageWithRoleList = new List<TicketMessageWithRoleVM>();
            List<TicketMessage> ticketMessageList = await _context.TicketMessages.Include(t => t.ApplicationUser).Include(t => t.Ticket).Where(t => t.TicketId == ticketId).OrderBy(t => t.Id).ToListAsync();

            foreach (var ticketMessage in ticketMessageList)
            {
                var userRole = _context.UserRoles.FirstOrDefault(r => r.UserId == ticketMessage.ApplicationUserId);
                var roleName = _context.Roles.FirstOrDefault(r => r.Id == userRole.RoleId).Name;

                TicketMessageWithRoleVM ticketMessageWithRoleVM = new TicketMessageWithRoleVM()
                {
                    TicketMessage = ticketMessage,
                    RoleName = roleName
                };

                ticketMessageWithRoleList.Add(ticketMessageWithRoleVM);
            }

            return ticketMessageWithRoleList;
        }

        #endregion
    }
}
