using UKParliament.CodeTest.Data.Entities;

namespace UKParliament.CodeTest.Data.Repositories.Departments
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAllAsync();
    }
}
