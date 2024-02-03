using Sprout.Exam.DataAccess.Models;
using Sprout.Exam.WebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> ListEmployeesAsync();
        Task<Employee> GetEmployeeAsync(int id);
        Task<Common.Models.CrudResult<Employee>> UpsertEmployeeAsync(EmployeeModel request);
        Task<Common.Models.CrudResult<Employee>> GetEmployeeDeleteAsync(int id);
    }
}
