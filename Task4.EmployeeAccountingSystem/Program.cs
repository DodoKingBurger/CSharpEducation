using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.EmployeeAccountingSystem
{
	internal class Program
	{
		static void Main(string[] args)
		{
			EmployeeManager manager = new EmployeeManager();
			try
			{
				manager.MainMenu();
			}
			catch (Exception ex) 
			{
				Console.WriteLine(ex.Message);
			}
		}

	}
}
