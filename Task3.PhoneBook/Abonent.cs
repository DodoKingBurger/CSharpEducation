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
  internal class Abonent
  {
    private long number;
    private string name;
    public long Number
    {
      get 
      { if(number == 0|| number == null) 
          throw new ArgumentException("НЕ найден номер");
      else
          return this.number; 
      }
      set
      {
        if (value != null)
        {
          this.number = value;
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
          this.name = value;
      }
    }
    public Abonent(long number,string name = "Аноним")
    {
      Name = name;
      Number = number;
    }

  }
}
