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
    public partial class AdminBranchConfiguration : IEntityTypeConfiguration<AdminBranch>
    {
        public void Configure(EntityTypeBuilder<AdminBranch> entity)
        {
            entity.HasKey(e => e.BranchId);

            entity.HasIndex(e => e.CompId, "IX_AdminBranch_CompId");

            entity.HasIndex(e => e.OrgProdId, "IX_AdminBranch_OrgProdId");

            entity.Property(e => e.Address1).HasMaxLength(500);

            entity.Property(e => e.Address2).HasMaxLength(500);

            entity.Property(e => e.Address3).HasMaxLength(500);

            entity.Property(e => e.BranchName).HasMaxLength(200);

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.DeletedDate).HasColumnType("datetime");

            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            entity.Property(e => e.IsHo).HasDefaultValueSql("((0))");

            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Comp)
                .WithMany(p => p.AdminBranch)
                .HasForeignKey(d => d.CompId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdminBranch_AdminCompany");

            entity.HasOne(d => d.OrgProd)
                .WithMany(p => p.AdminBranch)
                .HasForeignKey(d => d.OrgProdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdminBranch_AdminOrgProduct");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<AdminBranch> entity);
    }
}
