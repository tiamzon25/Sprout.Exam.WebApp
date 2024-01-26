namespace Sprout.Exam.WebApp.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Birthdate { get; set; }
        public string Tin { get; set; }
        public int EmployeeTypeId { get; set; }
        public string EmployeeType { get; set; }
        public bool IsDeleted { get; set; }
    }

}