using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.EmployeeAccountingSystem
{
	/// <summary>
	/// Сотрудник.
	/// </summary>
	internal abstract class Employee
	{
		#region  Поля и свойства

		private string name;

		/// <summary>
		/// Имя сотрудника.
		/// </summary>
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentException("Имя не задано, какой то у вас плохой учет сотрудников");
				else
					this.name = value;
			}
		}

		private decimal baseSakary;

		/// <summary>
		/// Базовая зарплата.
		/// </summary>
		public decimal BaseSalary
		{
			get
			{
				return this.baseSakary;
			}
			set
			{
				if (value < 0)
					throw new ArgumentException("Никто не будет работать с отрицательной зарплатой");
				else if(value == 0) 
					throw new ArgumentNullException("Никто не будет работать без зарплатой");
					this.baseSakary = value;
			}
		}
		#endregion
		#region Методы

		public abstract decimal CalculateSalary();
		#endregion
		#region Конструктор

		/// <summary>
		/// Создает сотрудника для чего запрашивает имя и его заработную плату.
		/// </summary>
		public Employee(string name, decimal salary)
		{
			if (!string.IsNullOrEmpty(name))
				this.name = name;
			else
				throw new ArgumentNullException("Вы ввели некоректное имя");

			if (salary != null)
				BaseSalary = salary;
			else
				throw new ArgumentNullException("Вы ввели нечисло");
		}
		#endregion
	}
}
