using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

namespace Task3.PhoneBook
{
  internal class Phonebook
  {
    private static Phonebook instance;
    private List<Abonent> Contact;
    public bool Add(Abonent abonent)
    {
      for(int i = 0; i < this.Contact.Count; i++)
      {
        if (this.Contact[i].Name == abonent.Name || this.Contact[i].Number == abonent.Number)
        {
          return false;
        }
        else
        {
          Contact.Add(abonent);
          return true;
        }
      }
      return false;
    }
    private void ReadPhoneBook()
    {
      string line;
      try
      {
        var exePath = AppDomain.CurrentDomain.BaseDirectory;//path to exe file
        var path = Path.Combine(exePath, "\\phonebook.txt");
        //Проверка на наличие файла добавить
        StreamReader srt = new StreamReader($"{path}");
        List<Abonent> contact = new List<Abonent>();
        line = srt.ReadLine();
        while ( line != null )
        {
          Console.WriteLine( line );
          line = srt.ReadLine();
          string[] Contactick = line.Split('\t');
          long.TryParse(Contactick[1], out long Numbr);
          Abonent Abonentick = new Abonent(Numbr, Contactick[0]);
          contact.Add(Abonentick);
        }
        Contact = contact;
        srt.Close();
        Console.ReadLine();
      }
      catch(Exception e)
      {
        Console.WriteLine("Exception: " + e.Message);
      } 
    }

    public Phonebook()
    {
      Contact = new List<Abonent>();
      ReadPhoneBook();
    }

  }
}
