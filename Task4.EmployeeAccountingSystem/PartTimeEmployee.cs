using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.EmployeeAccountingSystem
{
	/// <summary>
	/// Сотрудник с зарплата рассчитываемой по часам.
	/// </summary>
	internal class PartTimeEmployee : Employee
	{
		#region  Поля и свойства.

		/// <summary>
		/// Сколько отработал сотрудник.
		/// </summary>
		private float HoursWorked;
		#endregion
		#region Базовый класс

		public override decimal CalculateSalary()
		{
			Console.Write("Сколько отработал сотрудник?: ");
			if (!float.TryParse(Console.ReadLine(), out HoursWorked) || (HoursWorked < 0 || float.IsNaN(HoursWorked)))
				throw new ArgumentException("Как будто он не работал");
			else
				return (BaseSalary / 176) * (decimal)HoursWorked;
		}
		#endregion
	}
}
