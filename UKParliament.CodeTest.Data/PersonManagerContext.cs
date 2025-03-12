using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data.Entities;

namespace UKParliament.CodeTest.Data;

public class PersonManagerContext(DbContextOptions<PersonManagerContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Department>().HasData(
            new Department { Id = 1, Name = "Sales" },
            new Department { Id = 2, Name = "Marketing" },
            new Department { Id = 3, Name = "Finance" },
            new Department { Id = 4, Name = "HR" });

        modelBuilder.Entity<Person>().HasData(
            new Person { Id = 1, FirstName = "Isaac", LastName = "Asare", DateOfBirth = new DateTime(1980, 1, 1, 0, 0, 0, DateTimeKind.Utc), DepartmentId = 1 },
            new Person { Id = 2, FirstName = "Christiana", LastName = "Danso", DateOfBirth = new DateTime(1985, 2, 2, 0, 0, 0, DateTimeKind.Utc), DepartmentId = 2 },
            new Person { Id = 3, FirstName = "Ivan", LastName = "Opoku", DateOfBirth = new DateTime(1990, 3, 3, 0, 0, 0, DateTimeKind.Utc), DepartmentId = 3 },
            new Person { Id = 4, FirstName = "Cedric", LastName = "Yaw", DateOfBirth = new DateTime(1995, 4, 4, 0, 0, 0, DateTimeKind.Utc), DepartmentId = 4 });

    }

    public DbSet<Person> People { get; set; }

    public DbSet<Department> Departments { get; set; }
}
