using CustomerApi.Controllers;
using CustomerApi.Models;
using CustomerManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CustomerApi.Tests.Controllers
{
    public class CustomersControllerTests
    {
        private readonly CustomersController _controller;
        private readonly Mock<ICustomerRepository> _mockRepository;

        public CustomersControllerTests()
        {
            _mockRepository = new Mock<ICustomerRepository>();
            _controller = new CustomersController(_mockRepository.Object);
        }

        /// <summary>
        /// Tests that the GetCustomers action returns an OkObjectResult with a list of customers.
        /// </summary>
        [Fact]
        public async Task GetCustomers_ReturnsOkResult_WithListOfCustomers()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new Customer { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com" }
            };
            _mockRepository.Setup(repo => repo.GetCustomers()).ReturnsAsync(customers);

            // Act
            var result = await _controller.GetCustomers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<List<Customer>>(okResult.Value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(model);
            Assert.Equal(2, model.Count);
        }

        /// <summary>
        /// Tests that the GetCustomer action returns an OkObjectResult with a specific customer when found.
        /// </summary>
        [Fact]
        public async Task GetCustomer_ReturnsOkResult_WithCustomer()
        {
            // Arrange
            var customer = new Customer { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            _mockRepository.Setup(repo => repo.GetCustomer(1)).ReturnsAsync(customer);

            // Act
            var result = await _controller.GetCustomer(1);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<Customer>(okResult.Value);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(model);
            Assert.Equal(customer.Id, model.Id);
        }

        /// <summary>
        /// Tests that the GetCustomer action returns a NotFoundResult when the customer does not exist.
        /// </summary>
        [Fact]
        public async Task GetCustomer_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetCustomer(1)).ReturnsAsync((Customer)null);

            // Act
            var result = await _controller.GetCustomer(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        /// <summary>
        /// Tests that the PostCustomer action returns a CreatedAtActionResult when the customer is successfully created.
        /// </summary>
        [Fact]
        public async Task PostCustomer_ReturnsCreatedAtActionResult_WhenModelIsValid()
        {
            // Arrange
            // Create a sample customer object to be added
            var customer = new Customer
            {
                Id = 1, // Assuming the Id is auto-generated and returned from the repository
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com"
            };

            // Setup the mock repository to return a completed task when AddCustomer is called
            _mockRepository.Setup(repo => repo.AddCustomer(customer)).Returns(Task.CompletedTask);

            // Act
            // Call the PostCustomer method and get the result
            var result = await _controller.PostCustomer(customer);

            // Assert
            // Verify that the result is of type CreatedAtActionResult
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);

            // Check that the ActionName property of CreatedAtActionResult is "GetCustomer"
            Assert.Equal("GetCustomer", createdAtActionResult.ActionName);

            // Verify that the Id of the returned customer matches the Id of the input customer
            var returnedCustomer = Assert.IsType<Customer>(createdAtActionResult.Value);
            Assert.Equal(customer.Id, returnedCustomer.Id);
        }

        /// <summary>
        /// Tests that the UpdateCustomer action returns a NoContentResult when the customer is successfully updated.
        /// </summary>
        [Fact]
        public async Task UpdateCustomer_ReturnsNoContent_WhenModelIsValid()
        {
            // Arrange
            var customer = new Customer { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            _mockRepository.Setup(repo => repo.UpdateCustomer(customer)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateCustomer(1, customer);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        /// <summary>
        /// Tests that the UpdateCustomer action returns a BadRequestResult when the customer ID does not match.
        /// </summary>
        [Fact]
        public async Task UpdateCustomer_ReturnsBadRequest_WhenIdDoesNotMatch()
        {
            // Arrange
            var customer = new Customer { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };

            // Act
            var result = await _controller.UpdateCustomer(2, customer);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        /// <summary>
        /// Tests that the DeleteCustomer action returns a NoContentResult when the customer is successfully deleted.
        /// </summary>
        [Fact]
        public async Task DeleteCustomer_ReturnsNoContent_WhenCustomerExists()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.DeleteCustomer(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteCustomer(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
