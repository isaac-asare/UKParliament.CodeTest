

namespace UKParliament.CodeTest.Services.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string DateOfBirth { get; set; }
        public required int DepartmentId { get; set; }
        public required string ? DepartmentName { get; set; }
    }
}
