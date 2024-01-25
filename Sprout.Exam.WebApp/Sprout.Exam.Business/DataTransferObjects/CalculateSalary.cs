namespace Sprout.Exam.Business.DataTransferObjects
{
    public class CalculateSalary
    {
        public int Id { get; set; }
        public int EmployeeTypeId { get; set; }
        public decimal AbsentDays { get; set; }
        public decimal workedDays { get; set; }
    }
}
