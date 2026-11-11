using System.ComponentModel.DataAnnotations;

namespace ClubhouseAPI.Models
{
    public class Facility
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal HourlyRate { get; set; }
        public bool IsAvailable { get; set; } = true;
    }
}