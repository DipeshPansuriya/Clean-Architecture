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
    public partial class MasterCountryConfiguration : IEntityTypeConfiguration<MasterCountry>
    {
        public void Configure(EntityTypeBuilder<MasterCountry> entity)
        {
            entity.HasKey(e => e.CountryId);

            entity.Property(e => e.CodeThreeLtr).HasMaxLength(6);

            entity.Property(e => e.CodeTwoLtr).HasMaxLength(4);

            entity.Property(e => e.CreatedBy).HasMaxLength(250);

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.DeletedBy).HasMaxLength(250);

            entity.Property(e => e.DeletedDate).HasColumnType("datetime");

            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.Property(e => e.ModifedBy).HasMaxLength(250);

            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.Continent)
                .WithMany(p => p.MasterCountry)
                .HasForeignKey(d => d.ContinentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MasterCountry_MastersContinent");

            entity.HasOne(d => d.Currency)
                .WithMany(p => p.MasterCountry)
                .HasForeignKey(d => d.CurrencyId)
                .HasConstraintName("FK_MasterCountry_MasterCurrency");

            entity.HasOne(d => d.TimeZone)
                .WithMany(p => p.MasterCountry)
                .HasForeignKey(d => d.TimeZoneId)
                .HasConstraintName("FK_MasterCountry_MasterTimeZone");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<MasterCountry> entity);
    }
}
