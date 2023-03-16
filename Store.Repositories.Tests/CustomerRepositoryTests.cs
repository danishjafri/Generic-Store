using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Store.Db;
using Store.Models;
using Store.Repositories.Implementations;
using Store.Repositories.Promises;

[TestFixture]
public class CustomerRepositoryTests
{
    private Mock<AppDbContext> _dbContextMock;
    private IRepository<Customer> _customerRepository;

    [SetUp]
    public void Setup()
    {
        _dbContextMock = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
        _customerRepository = new CustomerRepository(_dbContextMock.Object);
    }

    [Test]
    public async Task GetAllAsync_ReturnsAllCustomers()
    {
        // Arrange
        var customers = new List<Customer>
         {
             new Customer { Id = 1, FirstName = "John", LastName = "Doe" },
             new Customer { Id = 2, FirstName = "Jane", LastName = "Doe" },
             new Customer { Id = 3, FirstName = "Bob", LastName = "Smith" }
         };
        _dbContextMock.Setup(c => c.Customers.ToListAsync(default(CancellationToken))).ReturnsAsync(customers);

        // Act
        var result = await _customerRepository.GetAllAsync();

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(customers.Count));
        Assert.That(result, Is.EquivalentTo(customers));
    }

    // Add more tests for GetByIdAsync, AddAsync, UpdateAsync, and DeleteAsync methods
}
