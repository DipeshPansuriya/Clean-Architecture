﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Users_Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace Users_Database.Configurations
{
    public partial class AdminProductConfiguration : IEntityTypeConfiguration<AdminProduct>
    {
        public void Configure(EntityTypeBuilder<AdminProduct> entity)
        {
            entity.HasKey(e => e.ProductId);

            entity.Property(e => e.ProductId).ValueGeneratedNever();

            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.ProductName).HasMaxLength(100);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<AdminProduct> entity);
    }
}