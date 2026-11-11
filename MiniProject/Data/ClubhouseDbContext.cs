using Microsoft.EntityFrameworkCore;
using ClubhouseAPI.Models;

namespace ClubhouseAPI.Data
{
    public class ClubhouseDbContext : DbContext
    {
        public ClubhouseDbContext(DbContextOptions<ClubhouseDbContext> options) : base(options)
        {
        }

        // These DbSets represent the tables in your database
        public DbSet<Member> Members { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<VisitorPass> VisitorPasses { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}