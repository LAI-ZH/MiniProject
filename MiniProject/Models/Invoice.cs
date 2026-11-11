using System.ComponentModel.DataAnnotations;

namespace ClubhouseAPI.Models
{
    public class Invoice
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid MemberId { get; set; }
        public Member? Member { get; set; }

        public Guid? BookingId { get; set; } // Optional: linked to a booking
        public Booking? Booking { get; set; }

        public decimal Amount { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public bool IsPaid { get; set; } = false;
        public string PaymentMethod { get; set; } = string.Empty;
    }
}