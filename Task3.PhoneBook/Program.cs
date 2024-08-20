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
      Phonebook phoneBook = new Phonebook();
      TimeSpan Timer = new TimeSpan();


		}
		/// <summary>
		/// Выбор действия в главном меню
		/// </summary>
		/// <param name="request">Номер под какоторым должна пройти операция</param>
		/// <param name="phonebook">Ссылка на коллекцию абонентов</param>
		/// <returns>true, если процесс прошел без ошибок, иначе false</returns>
		public bool Request(string request, ref Phonebook phonebook)
    {
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
						SearchContact(ref phonebook);
            break;
					case 4:
						Console.WriteLine("Вы выбрали удаление контакта");
						DeleteContact(ref phonebook);
						break;
          default:
            Console.WriteLine("Неизвестная операция");
            return false;
				}
        return true;
      }
      else
      {
				Console.WriteLine("Неизвестная операция");
        return false;
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
					}
				}
			}
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

			StopFlag = false;

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
		public void DeleteContact(ref Phonebook phonebook)
		{

		}
    public void SearchContact(ref Phonebook phonebook)
    {
			Console.WriteLine("Для пойска контакта введите имя или номер телефона");
			string request = Console.ReadLine();
			if (string.IsNullOrEmpty(request))
			{
				if(long.TryParse(request,out long number))
				{
					if (phonebook.SearchAbonent(number))
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
					Console.WriteLine("Абонент не найден, создать новый контакт можно в главном меню");
				} 
			}
			else
			{
				Console.WriteLine("Имя не было введенно или введено некоректно!");
			}
		}
    public void UpdateContact(ref Phonebook phonebook) 
    { 

    }
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
