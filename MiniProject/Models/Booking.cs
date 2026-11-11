using System.ComponentModel.DataAnnotations;

namespace ClubhouseAPI.Models
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        // Foreign Keys
        public Guid MemberId { get; set; }
        public Member? Member { get; set; } // Navigation property

        public Guid FacilityId { get; set; }
        public Facility? Facility { get; set; } // Navigation property

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = "Confirmed";
        public decimal TotalCost { get; set; }
    }
}