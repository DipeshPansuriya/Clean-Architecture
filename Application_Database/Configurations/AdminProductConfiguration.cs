﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Application_Database;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using System;

#nullable disable

namespace Application_Database.Configurations
{
    public partial class AdminProductConfiguration : IEntityTypeConfiguration<AdminProduct>
    {
        public void Configure(EntityTypeBuilder<AdminProduct> entity)
        {
            entity.HasKey(e => e.ProductId);

            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            entity.Property(e => e.ProductName).HasMaxLength(100);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<AdminProduct> entity);
    }
}
