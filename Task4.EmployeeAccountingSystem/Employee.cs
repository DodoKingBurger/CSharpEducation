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
				else if (value == 0)
					throw new ArgumentNullException("Никто не будет работать без зарплатой");
				this.baseSakary = value;
			}
		}

		private int id;

		/// <summary>
		/// ID сотрудника в список.
		/// </summary>
		public int ID
		{
			get { return this.id; }
			set
			{
				if (value >= 0)
					this.id = value;
				else
					throw new ArgumentException("ID не может быть отрицательным");
			}
		}
		#endregion

		#region Методы

		/// <summary>
		/// Метод, который будет возвращать зарплату сотрудника.
		/// </summary>
		/// <returns></returns>
		public abstract decimal CalculateSalary();

		/// <summary>
		/// Создает копию, выполняя неглубокое копирование обьекта.
		/// </summary>
		/// <returns>Возвращает копию объекта.</returns>
		public object Clone()
		{
			return MemberwiseClone();
		}
		#endregion

		#region Конструктор

		/// <summary>
		/// Создает сотрудника для чего запрашивает имя и его заработную плату.
		/// </summary>
		/// <param name="name">Имя сотрудника.</param>
		/// <param name="salary">Месячная зарплата.</param>
		/// <param name="id">id под которым хотят записать сотрудника.</param>
		public Employee(string name, decimal salary)
		{
			Name = name;
			BaseSalary = salary;
		}
		#endregion
	}
}
