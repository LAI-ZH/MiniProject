using ClubhouseAPI.Data;
using ClubhouseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClubhouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly ClubhouseDbContext _context;

        public MembersController(ClubhouseDbContext context)
        {
            _context = context;
        }

        // GET: api/members
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            return await _context.Members.ToListAsync();
        }

        // POST: api/members (注册新会员)
        [HttpPost]
        public async Task<ActionResult<Member>> CreateMember(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMembers), new { id = member.Id }, member);
        }
    }
}