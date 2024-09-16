using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task4.EmployeeAccountingSystem
{
	/// <summary>
	/// Сотрудник.
	/// </summary>
	internal class EmployeeManager : IEmployeeManager<Employee>
	{
		#region Поля и свойства

		/// <summary>
		/// Список сотрудников.
		/// </summary>
		private List<Employee> listEmployees;
		#endregion
		#region Методы

		public void Add(Employee employee) 
		{
			if (employee == null)
				throw new ArgumentNullException("Был передан работник с незаполненными данными");
			else
			{
				if (listEmployees.Any())
				{
					if (!listEmployees.Contains(employee))
						listEmployees.Add(employee);
					else
						throw new ArgumentException("Такой сотрудник уже есть в списке");
				}
				else
				{
					listEmployees.Add(employee);
				}
			}
		}

		public Employee Get(string name)
		{

			foreach (var employee in listEmployees)
			{
				if (employee.Name.ToLower() == name.ToLower())
					return employee;
			}
			throw new ArgumentException("Сотрудник не найден");
		}

		public void Update(Employee employee)
		{
			if (listEmployees.Contains(employee))
			{
				foreach(var employeer  in ListEmployees)
				{
					if(employeer == employee)
					{
						Console.WriteLine(
						"Какие данные поменять\n" +
						"1. Изменить имя сотрудника\n" +
						"2. Изменить зарплату сотруднику\n" +
						"Выберите действие: ");
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
