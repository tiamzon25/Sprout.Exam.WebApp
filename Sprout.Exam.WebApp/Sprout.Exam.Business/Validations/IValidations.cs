using System;

namespace Sprout.Exam.Business.Validations
{
    public interface IValidations
    {
        public string Validate(string fullName, string Tin, DateTime birthDate);
    }
}
