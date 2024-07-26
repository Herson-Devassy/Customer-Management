using System.ComponentModel.DataAnnotations;

namespace CustomerMvcApp.Models
{
    /// <summary>
    /// Represents a customer entity in the MVC application.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Gets or sets the unique identifier for the customer.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the customer.
        /// This field is required.
        /// </summary>
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the customer.
        /// This field is required.
        /// </summary>
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the customer.
        /// This field is required and must be a valid email address.
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the customer.
        /// This field must be a valid phone number format.
        /// </summary>
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the address of the customer.
        /// This field is optional.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the gender of the customer.
        /// This field is optional. Example values include "Male", "Female", "Other".
        /// </summary>
        public string Gender { get; set; }
    }
}
