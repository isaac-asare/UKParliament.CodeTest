
using Xunit;
using UKParliament.CodeTest.Services;
using Moq;
using UKParliament.CodeTest.Services.Models;
using UKParliament.CodeTest.Data.Entities;
using UKParliament.CodeTest.Data.Repositories.People;

namespace UKParliament.CodeTest.Tests.Services
{
    public class PersonServiceTests
    {
        private readonly Mock<IPersonRepository> _mockRepo;
        private readonly PersonService _service;

        public PersonServiceTests()
        {
            _mockRepo = new Mock<IPersonRepository>();
            _service = new PersonService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfPersons()
        {
            // Arrange
            var samplePersons = new List<Person>
            {
                new() { Id = 1, FirstName = "Alice", LastName = "Smith", DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), DepartmentId = 1 },
                new() { Id = 2, FirstName = "Bob", LastName = "Jones", DateOfBirth = new DateTime(1990, 2, 2, 0, 0, 0, DateTimeKind.Utc), DepartmentId = 2 }
            };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(samplePersons);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, p => p.FirstName == "Alice");
            Assert.Contains(result, p => p.FirstName == "Bob");
        }

        [Fact]
        public async Task CreateAsync_InvalidData_ShouldReturnErrors()
        {
            // Arrange
            var dto = new PersonModel
            {
                FirstName = "",
                LastName = "Doe",
                DateOfBirth = DateTime.Now.AddYears(-20).ToShortDateString(),
                DepartmentId = 1,
                DepartmentName = "HR"
            };

            // Act
            var (Success, Errors, Result) = await _service.CreateAsync(dto);

            // Assert
            Assert.False(Success);
            Assert.NotEmpty(Errors);
            Assert.Null(Result);
        }

        [Fact]
        public async Task CreateAsync_ValidPerson_ShouldSaveAndReturnDto()
        {
            // Arrange
            var dto = new PersonModel
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = DateTime.Now.AddYears(-20).ToShortDateString(),
                DepartmentId = 1,
                DepartmentName = "HR"
            };

            // Mock the repository calls
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Person>()))
                     .Returns(Task.CompletedTask);

            var savedPerson = new Person
            {
                Id = 123,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = Convert.ToDateTime( dto.DateOfBirth),
                DepartmentId = 1
            };
            _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                     .ReturnsAsync(savedPerson);

            // Act
            var (Success, Errors, Result) = await _service.CreateAsync(dto);

            // Assert
            Assert.True(Success);
            Assert.Empty(Errors);
            Assert.NotNull(Result);
            Assert.Equal("John", Result!.FirstName);

            // Verify the repo AddAsync was called
            _mockRepo.Verify(r => r.AddAsync(It.IsAny<Person>()), Times.Once);
        }
    }
}
