using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.EmployeeAccountingSystem
{
	/// <summary>
	/// Менеджер который управления данными сотрудников.
	/// </summary>
	/// <typeparam name="T">Сотрудник - Employee.</typeparam>
	internal interface IEmployeeManager<T> where T : Employee
	{

		/// <summary>
		/// Добавить сотрудника
		/// </summary>
		/// <param name="employee">Сотрудник.</param>
		void Add(T employee);

		/// <summary>
		/// Пойск, сотрудника по имени.
		/// </summary>
		/// <param name="name">Имя, искомого сотрудника.</param>
		/// <returns>Сотрудника.</returns>
		T Get(string name);

		/// <summary>
		/// Метод для изменения данных сотрудника. 
		/// </summary>
		/// <param name="employee">Сотрудник.</param>
		void Update(T employee);
	}
}
