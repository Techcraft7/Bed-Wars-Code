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
			this.actions = actions;
		}

		private string GetOptions()
		{
			string output = "";
			foreach (BWAction i in actions) {
				output += i.alias + ",";
			}
			output.Remove(output.Length - 1, 1);
			return output;
		}

		public void Ask()
		{
			while (success == false) {
				Console.WriteLine(Text);
				foreach (BWAction i in actions) {
					if (Console.ReadLine() == i.alias) {
						i.Execute();
						success = true;
					}
					else {
						CCM.WriteLineColor("You did not enter a valid option! options: " + GetOptions() + "\nPress enter to continue", ConsoleColor.Red);
						Console.ReadLine();
						Console.Clear();
						break;
					}
				}
			}
			success = false;
		}
	}
}

