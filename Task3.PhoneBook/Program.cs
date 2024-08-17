using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.PhoneBook
{
  internal class Program
  {
    static void Main(string[] args)
    {
      Console.OutputEncoding = Encoding.GetEncoding(1251);
      Phonebook phoneBook = new Phonebook();
      Console.WriteLine(phoneBook.Contact);
    }
  }
}
