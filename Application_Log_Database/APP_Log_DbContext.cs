﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Users_Database.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
#nullable enable

namespace Users_Database
{
    public partial class APP_Log_DbContext : DbContext
    {
        public APP_Log_DbContext()
        {
        }

        public APP_Log_DbContext(DbContextOptions<APP_Log_DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<APIRequest> APIRequest { get; set; } = null!;
        public virtual DbSet<APIResponse> APIResponse { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.APIRequestConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.APIResponseConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
