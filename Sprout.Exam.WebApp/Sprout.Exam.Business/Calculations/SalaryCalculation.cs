using Sprout.Exam.Common.Enums;
using System;

namespace Sprout.Exam.Business.Calculations
{
    public class SalaryCalculation : ISalaryCalculation
    {
        public decimal ComputeSalary(EmployeeType employeeType, decimal inputDays)
        {
            var salary = employeeType switch
            {
                EmployeeType.Regular => RegularEmployeeComputation(inputDays),
                EmployeeType.Contractual => ContractualEmployeeComputation(inputDays),
                EmployeeType.Probationary => ProbationaryEmployeeComputation(inputDays),
                EmployeeType.PartTime => PartTimeEmployeeComputation(inputDays),
                _ => 0m

            };

            return Math.Round(salary, 2);
        }
        private decimal RegularEmployeeComputation(decimal inputDays)
        {
            var baseSalary = 20000.00m;
            var baseDailySalary = (baseSalary / 22) * inputDays;
            var tax = 0.12m;
            var taxBasePay = 20000 * tax;

            if (inputDays >= 22)
            {
                return 0.00m;
            }

            return (baseSalary - baseDailySalary - taxBasePay);

        }
        private decimal ProbationaryEmployeeComputation(decimal inputDays)
        {
            var baseSalary = 16000.00m;
            var baseDailySalary = (baseSalary / 22) * inputDays;
            var tax = 0.12m;
            var taxBasePay = 16000 * tax;

            if (inputDays >= 22)
            {
                return 0.00m;
            }

            return (baseSalary - baseDailySalary - taxBasePay);

        }
        private decimal ContractualEmployeeComputation(decimal inputDays)
        {
            var rate = 500.00m;

            return (rate * inputDays);

        }
        private decimal PartTimeEmployeeComputation(decimal inputDays)
        {
            var rate = 400.00m;

            return (rate * inputDays);

        }
    }
}
