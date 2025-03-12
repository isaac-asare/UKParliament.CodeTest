using UKParliament.CodeTest.Data.Entities;

namespace UKParliament.CodeTest.Data.Repositories.People
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person?> GetByIdAsync(int personId);
        Task AddAsync(Person person);
        Task UpdateAsync(Person person);
        Task DeleteAsync(int personId);
    }
}
