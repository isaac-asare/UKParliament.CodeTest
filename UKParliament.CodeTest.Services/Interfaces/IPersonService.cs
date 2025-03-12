using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Services.Interfaces;

public interface IPersonService
{
    Task<IEnumerable<PersonModel>> GetAllAsync();
    Task<PersonModel?> GetByIdAsync(int id);
    Task<(bool Success, IEnumerable<string> Errors, PersonModel? Result)> CreateAsync(PersonModel personModel);
    Task<(bool Success, IEnumerable<string> Errors)> UpdateAsync(PersonModel personModel);
    Task DeleteAsync(int id);
}
