using Teko_test_task.Models;

namespace Teko_test_task.ViewModels
{
    public class VacationSchedule
    {
        public List<Employee>? Employees { get; set; }
        public List<Vacation>? Vacations { get; set; }
        public List<Vacation>? DepartmentUnder30 { get; set; }
        public List<Vacation>? WomenOver30Under50 { get; set; }
        public List<Vacation>? EmployeesOver50 { get; set; }
        public List<Vacation>? NoOverlap { get; set; }
        public Vacation? vacation { get; set; }
    }
}
