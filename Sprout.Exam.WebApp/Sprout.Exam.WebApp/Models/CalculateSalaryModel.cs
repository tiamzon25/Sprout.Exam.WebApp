namespace Sprout.Exam.WebApp.Models
{
    public class CalculateSalaryModel
    {
        public int Id { get; set; }
        public int EmployeeTypeId { get; set; }
        public decimal AbsentDays { get; set; }
        public decimal workedDays { get; set; }
    }
}
