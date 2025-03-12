
using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data;
using UKParliament.CodeTest.Data.Entities;
using UKParliament.CodeTest.Data.Repositories.People;
using Xunit;

namespace UKParliament.CodeTest.Tests.Repositories
{
    public class PersonRepositoryTests
    {
        [Fact]
        public async Task AddAsync_ShouldAddPersonToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PersonManagerContext>()
                .UseInMemoryDatabase(databaseName: "PersonManager_Test")
                .Options;

            using var context = new PersonManagerContext(options);
            var repository = new PersonRepository(context);

            var person = new Person
            {
                FirstName = "Test",
                LastName = "User",
                DepartmentId = 1,
                DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            };

            // Act
            await repository.AddAsync(person);

            // Assert
            Assert.Equal(1, await context.People.CountAsync());
            Assert.Equal("Test", (await context.People.FirstAsync()).FirstName);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdatePersonInDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PersonManagerContext>()
                .UseInMemoryDatabase(databaseName: "PersonManager_Test")
                .Options;
            using var context = new PersonManagerContext(options);
            var repository = new PersonRepository(context);
            var person = new Person
            {
                FirstName = "Test",
                LastName = "User",
                DepartmentId = 1,
                DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            };
            await repository.AddAsync(person);
            // Act
            person.FirstName = "Updated";
            await repository.UpdateAsync(person);
            // Assert
            Assert.Equal(1, await context.People.CountAsync());
            Assert.Equal("Updated", (await context.People.FirstAsync()).FirstName);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeletePersonFromDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PersonManagerContext>()
                .UseInMemoryDatabase(databaseName: "PersonManager_Test")
                .Options;
            using var context = new PersonManagerContext(options);
            var repository = new PersonRepository(context);
            var person = new Person
            {
                FirstName = "Test",
                LastName = "User",
                DepartmentId = 1,
                DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            };
            await repository.AddAsync(person);
            // Act
            await repository.DeleteAsync(person.Id);
            // Assert
            Assert.Equal(0, await context.People.CountAsync());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnPersonFromDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PersonManagerContext>()
                .UseInMemoryDatabase(databaseName: "PersonManager_Test")
                .Options;
            using var context = new PersonManagerContext(options);
            var repository = new PersonRepository(context);
            var person = new Person
            {
                FirstName = "Test",
                LastName = "User",
                DepartmentId = 1,
                DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            };
            await repository.AddAsync(person);
            // Act
            var result = await repository.GetByIdAsync(person.Id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test", result.FirstName);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfPersons()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PersonManagerContext>()
                .UseInMemoryDatabase(databaseName: "PersonManager_Test")
                .Options;
            using var context = new PersonManagerContext(options);
            var repository = new PersonRepository(context);
            var samplePersons = new List<Person>
            {
                new() { Id = 1, FirstName = "Alice", LastName = "Smith", DateOfBirth = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc), DepartmentId = 1 },
                new() { Id = 2, FirstName = "Bob", LastName = "Jones", DateOfBirth = new DateTime(1990, 2, 2, 0, 0, 0, DateTimeKind.Utc), DepartmentId = 2 }
            };
            await context.People.AddRangeAsync(samplePersons);
            await context.SaveChangesAsync();
            // Act
            var result = await repository.GetAllAsync();
            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, p => p.FirstName == "Alice");
            Assert.Contains(result, p => p.FirstName == "Bob");
        }


    }
}
