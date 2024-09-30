using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Task5.Exception
{
	internal class UserAlreadyExistsException : System.Exception
	{
		/// <summary>
		/// Это исключение выбрасывается, если пользователь уже существует в списке сотрудников.
		/// </summary>
		/// <param name="message">Сообщение об ошибке с объяснением причины исключения.</param>
		public UserAlreadyExistsException(string message)
			: base(message) { }
	}
}
