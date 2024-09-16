using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.EmployeeAccountingSystem
{
	internal class Program
	{
		static void Main(string[] args)
		{
			try
			{			
				EmployeeManager manager = new EmployeeManager();
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
							if (decimal.TryParse(stringSalary, out decimal salary))
								var FullEmployee = new FullTimeEmployee(Name, salary);
							manager.Add(FullEmployee);
							break;
						case 2:
							var PartEmployee = new PartTimeEmployee();
							manager.Add(PartEmployee);
							break;
						case 3:
							Console.Write("Введите имя искомого сотрудника: ");
							var employee = manager.Get(Console.ReadLine());
							Console.Write($"Имя: {employee.Name}\nЗарплата: {employee.CalculateSalary()}\n");
							break;
						case 4:
							Console.Write("Введите имя искомого сотрудника: ");
							ref var employee = manager.Get(Console.ReadLine());
							if (employee == null)
								throw new ArgumentNullException("Был передан работник с незаполненными данными");
							manager.Update(Get(Console.ReadLine()));
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
				Console.ReadLine();
			}
			catch (Exception ex) 
			{
				Console.WriteLine(ex.Message);
			}
		}

	}
}
