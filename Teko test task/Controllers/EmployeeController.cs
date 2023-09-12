using Microsoft.AspNetCore.Mvc;
using Teko_test_task.Models;
using Teko_test_task.Services;
using Teko_test_task.ViewModels;

namespace Teko_test_task.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IDataService _dataService;
        public EmployeeController(IDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public IActionResult Index()
        {

            var viewModel = new EmployeeDetails
            {
                Employees = _dataService.GetEmployees()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GenerateEmployees()
        {
            Random random = new Random();

            for (int i = 0; i < 100; i++)
            {
                Gender gender = (Gender)random.Next(2);
                var employee = new Employee
                {
                    FIO = GetFIO(gender),
                    Age = random.Next(20, 60),
                    Gender = gender,
                    Position = (Position)random.Next(10),
                };

                _dataService.AddEmployee(employee);
            }

            return RedirectToAction("Index");
        }

        private string GetFIO(Gender gender)
        {
            Random random = new Random();

            string FIO;
            string[] LastNames = { "Иванов", "Петров", "Сидоров", "Козлов", "Смирнов", "Волков", "Кузнецов", "Павлов", "Морозов", "Соловьёв" };

            if (gender == Gender.Мужчина)
            {
                string[] MaleFirstNames = { "Иван", "Петр", "Александр", "Михаил", "Андрей", "Никита", "Сергей", "Кирилл", "Олег", "Антон" };
                string[] MaleMiddleNames = { "Иванович", "Петрович", "Александрович", "Михайлович", "Андреевич", "Васильевич", "Павлович", "Алексеевич" };

                FIO = $"{LastNames[random.Next(LastNames.Length)]} " +
                      $"{MaleFirstNames[random.Next(MaleFirstNames.Length)]} " +
                      $"{MaleMiddleNames[random.Next(MaleMiddleNames.Length)]}";
            }
            else
            {
                string[] FemaleFirstNames = { "Елена", "Ольга", "Анна", "Мария", "Наталья", "Екатерина", "Дарья", "Антонина" };
                string[] FemaleMiddleNames = { "Ивановна", "Петровна", "Александровна", "Михайловна", "Андреевна", "Николаевна", "Тимофеевна", "Матвеевна" };

                FIO = $"{LastNames[random.Next(LastNames.Length)]}а " +
                      $"{FemaleFirstNames[random.Next(FemaleFirstNames.Length)]} " +
                      $"{FemaleMiddleNames[random.Next(FemaleMiddleNames.Length)]}";

            }
            return FIO;
        }

    }
}
