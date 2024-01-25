using Sprout.Exam.Common.Enums;
using System;

namespace Sprout.Exam.Business.Calculations
{
    public static class SalaryCalculation
    {
        public static Decimal ComputeSalary(EmployeeType employeeType, Decimal workedDays, decimal absentDays)
        {
            var salary = 0.0m;

            switch (employeeType)
            {
                case EmployeeType.Regular:
                    salary = RegularEmployeeComputation(absentDays);
                    break;
                case EmployeeType.Contractual:
                    salary = ContractualEmployeeComputation(workedDays);
                    break;
                default: return salary;
            }

            return Math.Round(salary, 2);
        }
        public static Decimal RegularEmployeeComputation(decimal absentDays)
        {
            var baseSalary = 20000.00m;
            var baseDailySalary = baseSalary / 22;
            var tax = 0.12m;
            var taxBasePay = 20000 * tax;

            return (baseSalary - baseDailySalary - taxBasePay);

        }
        public static Decimal ContractualEmployeeComputation(decimal workedDays)
        {
            var rate = 500.00m;

            return (rate * workedDays);

        }
    }
}
