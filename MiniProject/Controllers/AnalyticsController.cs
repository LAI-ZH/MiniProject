using ClubhouseAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClubhouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly ClubhouseDbContext _context;

        public AnalyticsController(ClubhouseDbContext context)
        {
            _context = context;
        }

        // GET: api/analytics/revenue
        [HttpGet("revenue")]
        public async Task<IActionResult> GetTotalRevenue()
        {
            var revenue = await _context.Invoices
                .Where(i => i.IsPaid)
                .SumAsync(i => i.Amount);

            return Ok(new { TotalRevenue = revenue });
        }

        // GET: api/analytics/popular-facilities
        [HttpGet("popular-facilities")]
        public async Task<IActionResult> GetPopularFacilities()
        {
            var report = await _context.Bookings
                .Include(b => b.Facility)
                .Where(b => b.Status == "Confirmed")
                .GroupBy(b => b.Facility!.Name)
                .Select(g => new
                {
                    FacilityName = g.Key,
                    BookingCount = g.Count()
                })
                .OrderByDescending(x => x.BookingCount)
                .ToListAsync();

            return Ok(report);
        }
    }
}