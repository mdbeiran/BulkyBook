using BulkyBook.DomainClass.Book;
using BulkyBook.DomainClass.Ticketing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Services.Repositories.Ticketing
{
    public interface ITicketAttachmentRepository
    {
        Task Insert(TicketAttachment ticketAttachment);
        void Update(TicketAttachment ticketAttachment);
        Task Delete(TicketAttachment ticketAttachment);
        Task Delete(int ticketAttachmentId);
        Task<IEnumerable<TicketAttachment>> GetTicketAttachments();
        Task<TicketAttachment> GetTicketAttachmentById(int ticketAttachmentId);
    }
}
