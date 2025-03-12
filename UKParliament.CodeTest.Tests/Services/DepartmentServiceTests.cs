
using Xunit;
using UKParliament.CodeTest.Services;
using Moq;
using UKParliament.CodeTest.Data.Entities;
using UKParliament.CodeTest.Data.Repositories.Departments;

namespace UKParliament.CodeTest.Tests.Services
{
    public class DepartmentServiceTests
    {
        private readonly Mock<IDepartmentRepository> _mockRepo;
        private readonly DepartmentService _service;
        public DepartmentServiceTests()
        {
            _mockRepo = new Mock<IDepartmentRepository>();
            _service = new DepartmentService(_mockRepo.Object);
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfDepartments()
        {
            // Arrange
            var sampleDepartments = new List<Department>
            {
                new() { Id = 1, Name = "HR" },
                new() { Id = 2, Name = "IT" }
            };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(sampleDepartments);
            // Act
            var result = await _service.GetAllAsync();
            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, d => d.Name == "HR");
            Assert.Contains(result, d => d.Name == "IT");
        }
       
    }
}
