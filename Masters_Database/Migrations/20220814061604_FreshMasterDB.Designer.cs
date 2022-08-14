﻿// <auto-generated />
using System;
using Masters_Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Masters_Database.Migrations
{
    [DbContext(typeof(Master_DbContext))]
    [Migration("20220814061604_FreshMasterDB")]
    partial class FreshMasterDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Masters_Database.MasterCity", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CityId"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSystemGenrated")
                        .HasColumnType("bit");

                    b.Property<string>("ModifedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int?>("NumericCode")
                        .HasColumnType("int");

                    b.Property<int?>("StateId")
                        .HasColumnType("int");

                    b.HasKey("CityId");

                    b.HasIndex("StateId");

                    b.ToTable("MasterCity");
                });

            modelBuilder.Entity("Masters_Database.MasterCountry", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryId"), 1L, 1);

                    b.Property<string>("CodeThreeLtr")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("CodeTwoLtr")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<int>("ContinentId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSystemGenrated")
                        .HasColumnType("bit");

                    b.Property<string>("ModifedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int?>("NumericCode")
                        .HasColumnType("int");

                    b.Property<int?>("TimeZoneId")
                        .HasColumnType("int");

                    b.HasKey("CountryId");

                    b.HasIndex("ContinentId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("TimeZoneId");

                    b.ToTable("MasterCountry");
                });

            modelBuilder.Entity("Masters_Database.MasterCurrency", b =>
                {
                    b.Property<int>("CurrencyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CurrencyId"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSystemGenrated")
                        .HasColumnType("bit");

                    b.Property<string>("ModifedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Symbole")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("CurrencyId");

                    b.ToTable("MasterCurrency");
                });

            modelBuilder.Entity("Masters_Database.MastersContinent", b =>
                {
                    b.Property<int>("ContinentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ContinentId"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSystemGenrated")
                        .HasColumnType("bit");

                    b.Property<string>("ModifedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ContinentId");

                    b.ToTable("MastersContinent");
                });

            modelBuilder.Entity("Masters_Database.MasterState", b =>
                {
                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSystemGenrated")
                        .HasColumnType("bit");

                    b.Property<string>("ModifedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int?>("NumericCode")
                        .HasColumnType("int");

                    b.HasKey("StateId");

                    b.HasIndex("CountryId");

                    b.ToTable("MasterState");
                });

            modelBuilder.Entity("Masters_Database.MasterTimeZone", b =>
                {
                    b.Property<int>("TimeZoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TimeZoneId"), 1L, 1);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("DeletedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSystemGenrated")
                        .HasColumnType("bit");

                    b.Property<string>("ModifedBy")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("OffSet")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("OffSetName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TimeZoneId");

                    b.ToTable("MasterTimeZone");
                });

            modelBuilder.Entity("Masters_Database.MasterCity", b =>
                {
                    b.HasOne("Masters_Database.MasterState", "State")
                        .WithMany("MasterCity")
                        .HasForeignKey("StateId")
                        .HasConstraintName("FK_MasterCity_MasterState");

                    b.Navigation("State");
                });

            modelBuilder.Entity("Masters_Database.MasterCountry", b =>
                {
                    b.HasOne("Masters_Database.MastersContinent", "Continent")
                        .WithMany("MasterCountry")
                        .HasForeignKey("ContinentId")
                        .IsRequired()
                        .HasConstraintName("FK_MasterCountry_MastersContinent");

                    b.HasOne("Masters_Database.MasterCurrency", "Currency")
                        .WithMany("MasterCountry")
                        .HasForeignKey("CurrencyId")
                        .HasConstraintName("FK_MasterCountry_MasterCurrency");

                    b.HasOne("Masters_Database.MasterTimeZone", "TimeZone")
                        .WithMany("MasterCountry")
                        .HasForeignKey("TimeZoneId")
                        .HasConstraintName("FK_MasterCountry_MasterTimeZone");

                    b.Navigation("Continent");

                    b.Navigation("Currency");

                    b.Navigation("TimeZone");
                });

            modelBuilder.Entity("Masters_Database.MasterState", b =>
                {
                    b.HasOne("Masters_Database.MasterCountry", "Country")
                        .WithMany("MasterState")
                        .HasForeignKey("CountryId")
                        .IsRequired()
                        .HasConstraintName("FK_MasterState_MasterCountry");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Masters_Database.MasterCountry", b =>
                {
                    b.Navigation("MasterState");
                });

            modelBuilder.Entity("Masters_Database.MasterCurrency", b =>
                {
                    b.Navigation("MasterCountry");
                });

            modelBuilder.Entity("Masters_Database.MastersContinent", b =>
                {
                    b.Navigation("MasterCountry");
                });

            modelBuilder.Entity("Masters_Database.MasterState", b =>
                {
                    b.Navigation("MasterCity");
                });

            modelBuilder.Entity("Masters_Database.MasterTimeZone", b =>
                {
                    b.Navigation("MasterCountry");
                });
#pragma warning restore 612, 618
        }
    }
}
