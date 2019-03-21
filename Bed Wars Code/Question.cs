using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading;
using Bed_Wars_Code;
using Techcraft7_DLL_Pack;
using CCM = Techcraft7_DLL_Pack.ColorConsoleMethods;
namespace Bed_Wars_Code
{
	public class Question
	{
		public List<BWAction> actions = new List<BWAction>();

		public string Text = "";

		bool success = false;

		public Question(string QText, List<BWAction> actions)
		{
			Text = QText;
			this.actions = new List<BWAction>();
			foreach (BWAction act in actions)
			{
				this.actions.Add(act);
			}
		}

		private string GetOptionsString()
		{
			string output = "";
			foreach (BWAction i in actions) 
			{
				output += i.alias + ",";
			}
			output.Remove(output.Length - 1, 1);
			return output;
		}

		public void Ask(Player p)
		{
			while (success == false) 
			{
				Utils.PrintPlayerNameWithFormattingPlusMoreText(p, this.Text);
				foreach (BWAction i in actions)
				{
					if (Console.ReadLine().ToLower() == i.alias.ToLower())
					{
						i.Execute(p);
						success = true;
						break;
					}
					else
					{
						CCM.WriteLineColor("You did not enter a valid option! options: " + GetOptionsString() + "\nPress enter to continue", ConsoleColor.Red);
						success = false;
						Console.ReadLine();
						Console.Clear();
						break;
					}
				}
			}
			success = false;
		}
		
		public void CombatAsk(Player[] ps)
		{
			
		}
	}
}

