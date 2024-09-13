using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.EmployeeAccountingSystem
{
	/// <summary>
	/// Сотрудник с фиксированной зарплатой.
	/// </summary>
	internal class FullTimeEmployee : Employee
	{
		#region Базовый класс

		public override decimal CalculateSalary()
		{
			return BaseSalary;
		}
		#endregion
	}
}
