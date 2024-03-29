﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Business;

namespace DataAccess.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Table & Column Mappings
            builder.ToTable("Product");

            // Primary Key
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .HasColumnName("Name")
                .HasMaxLength(50)
                .IsRequired(); 
            
            builder.Property(t => t.Registration)
                .HasColumnName("Registration")
                .HasMaxLength(50)
                .IsRequired();    
            
            builder.Property(t => t.Observation)
                .HasColumnName("Observation")
                .HasMaxLength(2000)
                .IsRequired();            

            builder.Property(t => t.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();

            builder.Property(t => t.UpdatedAt)
                .HasColumnName("UpdatedAt")
                .IsRequired();

            builder.HasMany(x => x.EngelsCurves)
               .WithOne(y => y.Product)
               .HasForeignKey(x => x.ProductId);

            builder.HasIndex(t => t.Name)
                .IsUnique(true);
        }
    }
}
