using System.ComponentModel.DataAnnotations;

namespace ClubhouseAPI.Models
{
    public class VisitorPass
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid MemberId { get; set; }
        public Member? Member { get; set; }

        public string VisitorName { get; set; } = string.Empty;
        public string PassCode { get; set; } = string.Empty;
        public DateTime ValidDate { get; set; }
        public bool IsCheckedIn { get; set; } = false;
    }
}