using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Exception
{
  internal class Program
  {
    static void Main(string[] args)
    {
      UserManager userManager = new UserManager();
      MainMenu(userManager);
    }
    /// <summary>
    /// Вызов главного меню.
    /// </summary>
    /// <exception cref="ArgumentException">Запрос был даже не числом!</exception>
    static public void MainMenu(UserManager userManager)
    {
      Console.Write(
        "1. Добавить пользователя\n" +
        "2. Удаление пользователя\n" +
        "3. Найти пользователя\n" +
        "4. Получить информацию о всех пользователях\n" +
        "5. Выйти\n" +
        "Выберите действие: ");
      if (int.TryParse(Console.ReadLine(), out int request))
      {
        switch (request)
        {
          case 1:
            Console.Write("Введите Id: ");
            int.TryParse(Console.ReadLine(), out int IDAdd);
            Console.Write("Введите Имя: ");
            string Name = Console.ReadLine();
            Console.Write("Введите почту: ");
            string Email = Console.ReadLine();
            var user = new User(IDAdd, Name, Email);
            userManager.AddUser(user);
            Console.WriteLine("Пользователь добавлен успешно!\n__________________");
            break;
          case 2:
            Console.Write("Введите Id: ");
            int.TryParse(Console.ReadLine(), out int IDRemove);
            userManager.RemoveUser(IDRemove);
            Console.WriteLine("Пользователь удален успешно!\n__________________");
            break;
          case 3:
            Console.Write("Введите ID искомого сотрудника: ");
            int.TryParse(Console.ReadLine(), out int IDGet);
            var userGet = userManager.GetUser(IDGet);
            Console.WriteLine("Пользователь возвращен из списка успешно!\n__________________");
            Console.WriteLine($"ID: {userGet.Id}\nName: {userGet.Name}\nEmail: {userGet.Email}\n__________________");
            break;
          case 4:
            userManager.ListUsers();
            break;
          case 5:
            return;
          default:
            Console.WriteLine("Таких команд не знаем делать ниче не буду\n__________________");
            break;
        }
        MainMenu(userManager);
      }
      else
        throw new ArgumentException("Запрос неясен");
      Console.ReadLine();
    }
  }
}
