using System;
using Teko_test_task.Models;

namespace Teko_test_task.Services
{

    public interface IDataService
    {
        List<Employee> GetEmployees();
        List<Vacation> GetVacations();
        void AddEmployee(Employee employee);
        void AddVacation(Vacation vacation);
    }

    public class DataService : IDataService
    {
        private List<Employee> employees = new List<Employee>();
        private List<Vacation> vacations = new List<Vacation>();

        public List<Employee> GetEmployees()
        {
            return employees;
        }

        public List<Vacation> GetVacations()
        {
            return vacations;
        }

        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
        }

        public void AddVacation(Vacation vacation)
        {
            vacations.Add(vacation);
        }
    }


}
