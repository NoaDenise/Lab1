using Lab1.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Data
{
    public class Lab1DbContext : DbContext
    {
        public Lab1DbContext(DbContextOptions<Lab1DbContext> options) : base (options)
        {

        }

        public DbSet<Table> Tables { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
    }
}
