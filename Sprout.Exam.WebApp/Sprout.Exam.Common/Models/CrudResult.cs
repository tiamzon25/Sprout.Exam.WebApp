namespace Sprout.Exam.Common.Models
{
    public class CrudResult<T> where T : class
    {
        public int Count { get; set; }
        public T Entity { get; set; }
    }

}
