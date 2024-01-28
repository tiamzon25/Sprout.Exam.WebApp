using Sprout.Exam.Common.Enums;

namespace Sprout.Exam.Business.Calculations
{
    public interface ISalaryCalculation
    {
        public  decimal ComputeSalary(EmployeeType employeeType, decimal inputDays);
    }
}