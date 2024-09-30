using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Exception
{
	internal class User
	{
		#region Поля и свойства

		private int id;

		/// <summary>
		/// Id.
		/// </summary>
		public int Id 
		{ 
			get { return this.id; } 
			set 
			{ 
				if(value <= 0)
					throw new ArgumentNullException("Id меньше или равно нулю");
			  else
					this.id = value;
			} 
		}
		
		private string name;

		/// <summary>
		/// Имя.
		/// </summary>
		public string Name {  get { return this.name; }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("Id меньше или равно нулю");
				else
					this.name = value;
			}
		}

		private string email;

		/// <summary>
		/// Почтовый адрес.
		/// </summary>
		public string Email {  get { return this.email; }
			set
			{
				if (string.IsNullOrEmpty(value))
					throw new ArgumentNullException("Id меньше или равно нулю");
				else
					this.email = value;
			}
		}
		#endregion

		#region Конструкторы

		/// <summary>
		/// Создать, пользователя.
		/// </summary>
		/// <param name="Id">Под каким ID будет создан пользователь.</param>
		/// <param name="Name">Имя пользователя.</param>
		/// <param name="Email">Почовый адресс пользователя.</param>
		public User(int Id, string Name, string Email) 
		{
			this.Id = Id;
			this.name = Name;
			this.email = Email;
		}
		#endregion
	}
}
