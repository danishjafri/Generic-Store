using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Store.API.Controllers;
using Store.Models;
using Store.Repositories.Promises;

namespace Store.Controllers.Tests
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private Mock<IRepository<Customer>> _customerRepositoryMock;
        private CustomerController _customerController;

        [SetUp]
        public void Setup()
        {
            _customerRepositoryMock = new Mock<IRepository<Customer>>();
            _customerController = new CustomerController(_customerRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllCustomers_ReturnsAllCustomers()
        {
            // Arrange
            var customers = new List<Customer>
         {
             new Customer { Id = 1, FirstName = "John", LastName = "Doe" },
             new Customer { Id = 2, FirstName = "Jane", LastName = "Doe" },
             new Customer { Id = 3, FirstName = "Bob", LastName = "Smith" }
         };
            _customerRepositoryMock.Setup(c => c.GetAllAsync()).ReturnsAsync(customers);

            // Act
            var result = await _customerController.GetAllCustomers();

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result.Result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            var okObject = (IEnumerable<Customer>)okResult.Value;
            Assert.That(okObject, Is.EquivalentTo(customers));
        }

        // Add more tests for GetCustomerById, AddCustomer, UpdateCustomer, and DeleteCustomer methods
    }

}