using Lab1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab1.Data.Repositories.IRepositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<Customer> AddCustomerAsync(Customer customer);
        Task<Customer> UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
        Task<Customer> GetCustomerByEmailAsync(string email); // Ny metod för att hämta kund via e-post
    }
}
