using CustomerApi.Models;
using Microsoft.EntityFrameworkCore;
using CustomerApi.Data;

namespace CustomerManagement.Repositories
{
    /// <summary>
    /// Repository class for managing customer data operations.
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRepository"/> class.
        /// </summary>
        /// <param name="context">The database context to be used by the repository.</param>
        public CustomerRepository(CustomerDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronously retrieves all customers from the database.
        /// </summary>
        /// <returns>A list of all customers.</returns>
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves a customer by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer.</param>
        /// <returns>The customer if found; otherwise, null.</returns>
        public async Task<Customer> GetCustomer(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        /// <summary>
        /// Asynchronously adds a new customer to the database.
        /// </summary>
        /// <param name="customer">The customer object to be added.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task AddCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously updates an existing customer's information in the database.
        /// </summary>
        /// <param name="customer">The customer object with updated information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously deletes a customer from the database by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task DeleteCustomer(int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }
    }
}
