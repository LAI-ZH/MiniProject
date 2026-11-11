using System.ComponentModel.DataAnnotations;

namespace ClubhouseAPI.Models
{
    public class Member
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string MembershipTier { get; set; } = "Standard"; // Gold, Silver, etc.
        public decimal AccountBalance { get; set; } = 0;
        public int AvailableVisitorPasses { get; set; } = 5;
    }
}