using ClubhouseAPI.Data;
using ClubhouseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClubhouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ClubhouseDbContext _context;

        public BookingsController(ClubhouseDbContext context)
        {
            _context = context;
        }

        // GET: api/bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            return await _context.Bookings
                .Include(b => b.Member)
                .Include(b => b.Facility)
                .ToListAsync();
        }

        // POST: api/bookings (创建预订)
        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking(Booking booking)
        {
            // 1. 检查设施是否可用 (防止双重预订)
            var isDoubleBooked = await _context.Bookings.AnyAsync(b =>
                b.FacilityId == booking.FacilityId &&
                b.Status == "Confirmed" &&
                ((booking.StartTime >= b.StartTime && booking.StartTime < b.EndTime) ||
                 (booking.EndTime > b.StartTime && booking.EndTime <= b.EndTime)));

            if (isDoubleBooked)
            {
                return BadRequest("该时间段设施已被预订。");
            }

            // 2. 获取设施信息，计算总价
            var facility = await _context.Facilities.FindAsync(booking.FacilityId);
            if (facility == null) return NotFound("设施不存在。");

            var duration = (booking.EndTime - booking.StartTime).TotalHours;
            booking.TotalCost = (decimal)duration * facility.HourlyRate;
            booking.Status = "Confirmed";

            // 3. 保存到数据库
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Ok(booking);
        }
    }
}