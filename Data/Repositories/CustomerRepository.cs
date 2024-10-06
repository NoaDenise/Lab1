using Lab1.Data.Repositories.IRepositories;
using Lab1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab1.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly Lab1DbContext _context;

        public CustomerRepository(Lab1DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            try
            {
                return await _context.Customers.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Ett fel uppstod när kunderna hämtades.", ex);
            }
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            try
            {
                return await _context.Customers.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ett fel uppstod när kunden med ID {id} hämtades.", ex);
            }
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return customer;
            }
            catch (Exception ex)
            {
                throw new Exception("Ett fel uppstod när kunden skulle sparas i databasen.", ex);
            }
        }

        public async Task<Customer> UpdateCustomerAsync(Customer customer)
        {
            try
            {
                var existingCustomer = await _context.Customers.FindAsync(customer.CustomerId);
                if (existingCustomer != null)
                {
                    existingCustomer.Name = customer.Name;
                    existingCustomer.Email = customer.Email;
                    existingCustomer.Phone = customer.Phone;
                    _context.Entry(existingCustomer).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return existingCustomer;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ett fel uppstod när kunden med ID {customer.CustomerId} skulle uppdateras.", ex);
            }
        }

        public async Task DeleteCustomerAsync(int id)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);
                if (customer != null)
                {
                    _context.Customers.Remove(customer);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ett fel uppstod när kunden med ID {id} skulle raderas.", ex);
            }
        }

        // Implementera GetCustomerByEmailAsync-metoden
        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            try
            {
                return await _context.Customers.FirstOrDefaultAsync(c => c.Email == email);
            }
            catch (Exception ex)
            {
                throw new Exception("Ett fel uppstod när kunden skulle hämtas via e-post.", ex);
            }
        }
    }
}
