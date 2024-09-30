using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Task4.EmployeeAccountingSystem
{
	/// <summary>
	/// Менеджер.
	/// </summary>
	internal class EmployeeManager : IEmployeeManager<Employee>
	{
		#region Поля и свойства

		/// <summary>
		/// Список сотрудников.
		/// </summary>
		private Dictionary<int,Employee> listEmployees;

		/// <summary>
		/// ID под которым можно записать сотрудника в список.
		/// </summary>
		private int ID = 0;
		#endregion

		#region Методы

		public void Add(Employee employee) 
		{
			if (employee == null)
				throw new ArgumentNullException("Был передан работник с незаполненными данными");
			else
			{
				if (this.listEmployees.Any())
				{
					if (!this.listEmployees.ContainsValue(employee))
					{
						this.listEmployees.Add(this.ID,employee);
						employee.ID = this.ID;
						this.ID++;
					}
					else
						throw new ArgumentException("Такой сотрудник уже есть в списке");
				}
				else
				{
					this.listEmployees.Add(this.ID, employee);
					employee.ID = this.ID;
					this.ID++;
				}
			}
		}

		public Employee Get(string name)
		{
			if (this.listEmployees.Any())
			{
				foreach (var employee in this.listEmployees)
				{
					if (employee.Value.Name.ToLower() == name.ToLower())
						return employee.Value;
				}
				throw new ArgumentException("Сотрудник не найден");
			}
			else
				throw new ArgumentNullException("Список пуст");
		}

		public void Update(Employee employeeUp)
		{
			if (this.listEmployees.Any())
			{
				foreach(var employeer  in this.listEmployees)
				{
					if(employeer.Key == employeeUp.ID)
					{
						this.listEmployees[employeer.Value.ID] = employeeUp;
						return;
					}
				}
			}
			else
				throw new ArgumentNullException("Список пуст");
		}
		#endregion

		#region Конструктор

		/// <summary>
		/// Менеджер, для работы с данными о сотрудниках и хранения их списка.
		/// </summary>
		public EmployeeManager()
		{
			this.listEmployees = new Dictionary<int, Employee>();
		}
		#endregion
	}
}
