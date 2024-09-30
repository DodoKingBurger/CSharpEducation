using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Exception.MyException
{
	internal class UserNotFoundException : System.Exception
	{
		/// <summary>
		/// Это исключение выбрасывается, если пользователь не был найден.
		/// </summary>
		/// <param name="message">Сообщение об ошибке с объяснением причины исключения.</param>
		public UserNotFoundException(string message)
	: base(message) { }
	}
}
