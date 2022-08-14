﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Masters_Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace Masters_Database.Configurations
{
    public partial class MastersContinentConfiguration : IEntityTypeConfiguration<MastersContinent>
    {
        public void Configure(EntityTypeBuilder<MastersContinent> entity)
        {
            entity.HasKey(e => e.ContinentId);

            entity.Property(e => e.Code).HasMaxLength(4);

            entity.Property(e => e.CreatedBy).HasMaxLength(250);

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.DeletedBy).HasMaxLength(250);

            entity.Property(e => e.DeletedDate).HasColumnType("datetime");

            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.ModifedBy).HasMaxLength(250);

            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.Property(e => e.Name).HasMaxLength(100);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<MastersContinent> entity);
    }
}