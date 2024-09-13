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
				if (value <= 0)
					throw new ArgumentException("Никто не будет работать с отрицательной зарплатой");
				else
					this.baseSakary = value;
			}
		}
		#endregion
		#region Методы

		/// <summary>
		/// Абстрактный метод CalculateSalary(), который будет возвращать зарплату сотрудника.
		/// </summary>
		/// <returns>Зарплата сотрудника.</returns>
		public abstract decimal CalculateSalary();
		#endregion
		#region Конструктор

		/// <summary>
		/// Создает сотрудника для чего запрашивает имя и его заработную плату.
		/// </summary>
		public Employee()
		{
			Console.Write("Введите имя сотрудника: ");
			Name = Console.ReadLine();
			Console.Write("Введите месячную зарплату у сотрудника: ");
			if (decimal.TryParse(Console.ReadLine(), out decimal Salary))
				BaseSalary = Salary;
			else
				throw new ArgumentException("Вы ввели нечисло");
		}
		#endregion
	}
}
