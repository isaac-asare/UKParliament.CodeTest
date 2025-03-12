
using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Services.Validators
{
    public static class PersonValidator
    {
        public static IList<string> Validate(PersonModel model)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(model.FirstName))
                errors.Add("First Name is required.");

            if (string.IsNullOrWhiteSpace(model.LastName))
                errors.Add("Last Name is required.");

            if (model.DateOfBirth == default)
                errors.Add("Date of Birth is required.");

            if (model.DepartmentId <= 0)
                errors.Add("Valid Department must be selected.");

            return errors;
        }
    }
}
