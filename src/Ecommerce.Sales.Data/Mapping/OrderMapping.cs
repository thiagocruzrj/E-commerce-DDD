using Ecommerce.Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Sales.Data.Mapping
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Code)
                .HasDefaultValueSql("NEXT VALUE FOR MySequence");

            // 1 : N => Order : OrderItem
            builder.HasMany(c => c.OrderItem)
                .WithOne(c => c.Order)
                .HasForeignKey(c => c.OrderId);

            builder.ToTable("Orders");
        }
    }
}
