using Microsoft.EntityFrameworkCore;
using CustomerApi.Models;

namespace CustomerApi.Data
{
    /// <summary>
    /// Represents the database context for managing customer data.
    /// </summary>
    public class CustomerDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet of customers.
        /// This property represents the collection of all customers in the database.
        /// </summary>
        public DbSet<Customer> Customers { get; set; }
    }
}
