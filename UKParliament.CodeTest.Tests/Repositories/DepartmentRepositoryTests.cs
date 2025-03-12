
using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Data.Entities;
using UKParliament.CodeTest.Data.Repositories.Departments;
using Xunit;

namespace UKParliament.CodeTest.Tests.Repositories
{
    public class DepartmentRepositoryTests
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfDepartments()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PersonManagerContext>()
                .UseInMemoryDatabase(databaseName: "PersonManager_Test")
                .Options;
            using var context = new PersonManagerContext(options);
            var repository = new DepartmentRepository(context);
            var sampleDepartments = new List<Department>
            {
                new() { Id = 1, Name = "Sales" },
                new() { Id = 2, Name = "Marketing" }
            };
            await context.Departments.AddRangeAsync(sampleDepartments);
            await context.SaveChangesAsync();
            // Act
            var result = await repository.GetAllAsync();
            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, p => p.Name == "Sales");
            Assert.Contains(result, p => p.Name == "Marketing");
        }
    }
}
