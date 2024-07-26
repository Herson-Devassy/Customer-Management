using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CustomerMvcApp.Models;

namespace CustomerMvcApp.Controllers
{
    /// <summary>
    /// Controller for managing customer-related operations.
    /// </summary>
    public class CustomersController : Controller
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersController"/> class.
        /// </summary>
        /// <param name="httpClientFactory">The factory for creating HttpClient instances.</param>
        public CustomersController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("CustomerApi");
        }

        /// <summary>
        /// Displays a list of customers.
        /// </summary>
        /// <returns>A view displaying all customers.</returns>
        public async Task<IActionResult> Index()
        {
            IEnumerable<Customer> customers = await _httpClient.GetFromJsonAsync<IEnumerable<Customer>>("customers");
            return View(customers);
        }

        // GET: /Customers/Create
        /// <summary>
        /// Displays the form for creating a new customer.
        /// </summary>
        /// <returns>A view with an empty customer model.</returns>
        public IActionResult Create()
        {
            return View(new Customer());
        }

        // POST: /Customers/Create
        /// <summary>
        /// Handles the form submission for creating a new customer.
        /// </summary>
        /// <param name="customer">The customer object to be created.</param>
        /// <returns>A redirect to the Index action if successful, otherwise the Create view with an error message.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("customers", customer);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
            return View(customer);
        }

        // GET: /Customers/Edit/5
        /// <summary>
        /// Displays the form for editing an existing customer.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to edit.</param>
        /// <returns>A view with the customer model if found, otherwise a 404 Not Found result.</returns>
        public async Task<IActionResult> Edit(int id)
        {
            Customer customer = await _httpClient.GetFromJsonAsync<Customer>($"customers/{id}");
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: /Customers/Edit/5
        /// <summary>
        /// Handles the form submission for updating an existing customer.
        /// </summary>
        /// <param name="customer">The customer object with updated data.</param>
        /// <returns>A redirect to the Index action if successful, otherwise the Edit view with an error message.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"customers/{customer.Id}", customer);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
            return View(customer);
        }

        // GET: /Customers/Details/5
        /// <summary>
        /// Displays the details of a specific customer.
        /// </summary>
        /// <param name="id">The unique identifier of the customer.</param>
        /// <returns>A view displaying the customer's details if found, otherwise a 404 Not Found result.</returns>
        public async Task<IActionResult> Details(int id)
        {
            Customer customer = await _httpClient.GetFromJsonAsync<Customer>($"customers/{id}");
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // GET: /Customers/Delete/5
        /// <summary>
        /// Displays the confirmation page for deleting a specific customer.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to delete.</param>
        /// <returns>A view with the customer model if found, otherwise a 404 Not Found result.</returns>
        public async Task<IActionResult> Delete(int id)
        {
            Customer customer = await _httpClient.GetFromJsonAsync<Customer>($"customers/{id}");
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: /Customers/Delete/5
        /// <summary>
        /// Handles the form submission for deleting a specific customer.
        /// </summary>
        /// <param name="id">The unique identifier of the customer to be deleted.</param>
        /// <returns>A redirect to the Index action if successful, otherwise the Delete view with an error message.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"customers/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            return View();
        }
    }
}
