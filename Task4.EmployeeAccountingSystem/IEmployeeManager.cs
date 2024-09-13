using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.EmployeeAccountingSystem
{
	/// <summary>
	/// Интерфейс, для управления данными сотрудник.
	/// </summary>
	/// <typeparam name="T">Сотрудник - Employee.</typeparam>
	internal interface IEmployeeManager<T> 
	{

		/// <summary>
		/// Добавить сотрудника
		/// </summary>
		/// <param name="employee">Сотрудник, наследник класса Employee.</param>
		void Add(T employee);

		/// <summary>
		/// Пойск, сотрудника по имени.
		/// </summary>
		/// <param name="name">Имя, искомого сотрудника.</param>
		/// <returns>Сотрудника, наследник класса Employee.</returns>
		T Get(string name);

		/// <summary>
		/// Метод для изменения данных сотрудника. 
		/// </summary>
		/// <param name="employee">Сотрудник.</param>
		void Update(T employee);
	}
}
