using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task4.EmployeeAccountingSystem
{
	/// <summary>
	/// Интерфейс, для управления данными сотрудник
	/// </summary>
	internal class EmployeeManager : IEmployeeManager<Employee>
	{
		#region Поля и свойства

		/// <summary>
		/// Список сотрудников.
		/// </summary>
		private List<Employee> ListEmployees;
		#endregion
		#region Методы

		/// <summary>
		/// Вызов главного меню.
		/// </summary>
		/// <exception cref="ArgumentException">Запрос был даже не числом!</exception>
		public void MainMenu()
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
						var FullEmployee = new FullTimeEmployee();
						Add(FullEmployee);
						break;
					case 2:
						var PartEmployee = new PartTimeEmployee();
						Add(PartEmployee);
						break;
					case 3:
						Console.Write("Введите имя искомого сотрудника: ");
						var employee = Get(Console.ReadLine());
						Console.Write($"Имя: {employee.Name}\nЗарплата: {employee.CalculateSalary()}\n");
						break;
					case 4:
						Update(Get(Console.ReadLine()));
						break;
					case 5:
						return;
					default:
						Console.WriteLine("Таких команд не знаем делать ниче не будем ");
						break;
				}
				MainMenu();
			}
			else
				throw new ArgumentException("Запрос неясен");
			Console.ReadLine();
		}

		public void Add(Employee employee) 
		{
			if (employee == null)
				throw new ArgumentNullException("Был передан работник с незаполненными данными");
			else
			{
				if (ListEmployees.Any())
				{
					if (!ListEmployees.Contains(employee))
						ListEmployees.Add(employee);
					else
						throw new ArgumentException("Такой сотрудник уже есть в списке");
				}
				else
				{
					ListEmployees.Add(employee);
				}
			}
		}

		public Employee Get(string name)
		{

			foreach (var employee in ListEmployees)
			{
				if (employee.Name.ToLower() == name.ToLower())
					return employee;
			}
			throw new ArgumentException("Сотрудник не найден");
		}

		public void Update(Employee employee)
		{
			if (employee == null) 
				throw new ArgumentNullException("Был передан работник с незаполненными данными");
			if (ListEmployees.Any())
				throw new ArgumentNullException("Список пуст! Добавьте сотрудников");
			if (ListEmployees.Contains(employee))
			{
				foreach(var employeer  in ListEmployees)
				{
					if(employeer == employee)
					{
						Console.WriteLine(
						"Какие данные поменять\n" +
						"1. Изменить имя сотрудника\n" +
						"2. Изменить зарплату сотруднику\n" +
						"Выберите действие:");
						if (int.TryParse(Console.ReadLine(), out int request))
						{
							switch (request)
							{
								case 1:
									employee.Name = Console.ReadLine();
									break;
								case 2:
									if (decimal.TryParse(Console.ReadLine(), out decimal salary))
										employee.BaseSalary = salary;
									break;
								default:
									throw new ArgumentException("Такого выбора не существует");
							}
						}
						else
							throw new InvalidOperationException("Такой операции не существует");
					}
				}
			}
			else
				throw new ArgumentException("Сотрудник не найден");
		}
		#endregion
		#region Конструктор

		/// <summary>
		/// Менеджер, для работы с данными о сотрудниках и хранения их списка.
		/// </summary>
		public EmployeeManager()
		{
			ListEmployees = new List<Employee>();
		}
		#endregion
	}
}
