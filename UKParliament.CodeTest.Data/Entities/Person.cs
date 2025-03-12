namespace UKParliament.CodeTest.Data.Entities;

public class Person
{
    public int Id { get; set; }

    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required int DepartmentId { get; set; }
    public Department? Department { get; set; }
}