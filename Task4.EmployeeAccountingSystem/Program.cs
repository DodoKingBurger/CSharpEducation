using System;

namespace Task4.EmployeeAccountingSystem
{
  internal class Program
  {
    static void Main(string[] args)
    {
      EmployeeManager manager = new EmployeeManager();
      MainMenu(manager);
      Console.ReadLine();
    }
    /// <summary>
    /// Главное меню. 
    /// </summary>
    /// <param name="manager">Работник-Менеджер.</param>
    static void MainMenu(EmployeeManager manager)
    {
      try
      {
        Console.Write(
          "1. Добавить полного сотрудника\n" +
          "2. Добавить частичного сотрудника\n" +
          "3. Получить информацию о сотруднике\n" +
          "4. Обновить данные сотрудника\n" +
          "5. Выйти\n" +
          "Выберите действие: ");
        if (int.TryParse(Console.ReadLine(), out int request))
        {
          switch (request)
          {
            case 1:
              Console.Write("Введите имя сотрудника: ");
              string Name = Console.ReadLine();
              Console.Write("Введите месячную зарплату у сотрудника: ");
              string stringSalary = Console.ReadLine();
              FullTimeEmployee FullEmployee;
              if (decimal.TryParse(stringSalary, out decimal salary))
              {
                FullEmployee = new FullTimeEmployee(Name, salary);
                manager.Add(FullEmployee);
                Console.WriteLine("______________Успех_______________");
              }
              else
                throw new ArgumentException("Введено нечисло");

              break;
            case 2:
              Console.Write("Введите имя сотрудника: ");
              string NameP = Console.ReadLine();
              Console.Write("Введите месячную зарплату у сотрудника: ");
              string stringSalaryP = Console.ReadLine();
              PartTimeEmployee PartEmployee;
              if (decimal.TryParse(stringSalaryP, out decimal salaryF))
              {
                PartEmployee = new PartTimeEmployee(NameP, salaryF);
                manager.Add(PartEmployee);
                Console.WriteLine("______________Успех_______________");
              }
              else
                throw new ArgumentException("Введено нечисло");
              break;
            case 3:
              Console.Write("Введите имя искомого сотрудника: ");
              var employee = manager.Get(Console.ReadLine());
              Console.Write($"Имя: {employee.Name}\nЗарплата: {employee.CalculateSalary()}\n");
              Console.WriteLine("______________Успех_______________");
              break;
            case 4:
              Console.Write("Введите имя искомого сотрудника: ");
              Employee employeeGet = (Employee)manager.Get(Console.ReadLine()).Clone();
              if (employeeGet == null)
                throw new ArgumentNullException("Был передан работник с незаполненными данными");
              Console.Write(
              "Какие данные поменять\n" +
              "1. Изменить имя сотрудника\n" +
              "2. Изменить зарплату сотруднику\n" +
              "Выберите действие: ");
              if (int.TryParse(Console.ReadLine(), out int requesting))
              {
                switch (requesting)
                {
                  case 1:
                    Console.Write("Введите имя: ");
                    string nameUp = Console.ReadLine();
                    if (string.IsNullOrEmpty(nameUp))
                      throw new ArgumentException("Новое имя пустое");
                    else
                      employeeGet.Name = nameUp;
                    break;
                  case 2:
                    Console.Write("Введите зарлату: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal salaryUp))
                      employeeGet.BaseSalary = salaryUp;
                    else
                      throw new ArgumentException("Новая зарплата нечисло");
                    break;
                  default:
                    throw new ArgumentException("Такого выбора не существует");
                }
              }
              else
                throw new InvalidOperationException("Такой операции не существует");
              manager.Update(employeeGet);
              Console.WriteLine("______________Успех_______________");
              break;
            case 5:
              return;
            default:
              Console.WriteLine("Таких команд не знаем делать ниче не будем ");
              break;
          }
        }
        else
          throw new ArgumentException("Запрос неясен");
        MainMenu(manager);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }
}
