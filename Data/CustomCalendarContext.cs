using Microsoft.EntityFrameworkCore;
using CustomCalendar.Entities;

namespace CustomCalendar.Data
{
    public class CustomCalendarDBContext : DbContext
    {
        public CustomCalendarDBContext(DbContextOptions<CustomCalendarDBContext> options)
            : base(options)
        {
        }
        public DbSet<Block> Block { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}