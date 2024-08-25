using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.PhoneBook
{
	/// <summary>
	/// Abonent - Класс Абонент представляет собой будуйщий экземпляр контанкта в телефонной книге
	/// </summary> 
	internal struct Abonent
  {
    private long number;
    private string name;
    private string numberStr;

    /// <summary>
    /// Номер телефна в числовом значении
    /// </summary>
    public long Number
    {
      get
      {
        if(this.number == 0)
        {
          throw new ArgumentException("Exception: Номер телефона не стандартный");
				}
        else
        {
          return this.number;
        }

      }
      set
      {
        if (value != null)
        {
          this.number = value;
					this.NumberStr = value.ToString();
				}
        else
        {
					this.number = -1;
					this.NumberStr = "Номер телефона не найден";
				}
      }
    }
    /// <summary>
    /// Имя абонента
    /// </summary>
    public string Name
    {
      get
      {
				if (this.name == null)
				{
					throw new ArgumentException("Exception: Номер телефона не стандартный");
				}
				else
				{
					return this.name;
				}
			}
      set
      {
        if (string.IsNullOrEmpty(value))
        {
          this.name = "Аноним";
        }
        else
        {
          this.name = value;
        }

      }
    }
    /// <summary>
    /// Номер абонента в строке
    /// </summary>
		public string NumberStr
		{
			get
			{
				return this.numberStr;
			}
			private set
			{
				if (value != null)
				{
          if (value.Length == 11 || value.Length == 6)
          {
            this.numberStr = PhoneType(this.number);
          }
          else
          {
            Console.WriteLine("Такого номер не существует");
          }
        }
				else
				{
					this.number = -1;
				}
			}
		}
    /// <summary>
    /// Проверка заполненности анкеты для пополнения списка контактов
    /// </summary>
    /// <returns>true, если процесс прошел без ошибок, иначе false</returns>
    public bool Any()
    {
      if ((Number != 0 && Number != -1) || !string.IsNullOrEmpty(Name))
      {
        return true;
      }
      else { return false; }
    }
    /// <summary>
    /// Метод для изменения номера телефона в вид как спраавочнике
    /// </summary>
    /// <param name="Number">Номер телефона в числовом занчении</param>
    /// <returns>Возвращает номер телефона в строков ввиде, как в справочнике</returns>
    private string PhoneType(long Number)
    {
      string NumberStr;

			if (Length(Number) == 11)
      {
        string s = Number.ToString();
        if (int.Parse(s[0].ToString()) == 8)
        {
					NumberStr = Number.ToString("# (###) ###-##-##");
				}
				else
				{
					NumberStr = Number.ToString("+# (###) ###-##-##");
				}
			}
      else if(Length(Number) == 6)
      {
				NumberStr = Number.ToString("##-##-##");
			}
      else
      {
        NumberStr = ($"Exception: Номер телефона не стандартный");
      }
      return NumberStr;
    }
		/// <summary>
		/// Определение длины номера телефона
		/// </summary>
		/// <param name="Number">Номер телефона в числовом занчении</param>
		/// <returns>Длиину номера телефоа</returns>
		private int Length(long Number)
    {
      int i = 0;
      do
      {
				Number /= 10;
        i++;
      } while (Number > 0);
      return i;
    }
  }
}
