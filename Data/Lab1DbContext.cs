using Lab1.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Data
{
    public class Lab1DbContext : DbContext
    {
        public Lab1DbContext(DbContextOptions<Lab1DbContext> options) : base(options)
        {
        }

        public DbSet<Table> Tables { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        // Konfigurera mockdata
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unikhetsbegränsning för TableNumber
            modelBuilder.Entity<Table>()
                .HasIndex(t => t.TableNumber)
                .IsUnique();

            // Mockdata för menyalternativ
            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem
                {
                    MenuItemId = 1,
                    Name = "Pizza Margherita",
                    Price = 99,
                    IsAvailable = true,
                    Description = "En klassisk pizza med tomat, basilika och mozzarella."
                },
                new MenuItem
                {
                    MenuItemId = 2,
                    Name = "Spaghetti Bolognese",
                    Price = 129,
                    IsAvailable = true,
                    Description = "Pasta med en köttfärssås baserad på nötkött och tomater."
                },
                new MenuItem
                {
                    MenuItemId = 3,
                    Name = "Caesarsallad",
                    Price = 89,
                    IsAvailable = true,
                    Description = "Fräsch sallad med kyckling, krutonger och parmesan."
                },
                new MenuItem
                {
                    MenuItemId = 4,
                    Name = "Hamburgare",
                    Price = 139,
                    IsAvailable = false,
                    Description = "Saftig hamburgare serverad med pommes frites."
                },
                new MenuItem
                {
                    MenuItemId = 5,
                    Name = "Grillad Kyckling",
                    Price = 149,
                    IsAvailable = true,
                    Description = "Kycklingfilé grillad till perfektion med örter."
                }
            );

            // Mockdata för bord
            modelBuilder.Entity<Table>().HasData(
                new Table
                {
                    TableId = 1,
                    TableNumber = 1,
                    Seats = 4
                },
                new Table
                {
                    TableId = 2,
                    TableNumber = 2,
                    Seats = 2
                },
                new Table
                {
                    TableId = 3,
                    TableNumber = 3,
                    Seats = 3
                }
            );

            // Mockdata för kunder
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    Name = "Anna Svensson",
                    Email = "anna.svensson@example.com",
                    Phone = "0701234567"
                },
                new Customer
                {
                    CustomerId = 2,
                    Name = "Erik Johansson",
                    Email = "erik.johansson@example.com",
                    Phone = "0707654321"
                },
                new Customer
                {
                    CustomerId = 3,
                    Name = "Maria Lind",
                    Email = "maria.lind@example.com",
                    Phone = "0709988776"
                }
            );

            // Mockdata för bokningar
            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    ReservationId = 1,
                    CustomerId = 1,
                    TableId = 1,
                    ReservationDate = DateTime.Now.AddDays(1),
                    NumberOfGuests = 4
                },
                new Reservation
                {
                    ReservationId = 2,
                    CustomerId = 2,
                    TableId = 2,
                    ReservationDate = DateTime.Now.AddDays(2),
                    NumberOfGuests = 2
                },
                new Reservation
                {
                    ReservationId = 3,
                    CustomerId = 3,
                    TableId = 3,
                    ReservationDate = DateTime.Now.AddDays(3),
                    NumberOfGuests = 3
                }
            );
        }
    }
}
