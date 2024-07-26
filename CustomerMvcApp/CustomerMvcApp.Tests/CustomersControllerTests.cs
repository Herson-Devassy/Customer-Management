using Moq;
using System.Net.Http.Json;
using CustomerMvcApp.Controllers;
using CustomerMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CustomerMvcApp.Tests.Controllers
{
    public class CustomersControllerTests
    {
        private readonly Mock<HttpClient> _mockHttpClient;
        private readonly CustomersController _controller;

        public CustomersControllerTests()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new Customer { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com" }
            };

            var mockHttpMessageHandler = new MockHttpMessageHandler(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(customers)
            });

            var httpClient = new HttpClient(mockHttpMessageHandler)
            {
                BaseAddress = new System.Uri("http://localhost/")
            };

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory.Setup(_ => _.CreateClient("CustomerApi")).Returns(httpClient);

            _controller = new CustomersController(mockHttpClientFactory.Object);
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfCustomers()
        {
            // Act
            var result = await _controller.Index() as ViewResult;
            var model = result?.Model as IEnumerable<Customer>;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
            Assert.NotNull(model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Create_POST_ReturnsRedirectToActionResult_WhenModelIsValid()
        {
            // Arrange
            var customer = new Customer { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" };
            //_mockHttpClient.Setup(client => client.PostAsJsonAsync("customers", customer))
                           //.ReturnsAsync(new HttpResponseMessage(System.Net.HttpStatusCode.Created));

            // Act
            var result = await _controller.Create(customer) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Edit_GET_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            int id = 999;
            //_mockHttpClient.Setup(client => client.GetFromJsonAsync<Customer>($"customers/{id}"))
            //               .ReturnsAsync((Customer)null);

            // Act
            var result = await _controller.Edit(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_POST_ReturnsRedirectToActionResult_WhenSuccessful()
        {
            // Arrange
            int id = 1;
            _mockHttpClient.Setup(client => client.DeleteAsync($"customers/{id}"))
                           .ReturnsAsync(new HttpResponseMessage(System.Net.HttpStatusCode.NoContent));

            // Act
            var result = await _controller.DeleteConfirmed(id) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }
    }
}
