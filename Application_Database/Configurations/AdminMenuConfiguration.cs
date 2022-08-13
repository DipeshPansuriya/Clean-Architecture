﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Application_Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

#nullable disable

namespace Application_Database.Configurations
{
    public partial class AdminMenuConfiguration : IEntityTypeConfiguration<AdminMenu>
    {
        public void Configure(EntityTypeBuilder<AdminMenu> entity)
        {
            entity.HasKey(e => e.MenuId);

            entity.Property(e => e.MenuId).ValueGeneratedNever();

            entity.Property(e => e.IsSysAdmin).HasDefaultValueSql("((0))");

            entity.Property(e => e.MenuName).HasMaxLength(250);

            entity.Property(e => e.MenuURL).HasMaxLength(250);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<AdminMenu> entity);
    }
}
