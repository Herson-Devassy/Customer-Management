using CustomerApi.Models;

namespace CustomerManagement.Repositories
{
    /// <summary>
    /// Interface defining the operations for managing customer data.
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Asynchronously retrieves all customers.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of customers.</returns>
        Task<IEnumerable<Customer>> GetCustomers();

        /// <summary>
        /// Asynchronously retrieves a customer by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the customer if found; otherwise, null.</returns>
        Task<Customer> GetCustomer(int id);

        /// <summary>
        /// Asynchronously adds a new customer.
        /// </summary>
        /// <param name="customer">The customer object to be added.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddCustomer(Customer customer);

        /// <summary>
        /// Asynchronously updates an existing customer's information.
        /// </summary>
        /// <param name="customer">The customer object with updated information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateCustomer(Customer customer);

        /// <summary>
        /// Asynchronously deletes a customer by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteCustomer(int id);
    }
}
