using UKParliament.CodeTest.Data.Repositories.People;
using UKParliament.CodeTest.Services.Interfaces;
using UKParliament.CodeTest.Services.Mappers;
using UKParliament.CodeTest.Services.Models;
using UKParliament.CodeTest.Services.Validators;

namespace UKParliament.CodeTest.Services;

public class PersonService(IPersonRepository personRepository) : IPersonService
{
    private readonly IPersonRepository _personRepository = personRepository;

    public async Task<IEnumerable<PersonModel>> GetAllAsync()
    {
        var people = await _personRepository.GetAllAsync();
        return people.ToModels();
    }

    public async Task<PersonModel?> GetByIdAsync(int id)
    {
        var person = await _personRepository.GetByIdAsync(id);
        return person?.ToViewModel();
    }

    public async Task<(bool Success, IEnumerable<string> Errors, PersonModel? Result)> CreateAsync(PersonModel personModel)
    {
        var errors = PersonValidator.Validate(personModel);
        if (errors.Any())
        {
            return (false, errors, null);
        }

        var entity = personModel.ToEntityModel();

        await _personRepository.AddAsync(entity);

        var savedPerson = await _personRepository.GetByIdAsync(entity.Id);
        var resultDto = savedPerson?.ToViewModel();

        return (true, Enumerable.Empty<string>(), resultDto);
    }

    public async Task<(bool Success, IEnumerable<string> Errors)> UpdateAsync(PersonModel personModel)
    {
        var errors = PersonValidator.Validate(personModel);
        if (errors.Any())
        {
            return (false, errors);
        }

        var existing = await _personRepository.GetByIdAsync(personModel.Id);
        if (existing == null)
        {
            return (false, new[] { "Person not found." });
        }

        existing.FirstName = personModel.FirstName;
        existing.LastName = personModel.LastName;
        existing.DateOfBirth = Convert.ToDateTime(personModel.DateOfBirth);
        existing.DepartmentId = personModel.DepartmentId;

        await _personRepository.UpdateAsync(existing);

        return (true, Enumerable.Empty<string>());
    }

    public async Task DeleteAsync(int id)
    {
        await _personRepository.DeleteAsync(id);
    }

   
}
