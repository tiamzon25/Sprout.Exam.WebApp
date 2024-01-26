using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Models;
using Sprout.Exam.WebApp.Services;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService _service;
        private readonly ILogger<EmployeesController> _logger;


        public EmployeesController(EmployeeService service,
       ILogger<EmployeesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.ListEmployeesAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetEmployeeAsync(id);

            if (result == null)
            {
                return NotFound("No Employee Found");
            }

            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeDto input)
        {


            var inputEmployee = new EmployeeModel
            {
                Id = input.Id,
                Birthdate = input.Birthdate.ToString("yyyy-MM-dd"),
                EmployeeTypeId = input.TypeId,
                FullName = input.FullName,
                Tin = input.Tin,
                IsDeleted = false,
            };

            var result = await _service.UpsertEmployeeAsync(inputEmployee);

            return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {
            var inputEmployee = new EmployeeModel
            {
                Birthdate = input.Birthdate.ToString("yyyy-MM-dd"),
                EmployeeTypeId = input.TypeId,
                FullName = input.FullName,
                Tin = input.Tin,
                IsDeleted = false,
            };

            var result = await _service.UpsertEmployeeAsync(inputEmployee);

            return Ok(result);
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.GetEmployeeDeleteAsync(id);

            return Ok(result.Entity.Id);
        }



        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        //[HttpPost("{id}/calculate")]
        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate(CalculateSalary input)
        {
            var salary = await _service.GetCalculateEmployeeSalaryAsync(
                new CalculateSalaryModel
                {
                    InputDays = input.InputDays,
                    EmployeeTypeId = input.EmployeeTypeId,
                    Id = input.Id,

                }
                );

            return Ok(salary);
        }

    }
}
