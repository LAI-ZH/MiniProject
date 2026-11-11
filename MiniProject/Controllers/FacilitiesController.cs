using ClubhouseAPI.Data;
using ClubhouseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClubhouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilitiesController : ControllerBase
    {
        private readonly ClubhouseDbContext _context;

        public FacilitiesController(ClubhouseDbContext context)
        {
            _context = context;
        }

        // GET: api/facilities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Facility>>> GetFacilities()
        {
            return await _context.Facilities.ToListAsync();
        }

        // POST: api/facilities (添加新设施)
        [HttpPost]
        public async Task<ActionResult<Facility>> CreateFacility(Facility facility)
        {
            _context.Facilities.Add(facility);
            await _context.SaveChangesAsync();
            return Ok(facility);
        }
    }
} 