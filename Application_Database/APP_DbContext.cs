﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Application_Database
{
    public partial class APP_DbContext : DbContext
    {
        public APP_DbContext()
        {
        }

        public APP_DbContext(DbContextOptions<APP_DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblApiResponse> TblApiResponse { get; set; }
        public virtual DbSet<TblNotification> TblNotification { get; set; }
        public virtual DbSet<TblRightmaster> TblRightmaster { get; set; }
        public virtual DbSet<TblRolemaster> TblRolemaster { get; set; }
        public virtual DbSet<TblUsermaster> TblUsermaster { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblApiResponse>(entity =>
            {
                entity.ToTable("tbl_API_Response");

                entity.Property(e => e.Apidate)
                    .HasColumnType("datetime")
                    .HasColumnName("apidate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Apistauts).HasColumnName("apistauts");

                entity.Property(e => e.Filename)
                    .HasMaxLength(100)
                    .HasColumnName("filename");

                entity.Property(e => e.Request).HasColumnName("request");

                entity.Property(e => e.Response).HasColumnName("response");

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            modelBuilder.Entity<TblNotification>(entity =>
            {
                entity.ToTable("tbl_Notification");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FailDetails).HasMaxLength(2000);

                entity.Property(e => e.MsgBody).IsRequired();

                entity.Property(e => e.MsgCc)
                    .HasMaxLength(500)
                    .HasColumnName("MsgCC");

                entity.Property(e => e.MsgFrom).HasMaxLength(200);

                entity.Property(e => e.MsgSatus)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MsgSubject)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.MsgTo).HasMaxLength(500);

                entity.Property(e => e.MsgType)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TblRightmaster>(entity =>
            {
                entity.HasKey(e => e.RightId);

                entity.ToTable("tbl_rightmaster");

                entity.HasIndex(e => e.RoleId, "IX_tbl_rightmaster_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.TblRightmaster)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleRights_RoleId");
            });

            modelBuilder.Entity<TblRolemaster>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("tbl_rolemaster");

                entity.Property(e => e.RoleNmae)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TblUsermaster>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("tbl_usermaster");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}