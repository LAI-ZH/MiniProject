using ClubhouseAPI.Data;
using ClubhouseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClubhouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly ClubhouseDbContext _context;

        public InvoicesController(ClubhouseDbContext context)
        {
            _context = context;
        }

        // POST: api/invoices/pay (支付账单)
        [HttpPost("pay")]
        public async Task<ActionResult<Invoice>> ProcessPayment(Guid memberId, Guid? bookingId, decimal amount, string method)
        {
            var invoice = new Invoice
            {
                MemberId = memberId,
                BookingId = bookingId,
                Amount = amount,
                PaymentMethod = method,
                IsPaid = true,
                IssueDate = DateTime.UtcNow
            };

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return Ok(invoice);
        }
    }
}