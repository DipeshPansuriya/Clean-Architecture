using Application_Domain.UserConfig;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Database.UserConfig
{
    public class role_config : IEntityTypeConfiguration<role_cls>
    {
        public void Configure(EntityTypeBuilder<role_cls> builder)
        {
            builder.ToTable("tbl_rolemaster");

            builder.Property(e => e.RoleId)
                .HasColumnName("RoleId")
                .HasColumnType("int")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.HasKey(e => e.RoleId);

            builder.Property(e => e.RoleNmae)
                .HasColumnName("RoleNmae")
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.IsActive)
                .HasColumnName("IsActive")
                .IsRequired();
        }
    }
}