using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Task3.PhoneBook
{
  internal class Program
  {
		static void Main(string[] args)
    {
      Console.OutputEncoding = Encoding.GetEncoding(1251);
			Program program = new Program();
      Phonebook phoneBook = new Phonebook();
      TimeSpan Timer = new TimeSpan();
			Console.WriteLine("Добро пожаловать в телефонный справочник!\n");
      program.TimerTick(Timer,ref phoneBook);
      program.Request(ref phoneBook);
		}

		/// <summary>
		/// Вызов главного меню
		/// </summary>
		/// <param name="phonebook">Ссылка на коллекцию абонентов</param>
		public void Request(ref Phonebook phonebook)
    {
			Console.WriteLine("\nГлавное меню\n" +
        "1 - Создать новый контакт\n" +
        "2 - Обновить данные уже существуещего контакта\n" +
        "3 - Найти контакт\n" +
        "4 - Удалить контакт\n");
			string request = Console.ReadLine();
      if(int.TryParse(request, out int result))
      {
        switch(result)
        {
          case 1:
            Console.WriteLine("Вы выбрали создания нового контакта");
            SaveContact(ref phonebook);
            break;
					case 2:
						Console.WriteLine("Вы выбрали обновления данных контакта");
						UpdateContact(ref phonebook);
						break;
          case 3:
						Console.WriteLine("Вы выбрали пойск контакта");
						SearchContact(ref phonebook, out Abonent abonent);
            break;
					case 4:
						Console.WriteLine("Вы выбрали удаление контакта");
						DeleteContact(ref phonebook);
						break;
          default:
            Console.WriteLine("Неизвестная операция! Возвращаемся в\n");
						break;
				}
        phonebook.Save();
				Request(ref phonebook);
      }
      else
      {
				Console.WriteLine("Неизвестная операция! Возвращаемся в\n");
				Request(ref phonebook);
			}
    }

		/// <summary>
		/// Запись нового контакта в коллекцию абонентов
		/// </summary>
		/// <param name="phonebook">Ссылка на коллекцию абонентов</param>
		public void SaveContact(ref Phonebook phonebook)
    {
      Abonent NewAbonent = new Abonent();
			Console.WriteLine("Для создания нового контакта");

      bool StopFlag = false;			
      while (StopFlag == false)
			{			
        Console.Write("Введите номер телефона: ");
				string number = Console.ReadLine();
				if (string.IsNullOrWhiteSpace(number))
				{
					Console.WriteLine("Некорректная операция: не бывает таких номеров!");
					continue;
				}
				else
				{
					if(long.TryParse(number,out long Number))
          {
						NewAbonent.Number = Number;
					  StopFlag = true;
          }
          else
          {
						Console.WriteLine("Некорректная операция: не бывает таких номеров!");
            SaveContact(ref phonebook);
          }
				}
			}			
			StopFlag = false;
      while (StopFlag == false)
			{			
        Console.Write("Введите имя: ");
        string name = Console.ReadLine();
        if(string.IsNullOrWhiteSpace(name))
        {
          NewAbonent.Name = NewAbonent.NumberStr;
					Console.Write($"Имя не было введенно или введено некоректно! Взят номер телефона как имя: {NewAbonent.NumberStr}");
				}
        else
        {
          NewAbonent.Name = name;
          StopFlag = true;
				}
      }
      if (NewAbonent.Any())
      {
        if (phonebook.Add(NewAbonent))
        {
				  Console.WriteLine("Контакт сохранен");
        }
        else
        {
				  Console.WriteLine("Не получилось создать контакт");
				  SaveContact(ref phonebook);
        }
			}
      else
      {
				Console.WriteLine("Не получилось создать контакт");
				SaveContact(ref phonebook);
      }
		}

		/// <summary>
		/// Метод для удаление контакта из переданного списка.
		/// </summary>
		/// <param name="phonebook">Ссылка на телефонную книгу.</param>
		public void DeleteContact(ref Phonebook phonebook)
		{
			SearchContact(ref phonebook, out Abonent abonent);
			if (abonent.Any())
			{
				if (phonebook.Delete(abonent))
				{
					Console.WriteLine("Контакт удален");
				}
				else
				{
					Console.WriteLine("Контакт не был найден");
				}
			}
			else
			{
        Console.WriteLine("Данные об абонненте пусты");
      }
    }

		/// <summary>
		/// Метод для пойска абонента в телефонной книге.
		/// </summary>
		/// <param name="phonebook">Ссылка на телефонную книгу.</param>
		/// <param name="abonent">Возращает найденного абонента.</param>
    public void SearchContact(ref Phonebook phonebook, out Abonent abonent)
    {
			Console.WriteLine("Для пойска контакта введите имя или номер телефона");
			string request = Console.ReadLine();
			abonent = new Abonent();
			if (!string.IsNullOrEmpty(request))
			{
				if(long.TryParse(request,out long number))
				{
					abonent.Number = number;
					if (phonebook.SearchAbonent(number, out abonent))
					{
						return;
					}
					else
					{
						SaveContact(ref phonebook);
					}
				}
				else 
				{
					abonent.Name = request;
					if (phonebook.SearchAbonent(request, out abonent))
					{
						return;
					}
					else
					{
            Console.WriteLine("Абонент не найден, создать новый контакт можно в главном меню");
            Request(ref phonebook);
          }
				} 					
			}
			else
			{
				Console.WriteLine("Имя не было введенно или введено некоректно!");
        Request(ref phonebook);
      }
		}

    /// <summary>
    /// Метод для изменение данный абонента.
    /// </summary>
    /// <param name="phonebook">Ссылка на телефонную книгу.</param>
    public void UpdateContact(ref Phonebook phonebook) 
    {
      SearchContact(ref phonebook, out Abonent abonent);
			if (abonent.Any())
			{
				phonebook.Update(abonent);
      }
    }

    /// <summary>
    /// Таймер для вызова сохранений каждую минуту.
    /// </summary>
    /// <param name="Timer">Таймер</param>
		/// <param name="phonebook">Ссылка на телефонную книгу.</param>
    private void TimerTick(TimeSpan Timer, ref Phonebook phonebook)
    {
			Timer = Timer.Subtract(new TimeSpan(0, 0, 1, 0));
      if (Timer.Seconds == 0 && Timer.Minutes == 0)
      {
				phonebook.Save();
			}
    }
	}
}
