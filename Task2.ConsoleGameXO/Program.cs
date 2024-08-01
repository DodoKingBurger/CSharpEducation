using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2.ConsoleGameXO
{
	internal class Program
	{
		public class User
		{
			public int Who { get; set; }// 1 - Человек, а 2 - Компьютер
			public Char Char { get; set; }// 1 - X (Крестик), 2 - O (Нулик)
			public int HowHeWalks { get; set; } // Каким ходит экземпляр 1 или 2

			public bool Wining { get; set; } // Статус победителя 
			public string Name; // Имя игрока 
			public User(string name)
			{
				Name = name;
			}
		}
		static void Main(string[] args)
		{
			Console.OutputEncoding = Encoding.GetEncoding(1251);
			Program Game = new Program();
			bool NameSelected = false;
			User User = new User("");
			do
			{
				Console.WriteLine("Кто ты воин?\nКак тебя зовут?\n");
				string name = Console.ReadLine();
				if (string.Empty != name)
				{
					User = new User(name);
					User.Who = 1;
					NameSelected = true;
				}
				else
				{
					Console.WriteLine("Не знаю таких войнов! АТАКУЮ!!!\nЕще раз...");
					continue;
				}
			} while (!NameSelected);
			User II = new User("BILLY");
			II.Who = 2;
			char[] field = new char[9] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
			int[] MoveOrder = new int[1];
			Game.PrintStartGame();
			Game.PrintField(field, 0);
			//MoveOrder = Game.WhoIsFirst(II,user);
			Console.WriteLine("СТАРТ ИГРЫ!!!\nУДАЧИ!");
			if (Game.WhoIsFirst(ref II, ref User))
			{
				int MoveCounter = 1;
				bool DrowFlag = false;
				do
				{
					if (MoveCounter > 4)
					{
						DrowFlag = true;
						User.Wining = false;
						II.Wining = false;
						break;
					}
					if (User.HowHeWalks == 1 && II.HowHeWalks == 2)
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
					else if (User.HowHeWalks == 2 && II.HowHeWalks == 1)
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
				if (DrowFlag == false && Game.WinCheck(field, ref II, ref User))
					Game.LetsCelebrate(User, II);
				else
					Game.LetsCelebrate();
			}
			else
			{
				Console.WriteLine("Игра не запустилась(");
				Console.WriteLine("Выключение...");
			}
		}
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
				char ChangeConsole(char Cell)
				{
					if (Cell == 'X')
						Console.ForegroundColor = ConsoleColor.Red;
					else if (Cell == 'O')
						Console.ForegroundColor = ConsoleColor.Green;
					return Cell;
				}
			}
		}
		public bool Move(User Player, ref char[] field) //Функция хода
		{
			bool MoveFlag = false;
			while (!MoveFlag)
			{
				if (Player.Who == 1)
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
							field[UserMove - 1] = Player.Char;
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
				else if (Player.Who == 2)
				{
					field[BotMove(Player, field)] = Player.Char;
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
		}
		public void PrintStartGame()//Начальная заставка
		{
			Console.WriteLine("__________________________________________________________________________________");
			Console.WriteLine("         WEEEEEEEELCOME! В ИГРУ КРЕСТИКИ И НОЛИКИ ИЛИ АНАБОЛИКИ, не важно");
			Console.WriteLine("----------------------------------------------------------------------------------");
		}
		public int BotMove(User II, char[] field)//Алгоритм хода у Бота
		{
			int IndexBoteMove = ScrollField(II);
			Console.WriteLine($"Бот выбрал - {IndexBoteMove + 1}");
			return IndexBoteMove;
			int ScrollField(User player)
			{
				int NextMove = 0;
				int BestMove = 0;
				bool ChangeFlag = false;
				for (int i = 0; i < 8; i++)
				{
					int GatchaWin = 0;
					for (int j = 0; j < 3; j++)
					{
						if (field[winCombo[i, j]] == player.Char)
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
				return NextMove;
			}
		}
		public bool WhoIsFirst(ref User II, ref User User)// Функция для определения кто ходит первый (Игра Орел и Решка)
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
								User.Char = 'X';
								User.HowHeWalks = 1;
								II.Char = 'O';
								II.HowHeWalks = 2;
							}
							else if (UserChar == 2)
							{
								User.Char = 'O';
								User.HowHeWalks = 2;
								II.Char = 'X';
								II.HowHeWalks = 1;
							}
							else
							{
								Console.WriteLine("Я тебя не понял давай еще раз");
								continue;
							}
						}
					}
					else
					{
						int BotDrop = rnd.Next(1, 3);
						if (BotDrop == 1 || BotDrop == 2)
						{
							if (BotDrop == 1)
							{
								II.Char = 'X';
								II.HowHeWalks = 1;
								User.Char = 'O';
								User.HowHeWalks = 2;
							}
							else if (BotDrop == 2)
							{
								II.Char = 'O';
								II.HowHeWalks = 2;
								User.Char = 'X';
								User.HowHeWalks = 1;
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
		public bool WinCheck(char[] field, ref User II, ref User User)//Проверка на победу
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
						if (field[winCombo[i, j]] == player.Char)
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
		public void LetsCelebrate(User Player, User II)// Поздравительное сообщение
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
			void PrintMessege(User Winner)
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
		}
		public void LetsCelebrate()// Сообщение о ничьей 
		{
			PrintMessege();

			void PrintMessege()
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
		}
		int[,] winCombo = new int[,] // Набор выиграшных комбинаций
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
}
