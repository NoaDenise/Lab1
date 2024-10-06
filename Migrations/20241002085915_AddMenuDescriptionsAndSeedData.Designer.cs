﻿// <auto-generated />
using System;
using Lab1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Lab1.Migrations
{
    [DbContext(typeof(Lab1DbContext))]
    [Migration("20241002085915_AddMenuDescriptionsAndSeedData")]
    partial class AddMenuDescriptionsAndSeedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Lab1.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Lab1.Models.MenuItem", b =>
                {
                    b.Property<int>("MenuItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MenuItemId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("MenuItemId");

                    b.ToTable("MenuItems");

                    b.HasData(
                        new
                        {
                            MenuItemId = 1,
                            Description = "En klassisk pizza med tomat, basilika och mozzarella.",
                            IsAvailable = true,
                            Name = "Pizza Margherita",
                            Price = 99m
                        },
                        new
                        {
                            MenuItemId = 2,
                            Description = "Pasta med en köttfärssås baserad på nötkött och tomater.",
                            IsAvailable = true,
                            Name = "Spaghetti Bolognese",
                            Price = 129m
                        },
                        new
                        {
                            MenuItemId = 3,
                            Description = "Fräsch sallad med kyckling, krutonger och parmesan.",
                            IsAvailable = true,
                            Name = "Caesarsallad",
                            Price = 89m
                        },
                        new
                        {
                            MenuItemId = 4,
                            Description = "Saftig hamburgare serverad med pommes frites.",
                            IsAvailable = false,
                            Name = "Hamburgare",
                            Price = 139m
                        },
                        new
                        {
                            MenuItemId = 5,
                            Description = "Kycklingfilé grillad till perfektion med örter.",
                            IsAvailable = true,
                            Name = "Grillad Kyckling",
                            Price = 149m
                        });
                });

            modelBuilder.Entity("Lab1.Models.Reservation", b =>
                {
                    b.Property<int>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReservationId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfGuests")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReservationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TableId")
                        .HasColumnType("int");

                    b.HasKey("ReservationId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TableId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Lab1.Models.Table", b =>
                {
                    b.Property<int>("TableId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TableId"));

                    b.Property<int>("Seats")
                        .HasColumnType("int");

                    b.Property<int>("TableNumber")
                        .HasColumnType("int");

                    b.HasKey("TableId");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("Lab1.Models.Reservation", b =>
                {
                    b.HasOne("Lab1.Models.Customer", "Customer")
                        .WithMany("Reservations")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lab1.Models.Table", "Table")
                        .WithMany("Reservations")
                        .HasForeignKey("TableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Table");
                });

            modelBuilder.Entity("Lab1.Models.Customer", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Lab1.Models.Table", b =>
                {
                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
