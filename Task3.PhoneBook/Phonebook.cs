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
using System.Net.Cache;

namespace Task3.PhoneBook
{
  internal class Phonebook
  {
    private static Phonebook instance;
    #region Поля и свойства

/// <summary>
/// Список Аббонетов.
/// </summary>
    private List<Abonent> Contact;
/// <summary>
/// Путь к файлу со списком аббонентов.
/// </summary>
    private string path;
    #endregion
    #region Методы

    /// <summary>
    /// Метод для добавления абонента в телефонный справочник.
    /// </summary>
    /// <param name="abonent">Экземпляр структуры абонент.</param>
    /// <returns>true, если процесс прошел без ошибок, иначе false.</returns>
    public bool Add(Abonent abonent)
    {
      for (int i = 0; i < this.Contact.Count; i++)
      {
        if (this.Contact[i].Name == abonent.Name || this.Contact[i].Number == abonent.Number)
        {
          Console.WriteLine($"Контакт уже есть в записной книжке\nМожет вы ищете: {Contact[i].Name} c номером: {Contact[i].Number.ToString("+# (###) ###-##-##")}");
          return false;
        }
      }
      this.Contact.Add(abonent);
      return true;
    }
    /// <summary>
    /// Метод для удаления абонента из телефонного справочника.
    /// </summary>
    /// <param name="abonent">Экземпляр структуры абонент.</param>
    /// <returns>true, если процесс прошел без ошибок, иначе false.</returns>
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
				}            
        Console.WriteLine("Контакт не был найден");
			  return false;
			}
			else
			{
        Console.WriteLine("Списко контактов пуст");
        return false;
			}
    }
/// <summary>
/// Метод для изменения данный переданного абонента.
/// </summary>
/// <param name="abonent">Переданный абонент.</param>
    public void Update(Abonent abonent)
		{
      Console.WriteLine("Изменить Имя - 1\nИзменить номер телефона - 2\n");
      string request = Console.ReadLine();
      if (int.TryParse(request, out int number) && (number == 1 || number == 2))
      {
        Console.Write("Введите новые данные - ");
				string Data = Console.ReadLine();
				if (!string.IsNullOrEmpty(Data))
				{
					if(number == 1)
					{
						for(int i = 0; i < this.Contact.Count; i++)
						{
							if (Contact[i].Name == abonent.Name)
							{
                abonent.Name = Data;
								Contact[i] = abonent;
                Console.WriteLine("Данные успешно изменены");
              }
						}
					}
					else
					{
            for (int i = 0; i < this.Contact.Count; i++)
            {
              if (Contact[i].Number == abonent.Number)
              {
								if(long.TryParse(Data,out long numberAbonent))
								{
									abonent.Number = numberAbonent;
									Contact[i] = abonent;
                  Console.WriteLine("Данные успешно изменены");
                  
                }
								else
								{
									Console.WriteLine("Новые данные не являются номером");
                  Update(abonent);
                }
              }
            }
          }
				}
				else
				{
					Console.WriteLine("Запрос пуст");
					Update(abonent);
        }
      }
      else
      {
        Console.WriteLine("Неизвестная операция! Возвращаемся в\n");
        Update(abonent);
      }
    }
/// <summary>
/// Метод для сохранения списка в фаил phonebook.txt.
/// </summary>
/// <returns>true, если процесс прошел без ошибок, иначе false.</returns>
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
/// Пойск абонента
/// </summary>
/// <param name="name">Имя искомого абонента.</param>
/// <returns>true, если процесс прошел без ошибок, иначе false.</returns>
		public bool SearchAbonent(string name, out Abonent abonent)
    {
      abonent = new Abonent();
      if (this.Contact.Any())
			{
				for (int i = 0; i < this.Contact.Count; i++)
				{
					if (this.Contact[i].Name == name)
					{
						Console.WriteLine($"Контакт уже есть в записной книжке\nМожет вы ищете: {Contact[i].Name} c номером: {Contact[i].NumberStr}");
            abonent = this.Contact[i];
            return true;
					}

				}						
        Console.WriteLine($"Абонент не найден");
				return false;
			}
			else
			{
        Console.WriteLine("Список контактов пуст");
				return false;
			}
		}
/// <summary>
/// Пойск абонента.
/// </summary>
/// <param name="number">Номер искомого абонента.</param>
/// <returns>true, если процесс прошел без ошибок, иначе false.</returns>
		public bool SearchAbonent(long number, out Abonent abonent)
    {
      abonent = new Abonent();
      if (this.Contact.Any())
			{
				for (int i = 0; i < this.Contact.Count; i++)
				{
					if (this.Contact[i].Number == number)
					{
						Console.WriteLine($"Контакт уже есть в записной книжке\nМожет вы ищете: {Contact[i].Name} c номером: {Contact[i].NumberStr}");
            abonent = this.Contact[i];
            return true;
					}
				}
        Console.WriteLine($"Абонент не найден");
        return false;
      }
			else
			{
        Console.WriteLine("Список контактов пуст");
        return false;
			}
		}
/// <summary>
/// Открытие/создания телефонной книги.
/// </summary>
    private void OpenPhoneBook()
    {
      try
      {   
        List<Abonent> contact = new List<Abonent>();
        //Проверка на наличие файла добавить
        if(File.Exists(this.path))
        {
          ReadPhoneBook();
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
/// Считывания коллекции абонентов из файла.
/// </summary>
    private void ReadPhoneBook()
    {
      string line;
			StreamReader srt = new StreamReader($"{this.path}");
			List<Abonent> contact = new List<Abonent>();
			line = srt.ReadLine();
			while (line != null && line !="")
			{
				Console.WriteLine(line);
				string[] Contactick = line.Split('\t');
				long.TryParse(Contactick[1], out long Numbr);
				Abonent Abonentick = new Abonent();
        Abonentick.Number = Numbr;
        Abonentick.Name = Contactick[0];
				contact.Add(Abonentick);				
        line = srt.ReadLine();
			}
			Contact = contact;
			srt.Close();
		}
/// <summary>
/// Метод для записи в фаил коллекцию абонентов.
/// </summary>
/// <returns>true, если процесс прошел без ошибок, иначе false.</returns>
    private bool SaveProcessing()
    {
      if (this.Contact.Any())
      {
        string[] line = new string[this.Contact.Count];
        for (int i = 0; i < this.Contact.Count; i++)
        {
          using (StreamWriter sw = File.AppendText("phonebook.txt"))
          {
            sw.WriteLine($"{this.Contact[i].Name}\t{this.Contact[i].Number}");
            sw.Close();
          }
        }
        return true;
      }
      else
      {
        Console.WriteLine("Ошибка в сохранении списка");
        return false;
      }
    }
    #endregion
    #region Конструкторы

/// <summary>
/// Конструктор для создания телефонной книги и запрос пути к файлу .exe.
/// </summary>
    public Phonebook()
    {        
      //var exePath = Environment.CurrentDirectory;
      this.path = Path.Combine("phonebook.txt");
      this.Contact = new List<Abonent>();
			OpenPhoneBook();
    }
    #endregion

  }
}
