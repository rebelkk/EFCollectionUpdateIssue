using EFCollectionUpdateIssue.Console.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCollectionUpdateIssue.Console.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new OrderConfiguration());
        builder.ApplyConfiguration(new OrderItemConfiguration());

        base.OnModelCreating(builder);
    }
}