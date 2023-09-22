using EFCollectionUpdateIssue.Console.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EFCollectionUpdateIssue.Console.Data;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.OrderItems)
            .WithOne()
            .IsRequired();

    }
}