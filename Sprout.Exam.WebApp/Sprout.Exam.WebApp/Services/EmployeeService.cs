
using AutoMapper;
using Microsoft.Extensions.Logging;
using Sprout.Exam.Business.Calculations;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Validations;
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
        private readonly ISalaryCalculation _salaryCalculation;
        public EmployeeService(ILogger<EmployeeService> logger,
       IEmployeeRepository repository, IMapper mapper, ISalaryCalculation salaryCalculation)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _salaryCalculation = salaryCalculation;
        }

        public async Task<CrudResult<EmployeeModel>> UpsertEmployeeAsync(EmployeeModel request)
        {
            try
            {
                var birthDate = DateTime.Parse(request.Birthdate);
                var validate = await Task.FromResult(Validations.Validate(request.FullName, request.Tin, birthDate));

                if (validate is not null)
                {
                    return new CrudResult<EmployeeModel> { Entity = null, Count = 0, Message = validate };
                }

                var result = await _repository.UpsertEmployeeAsync(request);

                if (result.Count == 0)
                {
                    return new CrudResult<EmployeeModel> { Entity = null, Count = 0, Message = "Error on Saving Or No Changes Made" };
                }
                var employeeModel = _mapper.Map<EmployeeModel>(result.Entity);

                return new CrudResult<EmployeeModel> { Count = result.Count, Entity = employeeModel, Message = "Employee successfully saved" };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new CrudResult<EmployeeModel> { Entity = null, Count = 0, Message = e.Message };
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
                    return new CrudResult<EmployeeModel> { Entity = null, Count = 0, Message = "Error on Saving Changes" };

                }

                var employeeModel = _mapper.Map<EmployeeModel>(result.Entity);
                return new CrudResult<EmployeeModel> { Count = result.Count, Entity = employeeModel, Message = "Employee successfully Deleted" };

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new CrudResult<EmployeeModel> { Entity = null, Count = 0, Message = e.Message };
            }
        }

        public  SalaryResults GetCalculateEmployeeSalaryAsync(CalculateSalaryModel request)
        {
            try
            {
                if ((request.EmployeeTypeId == 2 || request.EmployeeTypeId == 4) && (request.InputDays > 31 || request.InputDays < 1))
                {

                    return new SalaryResults
                    {
                        Message = "Message Invalid Work Days",
                        Salary = 0.00m
                    };
                }
                else if ((request.EmployeeTypeId == 1 || request.EmployeeTypeId == 3) && (request.InputDays > 31 || request.InputDays < 0))
                {
                    return new SalaryResults
                    {
                        Message = "Message Invalid Absent Days",
                        Salary = 0.00m
                    };
                }

                var salary = _salaryCalculation.ComputeSalary((EmployeeType)request.EmployeeTypeId, request.InputDays);
                
                return new SalaryResults
                {
                    Message = null,
                    Salary = salary
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return new SalaryResults
                {
                    Message = e.Message,
                    Salary = 0.00m
                };
            }
        }
    }
}
