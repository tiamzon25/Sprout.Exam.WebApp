
using AutoMapper;
using Microsoft.Extensions.Logging;
using Sprout.Exam.Business.Calculations;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.Models;
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
        private readonly IMapper _mapper;
        public EmployeeService(ILogger<EmployeeService> logger,
       IEmployeeRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CrudResult<EmployeeModel>> UpsertEmployeeAsync(EmployeeModel request)
        {
            try
            {
                var result = await _repository.UpsertEmployeeAsync(request);

                if (result.Count == 0)
                {
                    return new CrudResult<EmployeeModel> { Entity = null, Count = 0 };
                }
                var employeeModel = _mapper.Map<EmployeeModel>(result);




                //(EmployeeType)employee.EmployeeType;

                //if (returnValue.Count > 0)
                //{
                //    result.Success = true;
                //    result.Message = "Successfully Created";
                //}
                //else
                //{
                //    result.Success = false;
                //}

                return new CrudResult<EmployeeModel> { Count = result.Count, Entity = employeeModel };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new CrudResult<EmployeeModel> { Entity = null, Count = 0 };
            }

        }

        public async Task<List<EmployeeModel>> ListEmployeesAsync()
        {
            try
            {
                List<Employee> employees = await _repository.ListEmployeesAsync();
                return _mapper.Map<List<EmployeeModel>>(employees);
            }
            catch
            {
                return new List<EmployeeModel>();
            }
        }

        public async Task<EmployeeModel> GetEmployeeAsync(int id)
        {
            try
            {
                var employee = await _repository.GetEmployeeAsync(id);
                var employeeModel = _mapper.Map<EmployeeModel>(employee);

                return employeeModel;
            }
            catch
            {
                return new EmployeeModel();
            }
        }

        public async Task<CrudResult<EmployeeModel>> GetEmployeeDeleteAsync(int id)
        {
            try
            {
                var result = await _repository.GetEmployeeDeleteAsync(id);

                if (result.Count == 0)
                {
                    return new CrudResult<EmployeeModel> { Entity = null, Count = 0 };

                }

                var employeeModel = _mapper.Map<EmployeeModel>(result);
                return new CrudResult<EmployeeModel> { Count = result.Count, Entity = employeeModel };

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new CrudResult<EmployeeModel> { Entity = null, Count = 0 };
            }
        }

        public async Task<Decimal> GetCalculateEmployeeSalaryAsync(CalculateSalaryModel request)
        {
            try
            {

                var salary = SalaryCalculation.ComputeSalary((EmployeeType)request.EmployeeTypeId, request.workedDays, request.AbsentDays);

                return salary;
            }
            catch
            {
                return 0.00m;
            }
        }
    }
}
