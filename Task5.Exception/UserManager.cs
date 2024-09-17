using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task5.Exception.MyException;

namespace Task5.Exception
{
	internal class UserManager
	{
		#region Поля и свойства

		/// <summary>
		/// Список пользователей.
		/// </summary>
		private List<User> users;
		#endregion

		#region Методы

		/// <summary>
		/// Добавляет пользователя в список.
		/// </summary>
		/// <param name="user">Добавляемый пользователь.</param>
		public void AddUser(User user)
		{
			if (user == null)
				throw new ArgumentNullException("У переданного пользователь нет данных");

			if (!users.Contains(user))
			{
				foreach(var usery in users)
				{
					if(usery.Id == user.Id)
						throw new UserAlreadyExistsException("Пользователь уже существует");
				}
				users.Add(user);
				Console.WriteLine("Пользователь добавлен успешно!\n__________________");
			}
			else
				throw new UserAlreadyExistsException("Пользователь уже существует");
		}

		/// <summary>
		/// Удаляет пользователя из списка по Id.
		/// </summary>
		/// <param name="id">Id пользователя которого нужно удалить из списка.</param>
		public void RemoveUser(int id) 
		{
			if (id <= 0)
				throw new ArgumentNullException("Такого id не существует");
			foreach (var usery in users)
			{
				if (usery.Id == id)
				{
					users.Remove(usery);
					Console.WriteLine("Пользователь удален успешно!\n__________________");
					return;
				}
			}
			throw new UserNotFoundException("Пользователь не существует");
		}

		/// <summary>
		/// Возвращает пользователя по Id.
		/// </summary>
		/// <param name="id">Id пользователя которого нужно найти.</param>
		/// <returns>Найденный пользователь.</returns>
		public User GetUser(int id)
		{
			if (id <= 0)
				throw new ArgumentNullException("Такого id не существует");
			foreach (var usery in users)
			{
				if (usery.Id == id)
				{
					Console.WriteLine("Пользователь возвращен из списка успешно!\n__________________");
					return usery;
				}
			}
			throw new UserNotFoundException("Пользователь не существует");
		}

		/// <summary>
		/// Вывести всех пользователей в консоль.
		/// </summary>
		public void ListUsers()
		{
			if (!users.Any())
				throw new ArgumentNullException("Список пользователей пуст!");
			foreach (var usery in users)
			{
				Console.WriteLine($"ID: {usery.Id}\nName: {usery.Name}\nEmail: {usery.Email}\n__________________");
			}
		}
		#endregion

		#region Конструкторы

		/// <summary>
		/// Создать, менеджера для работ с пользователями.  
		/// </summary>
		public UserManager() 
		{
			users = new List<User>();
		}
		#endregion
	}
}
