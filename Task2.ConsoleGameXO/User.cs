using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.ConsoleGameXO
{
  /// <summary>
  /// Класс участника игры.
  /// </summary>
  /// <param name = "thisIsAMan"> True, если экземпляр человек, иначе Бот. </param>
  /// <param name = "Chars"> Записывается символ каким играет экземпляр. </param>
  /// <param name = "heWalksFirst"> True, если экземпляр ходит пер, иначе нулики.</param>
  /// <param name= "Wining">True, если экземпляр стал победителям, иначе false.</param>
  /// <param name ="Name" > Поле с ником для экземпляра класса.</param>
  internal class User
  {
    public bool ThisIsAMan { get; set; }
    private Symbols howChars;
    public char Chars
    {
      get
      {
        return (char)this.howChars;
      }
      set
      {
        if (value == 'X')
        {
          this.howChars = Symbols.X;
        }
        else if (value == 'O')
        {
          this.howChars = Symbols.O;
        }
      }
    }
    public bool HeWalksFirst { get; set; }
    public bool Wining { get; set; }
    private string name;
    public string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        if (string.IsNullOrWhiteSpace(value))
        {
          this.name = value;
        }
      }
    }
    public User(string name = "Аноним")
    {
      Name = name;
    }
  }
}
