using Microsoft.Extensions.Logging;
using Sprout.Exam.WebApp.Models;
using Sprout.Exam.WebApp.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Services
{
    public partial class EmployeeService
    {

        private readonly ILogger<EmployeeService> _logger;
        private readonly IEmployeeRepository _repository;

        public EmployeeService(ILogger<EmployeeService> logger,
       IEmployeeRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<string> CreateEmployeeAsync(Employee request)
        {
            try
            {
                var returnValue = await _repository.CreateEmployeeAsync(request);
                return returnValue == false ? "Not Created" : "Successfully Created";
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return e.Message;
            }

            //return await _repository.HandleAsync(new BelastingTabellenWitGroenRepository.GetWoonlandbeginsel(), CancellationToken.None);
        }

        public async Task<List<Employee>> ListEmployeeAsync()
        {
            try
            {
                return await _repository.ListEmployeesAsync();
            }
            catch
            {
                return new List<Employee>();
            }
        }
    }
}
