using Microsoft.AspNetCore.Mvc;
using System;
using Teko_test_task.Models;
using Teko_test_task.Services;
using Teko_test_task.ViewModels;

namespace Teko_test_task.Controllers
{
    public class VacationController : Controller
    {
        private readonly IDataService _dataService;
        public VacationController(IDataService dataService) 
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IActionResult Index(string FIO)
        { 

            var viewModel = new VacationSchedule() 
            {
                Employees = _dataService.GetEmployees(),
                Vacations = _dataService.GetVacations()
            };

            var employee = viewModel.Employees.FirstOrDefault(e => e.FIO == FIO);

            if (employee != null)
            {
                var overlappingVacations = VacationWithOverlap(employee);
                var lastVacation = _dataService.GetVacations().Where(v => v.Employee == employee).ToList().Last();

                viewModel.vacation = lastVacation;

                viewModel.DepartmentUnder30 = overlappingVacations
                    .Where(vacation => vacation.Employee.Age < 30 && 
                                       vacation.Employee.Position == employee.Position &&
                                       vacation.Employee != employee)
                    .ToList();

                viewModel.WomenOver30Under50 = overlappingVacations
                    .Where(vacation => vacation.Employee.Age > 30 && 
                                       vacation.Employee.Age < 50 && 
                                       vacation.Employee.Gender == Gender.Женщина &&
                                       vacation.Employee.Position != employee.Position &&
                                       vacation.Employee != employee)
                    .ToList();

                viewModel.EmployeesOver50 = overlappingVacations
                    .Where(vacation => vacation.Employee.Age > 50 &&
                                       vacation.Employee != employee)
                    .ToList();

                viewModel.NoOverlap = VacationWithoutOverlap(employee);
            }

            return View(viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> GenerateVacation()
        {
            Random random = new Random();
            var vacationDuration = new int[] { 14, 7, 7 };

            var employees = _dataService.GetEmployees();
            foreach (var employee in employees)
            {
                for (int i = 0; i < 3; i++)
                {
                    var startDate = RandomDay(i);

                    var vacation = new Vacation
                    {
                        StartDate = startDate,
                        EndDate = startDate.AddDays(vacationDuration[i]),
                        Employee = employee
                    };
                    _dataService.AddVacation(vacation);
                }

            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult NewVacation()
        {

            var viewModel = new NewVacationViewModel()
            {
                Employees = _dataService.GetEmployees()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult NewVacation(NewVacationViewModel model)
        {
            model.Employees = _dataService.GetEmployees();

            var employee = _dataService.GetEmployees().FirstOrDefault(emp => emp.FIO == model.FIO);

            if (employee != null)
            {
                var vacation = new Vacation
                {
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Employee = employee
                };

                _dataService.AddVacation(vacation);
            }
            else
            {
                ModelState.AddModelError("", "Сотрудник не найден");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        private DateOnly RandomDay(int month)
        {
            Random random = new Random();

            switch (month)
            {
                case 0:
                    return DateOnly.FromDateTime(new DateTime(2023, random.Next(1, 5), random.Next(1, 28)));
                case 1:
                    return DateOnly.FromDateTime(new DateTime(2023, random.Next(5, 9), random.Next(1, 29)));
                case 2:
                    return DateOnly.FromDateTime(new DateTime(2023, random.Next(9, 12), random.Next(1, 29)));
                default:
                    return DateOnly.FromDateTime(DateTime.Today);
            }
        }

        private List<Vacation> VacationWithOverlap(Employee employee)
        {

            var vacation = _dataService.GetVacations();

            var employeeVacation = vacation.Where(v => v.Employee == employee).ToList().Last();

            var newList = new List<Vacation>();

            foreach (var item in vacation)
            {
                if (employeeVacation.StartDate <= item.EndDate && employeeVacation.EndDate >= item.StartDate)
                {
                    newList.Add(item);
                }
            }
            return newList;
        }

        private List<Vacation> VacationWithoutOverlap(Employee employee)
        {
            var vacation = _dataService.GetVacations();

            var employeeVacation = vacation.Where(v => v.Employee == employee).ToList().Last();

            var newList = new List<Vacation>();

            foreach (var item in vacation)
            {
                if (!(employeeVacation.StartDate <= item.EndDate && employeeVacation.EndDate >= item.StartDate))
                {
                    newList.Add(item);
                }
            }
            return newList;
        }
    }
}
