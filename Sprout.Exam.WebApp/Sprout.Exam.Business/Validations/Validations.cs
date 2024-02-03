using System;

namespace Sprout.Exam.Business.Validations
{
    public class Validations : IValidations
    {
        public string Validate(string fullName, string Tin, DateTime birthDate)
        {
            var age = GetAgeAsync(birthDate);
            if (fullName.Length < 1)
            {
                return "Invalid Name";
            }
            else if (Tin.Length < 1)
            {
                return "Invalid Tin";
            }
            else if (age < 15)
            {
                return "Age Must be older than 15";
            }
            else
            {
                return null;
            }
        }
        private int GetAgeAsync(DateTime birtDate)
        {
            var today = DateTime.Today;

            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (birtDate.Year * 100 + birtDate.Month) * 100 + birtDate.Day;

            return (a - b) / 10000;
        }
    }
}
