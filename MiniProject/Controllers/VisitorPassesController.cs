using ClubhouseAPI.Data;
using ClubhouseAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClubhouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorPassesController : ControllerBase
    {
        private readonly ClubhouseDbContext _context;

        public VisitorPassesController(ClubhouseDbContext context)
        {
            _context = context;
        }

        // POST: api/visitorpasses/generate (生成通行证)
        [HttpPost("generate")]
        public async Task<ActionResult<VisitorPass>> GeneratePass(Guid memberId, string visitorName, DateTime validDate)
        {
            var member = await _context.Members.FindAsync(memberId);
            if (member == null || member.AvailableVisitorPasses <= 0)
            {
                return BadRequest("会员不存在或可用访客通行证不足。");
            }

            // 扣除通行证数量
            member.AvailableVisitorPasses--;

            var pass = new VisitorPass
            {
                MemberId = memberId,
                VisitorName = visitorName,
                ValidDate = validDate,
                PassCode = Guid.NewGuid().ToString().Substring(0, 8).ToUpper()
            };

            _context.VisitorPasses.Add(pass);
            await _context.SaveChangesAsync();

            return Ok(pass);
        }

        // POST: api/visitorpasses/checkin/{passCode} (前台核销入场)
        [HttpPost("checkin/{passCode}")]
        public async Task<IActionResult> CheckIn(string passCode)
        {
            var pass = await _context.VisitorPasses
                .FirstOrDefaultAsync(p => p.PassCode == passCode && !p.IsCheckedIn);

            if (pass == null) return NotFound("无效或已使用的通行证。");

            pass.IsCheckedIn = true;
            await _context.SaveChangesAsync();

            return Ok($"访客 {pass.VisitorName} 已成功核销入场。");
        }
    }
}