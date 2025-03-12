using Microsoft.EntityFrameworkCore;
using UKParliament.CodeTest.Data.Entities;

namespace UKParliament.CodeTest.Data.Repositories.Departments
{
    public class DepartmentRepository(PersonManagerContext context) : IDepartmentRepository
    {
        private readonly PersonManagerContext _context = context;

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }
    }
}
