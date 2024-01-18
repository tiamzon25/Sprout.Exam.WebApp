using Sprout.Exam.WebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> ListEmployeesAsync();
        Task<bool> CreateEmployeeAsync(Employee request);
    }
}
