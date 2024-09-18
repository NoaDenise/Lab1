using Lab1.Data.Repositories.IRepositories;
using Lab1.Models;
using Lab1.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();

            if (customers == null || !customers.Any())
            {
                return NotFound("Inga kunder hittades.");  // Returnerar ett felmeddelande om inga kunder finns
            }

            var customerDTOs = customers.Select(c => new CustomerDTO
            {
                CustomerId = c.CustomerId,
                Name = c.Name,
                Email = c.Email
            });

            return Ok(customerDTOs);
        }

        // GET: api/Customer/{customerId}
        [HttpGet("{customerId}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById(int customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                return NotFound($"Kunden med ID {customerId} hittades inte.");  // Returnerar 404 om kunden inte finns
            }

            var customerDTO = new CustomerDTO
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Email = customer.Email
            };

            return Ok(customerDTO);
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> AddCustomer(CustomerCreateDTO customerCreateDTO)
        {
            // Enkel validering av indata
            if (string.IsNullOrEmpty(customerCreateDTO.Name) || string.IsNullOrEmpty(customerCreateDTO.Email))
            {
                return BadRequest("Namn och e-postadress måste anges.");  // Returnerar 400 om indata är ogiltiga
            }

            var customer = new Customer
            {
                Name = customerCreateDTO.Name,
                Email = customerCreateDTO.Email,
                Phone = customerCreateDTO.Phone
            };

            var addedCustomer = await _customerRepository.AddCustomerAsync(customer);

            var customerDTO = new CustomerDTO
            {
                CustomerId = addedCustomer.CustomerId,
                Name = addedCustomer.Name,
                Email = addedCustomer.Email
            };

            return CreatedAtAction(nameof(GetCustomerById), new { customerId = addedCustomer.CustomerId }, customerDTO);
        }

        // PUT: api/Customer/{customerId}
        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomer(int customerId, CustomerUpdateDTO customerUpdateDTO)
        {
            if (customerId != customerUpdateDTO.CustomerId)
            {
                return BadRequest("Kund-ID matchar inte."); 
            }

            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (existingCustomer == null)
            {
                return NotFound($"Kunden med ID {customerId} hittades inte.");  // Returnerar 404 om kunden inte finns
            }

            existingCustomer.Name = customerUpdateDTO.Name;
            existingCustomer.Email = customerUpdateDTO.Email;
            existingCustomer.Phone = customerUpdateDTO.Phone;

            await _customerRepository.UpdateCustomerAsync(existingCustomer);

            return NoContent();  // Returnerar 204 om uppdateringen lyckades
        }

        // DELETE: api/Customer/{customerId}
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (existingCustomer == null)
            {
                return NotFound($"Kunden med ID {customerId} hittades inte.");  // Returnerar 404 om kunden inte finns
            }

            await _customerRepository.DeleteCustomerAsync(customerId);
            return NoContent();  // Returnerar 204 efter borttagning
        }
    }
}
