using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sprout.Exam.WebApp.Data;
using Sprout.Exam.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Repositories
{
    public partial class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeeRepository> _logger;


        public EmployeeRepository(ILogger<EmployeeRepository> logger,
            ApplicationDbContext context)

        {
            _logger = logger;
            _context = context;
        }
        public async Task<List<Employee>> ListEmployeesAsync()
        {
            return await _context.Employee.Where(x => x.IsDeleted == false).ToListAsync().ConfigureAwait(false);
        }
        public async Task<Employee> GetEmployeeAsync(int id)
        {
            return await _context.Employee.FindAsync(id);
        }
        public async Task<Common.Models.CrudResult<Employee>> GetEmployeeDeleteAsync(int id)
        {
            try
            {
                var employee = await _context.Employee.FindAsync(id);
                employee.IsDeleted = true;

                return new Common.Models.CrudResult<Employee> { Entity = employee, Count = await _context.SaveChangesAsync() };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new Common.Models.CrudResult<Employee> { Entity = new Employee(), Count = await _context.SaveChangesAsync() };
            }

        }
        public async Task<Common.Models.CrudResult<Employee>> UpsertEmployeeAsync(EmployeeModel request)
        {
            try
            {
                var getEmployee = await _context.Employee.FindAsync(request.Id);

                if (getEmployee == null)
                {
                    await _context.Employee.AddAsync(new Models.Employee
                    {
                        Birthdate = DateTime.Parse(request.Birthdate),//request.Birthdate,
                        EmployeeTypeId = request.EmployeeTypeId,
                        FullName = request.FullName,
                        IsDeleted = request.IsDeleted,
                        Tin = request.Tin,
                    });
                }
                else
                {
                    getEmployee.FullName = request.FullName;
                    getEmployee.Birthdate = DateTime.Parse(request.Birthdate);
                    getEmployee.EmployeeTypeId = request.EmployeeTypeId;
                    getEmployee.Tin = request.Tin;
                    getEmployee.IsDeleted = request.IsDeleted;
                }

                return new Common.Models.CrudResult<Employee> { Entity = getEmployee, Count = await _context.SaveChangesAsync() };

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new Common.Models.CrudResult<Employee> { Entity = new Employee(), Count = await _context.SaveChangesAsync() };
            }
        }
    }
}
