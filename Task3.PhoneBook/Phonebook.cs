using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.Xml.Linq;

namespace Task3.PhoneBook
{
  internal class Phonebook
  {
    private static Phonebook instance;
    private List<Abonent> Contact;
    private string path;
		/// <summary>
		/// Метод для добавления абонента в телефонный справочник
		/// </summary>
		/// <param name="abonent">Экземпляр структуры абонент</param>
		/// <returns>true, если процесс прошел без ошибок, иначе false</returns>
		public bool Add(Abonent abonent)
    {
      if (this.Contact.Any())
      {
        for(int i = 0; i < this.Contact.Count; i++)
        {
          if (this.Contact[i].Name == abonent.Name || this.Contact[i].Number == abonent.Number)
          {
						Console.WriteLine($"Контакт уже есть в записной книжке\nМожет вы ищете: {Contact[i].Name} c номером: {Contact[i].Number.ToString("+# (###) ###-##-##")}");
						return false;
          }
          else
          {
						this.Contact.Add(abonent);
            return true;
          }
        }
      }
      else
      {
        return false;
      }
			return false;
		}
		/// <summary>
		/// Метод для удаления абонента из телефонного справочника
		/// </summary>
		/// <param name="abonent">Экземпляр структуры абонент</param>
		/// <returns></returns>
		public bool Delete(Abonent abonent)
    {
			if (this.Contact.Any())
			{
				for (int i = 0; i < this.Contact.Count; i++)
				{
					if (this.Contact[i].Name == abonent.Name || this.Contact[i].Number == abonent.Number)
					{
						this.Contact.RemoveAt(i);
						return true;
					}
					else
					{
            Console.WriteLine("Контакт не был найден");
						return false;
					}
				}
			}
			else
			{
				return false;
			}
			return false;
		}
		/// <summary>
		/// Метод для сохранения списка в фаил phonebook.txt
		/// </summary>
		/// <returns>true, если процесс прошел без ошибок, иначе false</returns>
		public bool Save()
    {
      if (File.Exists(this.path))
      {
        File.Delete(this.path);
        return SaveProcessing();
			}
      else
      {
				return SaveProcessing();
			}
    }
		/// <summary>
		/// Метод для записи в фаил коллекцию абонентов
		/// </summary>
		/// <returns>true, если процесс прошел без ошибок, иначе false</returns>
		private bool SaveProcessing()
    {
			if (this.Contact.Any())
			{
				string[] line = new string[this.Contact.Count - 1];
				for (int i = 0; i < this.Contact.Count; i++)
				{
					line[i + 1] = $"{this.Contact[i].Name}\t{this.Contact[i].Number}\n";
				}
				File.WriteAllLines(this.path, line);
				return true;
			}
			else
			{
				return false;
			}
		}		
		/// <summary>
		/// Пойск абонента
		/// </summary>
		/// <param name="name">Имя искомого абонента</param>
		/// <returns>true, если процесс прошел без ошибок, иначе false</returns>
		public bool SearchAbonent(string name)
    {
			if (this.Contact.Any())
			{
				for (int i = 0; i < this.Contact.Count; i++)
				{
					if (this.Contact[i].Name == name)
					{
						Console.WriteLine($"Контакт уже есть в записной книжке\nМожет вы ищете: {Contact[i].Name} c номером: {Contact[i].Number.ToString("+# (###) ###-##-##")}");
						return true;
					}
					else
					{
						Console.WriteLine($"Абонент не найден");
						return false;
					}
				}
			}
			else
			{
				return false;
			}
			return false;
		}
		/// <summary>
		/// Пойск абонента
		/// </summary>
		/// <param name="number">Номер искомого абонента</param>
		/// <returns>true, если процесс прошел без ошибок, иначе false</returns>
		public bool SearchAbonent(long number)
    {
			if (this.Contact.Any())
			{
				for (int i = 0; i < this.Contact.Count; i++)
				{
					if (this.Contact[i].Number == number)
					{
						Console.WriteLine($"Контакт уже есть в записной книжке\nМожет вы ищете: {Contact[i].Name} c номером: {Contact[i].Number.ToString("+# (###) ###-##-##")}");
						return true;
					}
					else
					{
						Console.WriteLine($"Абонент не найден");
						return false;
					}
				}
			}
			else
			{
				return false;
			}
			return false;
		}
		/// <summary>
		/// Открытие/создания телефонной книги
		/// </summary>
    private void OpenPhoneBook()
    {
      try
      {   
        List<Abonent> contact = new List<Abonent>();
        //Проверка на наличие файла добавить
        if(File.Exists(this.path))
        {
          ReadPhoneBook(ref contact);
				}
        else 
        {
          File.Create(this.path);
        }
      }
      catch(Exception e)
      {
        Console.WriteLine("Exception: " + e.Message);
      } 
    }
		/// <summary>
		/// Считывания коллекции абонентов из файла
		/// </summary>
    private void ReadPhoneBook()
    {
      string line;
			StreamReader srt = new StreamReader($"{this.path}");
			List<Abonent> contact = new List<Abonent>();
			line = srt.ReadLine();
			while (line != null)
			{
				Console.WriteLine(line);
				line = srt.ReadLine();
				string[] Contactick = line.Split('\t');
				long.TryParse(Contactick[1], out long Numbr);
				Abonent Abonentick = new Abonent();
        Abonentick.Number = Numbr;
        Abonentick.Name = Contactick[0];
				contact.Add(Abonentick);
			}
			Contact = contact;
			srt.Close();
			Console.ReadLine();
		}
		/// <summary>
		/// Конструктор для создания телефонной книги и запрос пути к файлу .exe
		/// </summary>
    public Phonebook()
    {        
      var exePath = AppDomain.CurrentDomain.BaseDirectory;//path to exe file
      this.path = Path.Combine(exePath, "\\phonebook.txt");
			this.Contact = new List<Abonent>();
			OpenPhoneBook();
    }
    
  }
}
