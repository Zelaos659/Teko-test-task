namespace Teko_test_task.Models
{
    public class Vacation
    {
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public Employee? Employee { get; set; }
    }
}
