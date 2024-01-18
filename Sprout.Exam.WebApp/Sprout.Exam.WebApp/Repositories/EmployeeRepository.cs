using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sprout.Exam.WebApp.Data;
using Sprout.Exam.WebApp.Models;
using System;
using System.Collections.Generic;
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
            return await _context.Employee.ToListAsync().ConfigureAwait(false);
        }

        public async Task<bool> CreateEmployeeAsync(Employee request)
        {
            try
            {
                await _context.Employee.AddAsync(new Models.Employee
                {
                    Birthdate = request.Birthdate,
                    EmployeeTypeId = request.EmployeeTypeId,
                    FullName = request.FullName,
                    IsDeleted = request.IsDeleted,
                    Tin = request.Tin,
                });

                return (await _context.SaveChangesAsync() > 0);

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }
    }
}
