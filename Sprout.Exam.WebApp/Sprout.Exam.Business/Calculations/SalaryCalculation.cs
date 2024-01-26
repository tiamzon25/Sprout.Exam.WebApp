using Sprout.Exam.Common.Enums;
using System;

namespace Sprout.Exam.Business.Calculations
{
    public static class SalaryCalculation
    {
        public static Decimal ComputeSalary(EmployeeType employeeType, Decimal inputDays)
        {
            var salary = 0.0m;

            switch (employeeType)
            {
                case EmployeeType.Regular:
                    salary = RegularEmployeeComputation(inputDays);
                    break;
                case EmployeeType.Contractual:
                    salary = ContractualEmployeeComputation(inputDays);
                    break;
                case EmployeeType.Probationary:
                    salary = ProbationaryEmployeeComputation(inputDays);
                    break;
                case EmployeeType.PartTime:
                    salary = PartTimeEmployeeComputation(inputDays);
                    break;
                default: return salary;
            }

            return Math.Round(salary, 2);
        }
        public static Decimal RegularEmployeeComputation(decimal inputDays)
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
        public static Decimal ProbationaryEmployeeComputation(decimal inputDays)
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
        public static Decimal ContractualEmployeeComputation(decimal inputDays)
        {
            var rate = 500.00m;

            return (rate * inputDays);

        }
        public static Decimal PartTimeEmployeeComputation(decimal inputDays)
        {
            var rate = 400.00m;

            return (rate * inputDays);

        }
    }
}
