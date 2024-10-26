using Microsoft.EntityFrameworkCore;
using DataRepository.Entities;

namespace DataRepository.Data;

public class CustomCalendarDBContext : DbContext
{
    public CustomCalendarDBContext(DbContextOptions<CustomCalendarDBContext> options)
        : base(options)
    {
    }
    public DbSet<Block> Block { get; set; }
    public DbSet<Category> Category { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}