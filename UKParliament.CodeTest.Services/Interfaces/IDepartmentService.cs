using UKParliament.CodeTest.Services.Models;

namespace UKParliament.CodeTest.Services.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentModel>> GetAllAsync();
}