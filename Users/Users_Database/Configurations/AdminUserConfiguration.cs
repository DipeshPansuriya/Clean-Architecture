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
    public partial class AdminUserConfiguration : IEntityTypeConfiguration<AdminUser>
    {
        public void Configure(EntityTypeBuilder<AdminUser> entity)
        {
            entity.HasKey(e => e.UserId);

            entity.HasIndex(e => e.OrgProdId, "IX_AdminUser_OrgProdId");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.DeletedDate).HasColumnType("datetime");

            entity.Property(e => e.FirstName).HasMaxLength(50);

            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            entity.Property(e => e.LastName).HasMaxLength(50);

            entity.Property(e => e.LoginMail).HasMaxLength(250);

            entity.Property(e => e.LoginPassword).HasMaxLength(100);

            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.OrgProd)
                .WithMany(p => p.AdminUser)
                .HasForeignKey(d => d.OrgProdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AdminUser_AdminOrgProduct");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<AdminUser> entity);
    }
}
