using UKParliament.CodeTest.Data.Entities;
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Services.Mappers
{
    public static class PersonMapper
    {
        public static PersonModel ToViewModel(this Person person)
        {
            return new PersonModel
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                DateOfBirth = person.DateOfBirth.ToString("yyyy-MM-dd"),
                DepartmentId = person.DepartmentId,
                DepartmentName = person.Department?.Name
            };
        }

        public static IEnumerable<PersonModel> ToModels(this IEnumerable<Person> people)
        {
            return people.Select(ToViewModel);
        }

        public static Person ToEntityModel(this PersonModel personModel)
        {
            return new Person
            {
                Id = personModel.Id,
                FirstName = personModel.FirstName,
                LastName = personModel.LastName,
                DateOfBirth = Convert.ToDateTime(personModel.DateOfBirth),
                DepartmentId = personModel.DepartmentId
            };
        }
    }

}
