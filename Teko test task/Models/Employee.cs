using System.ComponentModel;

namespace Teko_test_task.Models
{
    public class Employee
    {
        public string? FIO { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public Position Position { get; set; }
    }

    public enum Gender
    {
        Мужчина,
        Женщина
    }

    public enum Position
    {
        Менеджер,
        Разработчик,
        Тестировщик,
        Директор,
        Администратор,
        Продавец,
        Дизайнер,
        Стажёр,
        Маркетолог,
        Инженер
    }
}
