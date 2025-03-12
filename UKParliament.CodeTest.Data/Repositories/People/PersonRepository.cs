using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data.Entities;


namespace UKParliament.CodeTest.Data.Repositories.People
{
    public class PersonRepository(PersonManagerContext context) : IPersonRepository
    {
        private readonly PersonManagerContext _context = context;

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.People
                .Include(p => p.Department)
                .ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(int personId)
        {
            return await _context.People
                .Include(p => p.Department)
                .FirstOrDefaultAsync(p => p.Id == personId);
        }

        public async Task AddAsync(Person person)
        {
            await _context.People.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Person person)
        {
            _context.People.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int personId)
        {
            var existingPerson = await _context.People.FindAsync(personId);
            if (existingPerson != null)
            {
                _context.People.Remove(existingPerson);
                await _context.SaveChangesAsync();
            }
        }
    }
}
