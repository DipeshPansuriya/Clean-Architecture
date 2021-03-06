// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Application_Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace Application_Database.Configurations
{
    public partial class AdminOrganizationConfiguration : IEntityTypeConfiguration<AdminOrganization>
    {
        public void Configure(EntityTypeBuilder<AdminOrganization> entity)
        {
            entity.HasKey(e => e.OrgId);

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");

            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.Property(e => e.OrgEmail).HasMaxLength(500);

            entity.Property(e => e.OrgName).HasMaxLength(200);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<AdminOrganization> entity);
    }
}
