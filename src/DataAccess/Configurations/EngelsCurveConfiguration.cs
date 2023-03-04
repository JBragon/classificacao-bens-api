using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Business;

namespace DataAccess.Configurations
{
    internal class EngelsCurveConfiguration : IEntityTypeConfiguration<EngelsCurve>
    {
        public void Configure(EntityTypeBuilder<EngelsCurve> builder)
        {
            // Table & Column Mappings
            builder.ToTable("EngelsCurve");

            // Primary Key
            builder.HasKey(t => t.Id);

            builder.Property(t => t.ProductId)
                .IsRequired();

            builder.Property(t => t.Income)
                .IsRequired();

            builder.Property(t => t.Amount)
                .IsRequired();

            builder.Property(t => t.AngularCoefficient)
                .IsRequired();

            builder.Property(t => t.Classification);

            builder.Property(t => t.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();

            builder.Property(t => t.UpdatedAt)
                .HasColumnName("UpdatedAt")
                .IsRequired();
        }
    }
}
