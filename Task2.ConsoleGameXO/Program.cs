using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.ConsoleGameXO
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.GetEncoding(1251);
			Program Game = new Program();
			Console.WriteLine("Кто ты воин?\nКак тебя зовут?\n");
			User User = new User(Console.ReadLine()) 
			{ 
				ThisIsAMan = true 
			};
			User II = new User("BILLY") 
			{ 
				ThisIsAMan=false 
			};
			char[] field = new char[9] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
			Game.PrintStartGame();
			Game.PrintField(field, 0);

			Console.WriteLine("СТАРТ ИГРЫ!!!\nУДАЧИ!");
			if (Game.WhoIsFirst(ref II, ref User))
			{
				int MoveCounter = 1;
				bool DrowFlag = false;
				do
				{					
					if (MoveCounter > 4 && !Game.WinCheck(field, ref II, ref User))
					{
						DrowFlag = true;
						User.Wining = false;
						II.Wining = false;
						break;
					}
					if (User.HeWalksFirst)
					{
						if (Game.Move(User, ref field))
						{
							if (Game.Move(II, ref field))
							{
								Game.PrintField(field, MoveCounter++);
								continue;
							}
							else
							{
								Console.WriteLine("Я Бота не понял давай еще раз");
								Console.WriteLine("Выключение...");
								break;
							}
						}
						else
						{
							Console.WriteLine("Я тебя не понял давай еще раз");
							Console.WriteLine("Выключение...");
							break;
						}
					}
					else if (II.HeWalksFirst)
					{
						if (Game.Move(II, ref field))
						{
							if (Game.Move(User, ref field))
							{
								Game.PrintField(field, MoveCounter++);
								continue;
							}
							else
							{
								Console.WriteLine("Я тебя не понял давай еще раз");
								Console.WriteLine("Выключение...");
								break;
							}
						}
						else
						{
							Console.WriteLine("Я Бота не понял давай еще раз");
							Console.WriteLine("Выключение...");
							break;
						}
					}					

				} while (!Game.WinCheck(field, ref II, ref User));
				Game.LetsCelebrate(User, II, DrowFlag);
			}
			else
			{
				Console.WriteLine("Игра не запустилась(");
				Console.WriteLine("Выключение...");
			}
		}
		
		/// <summary>
		/// Вывод в консоль поля для игры.
		/// </summary>
		/// <param name="field">массив, хранящий поле для игры.</param>
		/// <param name="WhatMove">Какой ход по счету идет.</param>
		
		public void PrintField(char[] field, int WhatMove) // Вывод игрового поля 
		{
			if (WhatMove == 0)
			{
				Console.WriteLine($" ---Ход№{WhatMove}---");
				Console.WriteLine($"Сейчас в ячейках их номера)");
				Console.WriteLine(" -----------");
				Console.WriteLine($"| 1 | 2 | 3 |");
				Console.WriteLine(" -----------");
				Console.WriteLine($"| 4 | 5 | 6 |");
				Console.WriteLine(" -----------");
				Console.WriteLine($"| 7 | 8 | 9 |");
				Console.WriteLine(" -----------");
			}
			else
			{
				Console.WriteLine($" ---Ход№{WhatMove}---");
				Console.WriteLine(" -----------");
				//Console.WriteLine($"| {field[0]} | {field[1]} | {field[2]} |");
				int CellRow = 0;
				for (int i = 0; i < 9; i++)
				{
					Console.Write($"|");
					Console.Write($" {ChangeConsole(field[i])} ");
					Console.ForegroundColor = ConsoleColor.Gray;
					//Console.Write(" | ");
					CellRow++;
					if (CellRow == 3)
					{
						Console.WriteLine("|\n -----------");
						CellRow = 0;
					}
				}

				//if (field[0] == 'X') Console.ForegroundColor = ConsoleColor.Green; else if (field[0] == 'O') Console.ForegroundColor = ConsoleColor.Red; Console.ForegroundColor = ConsoleColor.Gray; Console.Write(" | ");
				//Console.WriteLine(" -----------");
				//Console.WriteLine($"| {field[3]} | {field[4]} | {field[5]} |");
				//Console.WriteLine(" -----------");
				//Console.WriteLine($"| {field[6]} | {field[7]} | {field[8]} |");
				Console.WriteLine(" -----------");

			}
		}				
		
		/// <summary>
		/// Изменения цвета консоли для окраски крестиков и ноликов.
		/// </summary>
		/// <param name="Cell">Ячейка игравого поля.</param>
		/// <returns>Возвращает используемый символ для вывода в строку.</returns>
		
		char ChangeConsole(char Cell)
		{
			if (Cell == 'X')
				Console.ForegroundColor = ConsoleColor.Red;
			else if (Cell == 'O')
				Console.ForegroundColor = ConsoleColor.Green;
			return Cell;
		}
		
		/// <summary>
		/// Осуществляет запрос на ход у переданного экземпляра.
		/// </summary>
		/// <param name="Player"> Переданный экземпляр класса User, игрок который совершает ход при вызове.</param>
		/// <param name="field">Ссылка на поле для игры.</param>
		/// <returns>True, если ход завершен успешно, иначе - false.</returns>
		
	
		private bool Move(User Player, ref char[] field) //Функция хода
		{
			bool MoveFlag = false;
			while (!MoveFlag)
			{
				if (Player.ThisIsAMan)
				{
					Console.WriteLine("ТВОЙ ХОД!!! ВЫБЕРИ ИЗ \n");
					List<int> FreeCells = WhichFreeCells(field);
					for (int i = 0; i < FreeCells.Count; i++)
					{
						Console.Write($"{FreeCells[i]}/");
					}
					if (int.TryParse(Console.ReadLine(), out int UserMove) && (UserMove >= 1 && UserMove <= 9))
					{
						if (field[UserMove - 1] == ' ')
						{
							field[UserMove - 1] = Player.Chars;
							MoveFlag = true;
							return MoveFlag;
						}
						else
						{
							Console.WriteLine("Сделай другой ход!");
							//return MoveFlag;
							continue;
						}
					}
					else
					{
						Console.WriteLine("Я тебя не понял давай еще раз");
						//return MoveFlag;]
						continue;
					}
				}
				else if (!Player.ThisIsAMan)
				{
					field[BotMove(Player, ref field)] = Player.Chars;
					MoveFlag = true;
					return MoveFlag;
				}
				else
				{
					Console.WriteLine("Я тебя не понял давай еще раз");
					//return false;
					continue;
				}
			}
			return MoveFlag;

		}
		
		/// <summary>
		/// Получение свободных клеток для хода.
		/// </summary>
		/// <param name="Field">Поле для игрыю.</param>
		/// <returns>
		/// Номера свободных клеток.
		/// </returns>
		
		List<int> WhichFreeCells(char[] Field)
		{
			List<int> FreeCellsArray = new List<int>();
			for (int i = 0; i < Field.Length; i++)
			{
				if (Field[i] == ' ')
				{
					FreeCellsArray.Add(i + 1);
				}
			}
			return FreeCellsArray;
		}
		
		/// <summary>
		/// Выводит стартовую заставку.
		/// </summary>
		
		private void PrintStartGame()//Начальная заставка
		{
			Console.WriteLine("__________________________________________________________________________________");
			Console.WriteLine("         WEEEEEEEELCOME! В ИГРУ КРЕСТИКИ И НОЛИКИ ИЛИ АНАБОЛИКИ, не важно");
			Console.WriteLine("----------------------------------------------------------------------------------");
		}
		
		/// <summary>
		/// Алгоритм хода Бота.
		/// </summary>
		/// <param name="II">Переданный экземпляр класса User, игрок который совершает ход при вызове.В данном случае Бот.</param>
		/// <param name="field">Ссылка на поле для игры.</param>
		/// <returns>Найденный свободный индекс в поле, для хода Бота.</returns>
		
		private int BotMove(User II, ref char[] field)//Алгоритм хода у Бота
		{			
			int NextMove = 0;
			int BestMove = 0;
			bool ChangeFlag = false;
			for (int i = 0; i < 8; i++)
			{
				int GatchaWin = 0;
				for (int j = 0; j < 3; j++)
				{
					if (field[winCombo[i, j]] == II.Chars)
					{
						GatchaWin++;
					}
				}
				if (GatchaWin > BestMove)
				{
					BestMove = GatchaWin;
					ChangeFlag = true;
				}
			}
			if (!ChangeFlag)
			{
				Random rnd = new Random();
				BestMove = rnd.Next(0, 8);
			}
			for (int j = 0; j < 3; j++)
			{
				if (field[winCombo[BestMove, j]] == ' ')
				{
					NextMove = winCombo[BestMove, j];
				}
			}
			int IndexBoteMove = NextMove;
			Console.WriteLine($"Бот выбрал - {IndexBoteMove + 1}");
			return IndexBoteMove;
		}
		
		/// <summary>
		/// Мини игра в Орла и Решку для определения кто первый будет ходить.
		/// </summary>
		/// <param name="II">Бот.</param>
		/// <param name="User">Пользователь.</param>
		/// <returns>True, если мини игра завершена успешно, иначе - false.</returns>
		
		private bool WhoIsFirst(ref User II, ref User User)// Функция для определения кто ходит первый (Игра Орел и Решка)
		{
			bool StartGameFlag = false;
			Random rnd = new Random();
			do
			{
				Console.WriteLine("Кто будет ходить первым? Определит случай ;-)\nВыбери сторону монетки\n\tОрел/Решка(1/2) -");
				if (int.TryParse(Console.ReadLine(), out int UserMove) && (UserMove == 1 || UserMove == 2))
				{
					int Drop = rnd.Next(1, 3);
					if (Drop == UserMove)
					{
						Console.WriteLine("ТЫ УГАДАЛ!!!!\nВыбери сторону Х/O(1/2) - ");
						if (int.TryParse(Console.ReadLine(), out int UserChar) && (UserChar == 1 || UserChar == 2))
						{
							if (UserChar == 1)
							{
								User.Chars = 'X';
								User.HeWalksFirst = true;
								II.Chars = 'O';
								II.HeWalksFirst = false;
							}
							else if (UserChar == 2)
							{
								User.Chars = 'O';
								User.HeWalksFirst = false;
								II.Chars = 'X';
								II.HeWalksFirst = true;
							}

						}							
						else
						{
							Console.WriteLine("Я тебя не понял давай еще раз");
							continue;
						}
					}
					else
					{
						int BotDrop = rnd.Next(1, 3);
						if (BotDrop == 1 || BotDrop == 2)
						{
							if (BotDrop == 1)
							{
								II.Chars = 'X';
								II.HeWalksFirst = true;
								User.Chars = 'O';
								User.HeWalksFirst = false;
							}
							else if (BotDrop == 2)
							{
								II.Chars = 'O';
								II.HeWalksFirst = false;
								User.Chars = 'X';
								User.HeWalksFirst = true;
							}
						}
						Console.WriteLine($"ТЫ НЕ УГАДАЛ!!!!\nБОТ Выбрал сторону Х/O(1/2) - {BotDrop}");
					}
					StartGameFlag = true;
				}
				else
				{
					Console.WriteLine("Я тебя не понял давай еще раз");
					continue;
				}

			} while (!StartGameFlag);
			return StartGameFlag;
		}


    /// <summary>
    /// Проверка ситуации на возможную победу.
    /// </summary>
    /// <param name="field">Игравое поле.</param>
    /// <param name="II">Бот.</param>
    /// <param name="User">Пользователь.</param>
    /// <returns>True, если найден победитель, иначе - false.</returns>
    private bool WinCheck(char[] field, ref User II, ref User User)//Проверка на победу
		{
			bool WinFlag = false;

			if (ScrollField(ref User) && !ScrollField(ref II))
			{
				II.Wining = false;
			}
			else if (ScrollField(ref II) && !ScrollField(ref User))
			{
				User.Wining = false;
			}
			else
			{
				//User.Wining = false;
				//II.Wining = false;
				return false;
			}
			bool ScrollField(ref User player)
			{
				for (int i = 0; i < 8; i++)
				{
					int GatchaWin = 0;
					for (int j = 0; j < 3; j++)
					{
						if (field[winCombo[i, j]] == player.Chars)
						{
							GatchaWin++;
						}
					}
					if (GatchaWin == 3)
					{
						WinFlag = true;
						player.Wining = true;
					}
				}
				return player.Wining;
			}
			return WinFlag;
		}
		
		/// <summary>
		/// Проверить кто-то победил или ничья.
		/// </summary>
		/// <param name="Player">Пользователь.</param>
		/// <param name="II">Бот.</param>
		/// <param name="DrawFlag">Флаг о ничьей. Если он True, то случилась ничья, иначе - false и есть победитель</param>
		
		private void LetsCelebrate(User Player,User II, bool DrawFlag)// Поздравительное сообщение
		{
			if (!DrawFlag)
			{
				if (Player.Wining)
				{
					PrintMessege(Player);
				}
				else if (II.Wining)
				{
					PrintMessege(II);
				}
				else
				{
					Console.WriteLine("Ошибка победы");
				}
			}
			else
			{
				PrintMessege();
			}
		}
		
		/// <summary>
		/// Поздравительное сообщение о конце игры. Выводит в консоль сообщенмие.
		/// </summary>
		/// <param name="Winner">Победитель.</param>
		private void PrintMessege(User Winner)
		{
			Console.WriteLine(" --------------------------------------------------------------------- ");
			Console.WriteLine("|                  \\  /    /\\    || ||                                |");	
			Console.WriteLine("|                   ||     \\/    \\\\//                                 |");	
			Console.WriteLine(" --------------------------------------------------------------------- ");	
			Console.WriteLine($"                    YOU  WIN MY BOY - {Winner.Name}                   ");	
			Console.WriteLine(" --------------------------------------------------------------------- ");			
			Console.WriteLine("|                 \\   /\\   / ||    ||\\ ||                             |");		
			Console.WriteLine("|                  \\/   \\/   ||    || \\||                             |");
			Console.WriteLine(" --------------------------------------------------------------------- ");
		}

		/// <summary>
		/// Cообщение о ничьей. Выводит в консоль сообщенмие.
		/// </summary>
		private void PrintMessege()
		{
			Console.WriteLine(" --------------------------------------------------------------------- ");
			Console.WriteLine("|                                                                     |");
			Console.WriteLine("|                                                                     |");
			Console.WriteLine(" --------------------------------------------------------------------- ");
			Console.WriteLine($"                            - НИЧЬЯ -                                 ");
			Console.WriteLine(" --------------------------------------------------------------------- ");
			Console.WriteLine("|                                                                     |");
			Console.WriteLine("|                                                                     |");
			Console.WriteLine(" --------------------------------------------------------------------- ");
		}

		/// <summary>
		/// Выигрышные комбинации в крестиках ноликах
		/// </summary>

		readonly int[,] winCombo = new int[,] // Набор выиграшных комбинаций
		{				
			{0,1,2},				
			{3,4,5},				
			{6,7,8},			
			{0,3,6},				
			{1,4,7},		
			{2,5,8},		
			{0,4,8},
			{6,4,2},
		};


	}
  /// <summary>
  /// Список возможных символов для игры.
  /// </summary>		
  ///  <param name = "X"> Крестик </param>
  ///  <param name = "O"> Нолик </param>
  internal enum Symbols
  {
    X = 'X',
    O = 'O'
  }
}
