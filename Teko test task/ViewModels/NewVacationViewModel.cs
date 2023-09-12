using Teko_test_task.Models;

namespace Teko_test_task.ViewModels
{
    public class NewVacationViewModel
    {
        public string? FIO { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public List<Employee>? Employees { get; set; }
    }
}
