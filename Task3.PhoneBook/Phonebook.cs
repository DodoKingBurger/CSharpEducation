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
    public long Number
    {
      get
      {
        return number;
      }
      set
      {
        if (value != null)
        {
          //if(lengh(value) == 11 || lengh(value) == 6 || lengh(value) == 12)
          //{
            this.number = value;
          //}
          //else
          //{
          //  Console.WriteLine("Такого номер не существует");
          //}
        }
        else
        {
					this.number = -1;
				}
      }
    }
    public string Name
    {
      get
      {
        return this.name;
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
    //private int lengh(long value)
    //{
    //  int i = 0;
    //  do
    //  {
    //    value /= 10;
    //    i++;
    //  }while (value>0);
    //  return i;
    //}
  }
}
