using Microsoft.AspNetCore.Mvc;
using CustomerApi.Models;
using CustomerManagement.Repositories;

namespace CustomerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersController"/> class.
        /// </summary>
        /// <param name="repository">The repository instance used for customer data operations.</param>
        public CustomersController(ICustomerRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves all customers from the repository.
        /// </summary>
        /// <returns>A list of customers.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            IEnumerable<Customer> customers = await _repository.GetCustomers();
            return Ok(customers);
        }

        /// <summary>
        /// Retrieves a specific customer by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer.</param>
        /// <returns>The customer if found; otherwise, a 404 Not Found result.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            Customer customer = await _repository.GetCustomer(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        /// <summary>
        /// Adds a new customer to the repository.
        /// </summary>
        /// <param name="customer">The customer object to be added.</param>
        /// <returns>A 201 Created result with the location of the new customer and the customer data.</returns>
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            await _repository.AddCustomer(customer);
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        /// <summary>
        /// Updates an existing customer in the repository.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to be updated.</param>
        /// <param name="customer">The customer object with updated data.</param>
        /// <returns>A 400 Bad Request result if the ID does not match or a 204 No Content result if the update was successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
                return BadRequest();

            await _repository.UpdateCustomer(customer);
            return NoContent();
        }

        /// <summary>
        /// Deletes a specific customer by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to be deleted.</param>
        /// <returns>A 204 No Content result if the deletion was successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _repository.DeleteCustomer(id);
            return NoContent();
        }
    }
}
