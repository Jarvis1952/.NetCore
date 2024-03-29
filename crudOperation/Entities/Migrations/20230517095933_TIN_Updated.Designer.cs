﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Entities.Migrations
{
    [DbContext(typeof(PersonsDbContext))]
    [Migration("20230517095933_TIN_Updated")]
    partial class TIN_Updated
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Entities.Country", b =>
                {
                    b.Property<Guid>("CountryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CountryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryID");

                    b.ToTable("Countries", (string)null);

                    b.HasData(
                        new
                        {
                            CountryID = new Guid("d919d5b4-832e-44d9-80b0-1dd5a2d5e081"),
                            CountryName = "USA"
                        },
                        new
                        {
                            CountryID = new Guid("600e27c4-18e8-4487-9151-fe7020de943e"),
                            CountryName = "UK"
                        },
                        new
                        {
                            CountryID = new Guid("a1c80eee-69ef-49b1-a41c-9359e77dd111"),
                            CountryName = "Canada"
                        },
                        new
                        {
                            CountryID = new Guid("6b69260c-3516-46a4-b7a2-e6df45e303ac"),
                            CountryName = "India"
                        });
                });

            modelBuilder.Entity("Entities.Person", b =>
                {
                    b.Property<Guid>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<Guid?>("CountryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Gender")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("PersonName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool?>("ReceiveNewsLetters")
                        .HasColumnType("bit");

                    b.Property<string>("TIN")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("varchar(8)")
                        .HasDefaultValue("ABC123145")
                        .HasColumnName("TaxIdentificationNumber");

                    b.HasKey("PersonID");

                    b.ToTable("Persons", (string)null);

                    b.HasData(
                        new
                        {
                            PersonID = new Guid("6bd65612-c114-44c9-8e20-134513546ed7"),
                            Address = "625 Fairview Road",
                            CountryID = new Guid("6b69260c-3516-46a4-b7a2-e6df45e303ac"),
                            DateOfBirth = new DateTime(2013, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "jcotta1@oracle.com",
                            Gender = "Male",
                            PersonName = "Jedediah",
                            ReceiveNewsLetters = false
                        },
                        new
                        {
                            PersonID = new Guid("ddab3d49-f5b6-46cc-858d-3af5bd5a8827"),
                            Address = "6 Weeping Birch Lane",
                            CountryID = new Guid("d919d5b4-832e-44d9-80b0-1dd5a2d5e081"),
                            DateOfBirth = new DateTime(2014, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "einggall0@ed.gov",
                            Gender = "Female",
                            PersonName = "Ellary",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonID = new Guid("4545bf56-d313-4872-a506-3b081d8ab008"),
                            Address = "30703 Chinook Center",
                            CountryID = new Guid("600e27c4-18e8-4487-9151-fe7020de943e"),
                            DateOfBirth = new DateTime(2000, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "sousley2@china.com.cn",
                            Gender = "Female",
                            PersonName = "Sybilla",
                            ReceiveNewsLetters = true
                        },
                        new
                        {
                            PersonID = new Guid("42ff717e-324d-43ac-9ee3-d8785077dceb"),
                            Address = "28022 Blaine Alley",
                            CountryID = new Guid("a1c80eee-69ef-49b1-a41c-9359e77dd111"),
                            DateOfBirth = new DateTime(2011, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "walbrighton3@rakuten.co.jp",
                            Gender = "Male",
                            PersonName = "Wilbur",
                            ReceiveNewsLetters = false
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
