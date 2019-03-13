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
	public class Game
	{		
		public Map BWMap;

		public bool Running = false;
		
		public int IndexOfTurn = 0;
		
		public List<Team> teams;

		public List<Player> Players = new List<Player>();

		public Game(List<Player> Players, Map map)
		{
			this.Players = Players;
			this.BWMap = map;
		}
		
		public void Start()
		{
			Running = true;
		Retry:
			for (int index = 0; index < Players.Count; index = index)
			{
				Player p = Players[index];
				#if DEBUG
				Console.WriteLine("testing player " + p.Name);
				#endif
				foreach (Location[] row in BWMap.LocCoords)
				{
					foreach (Location i in row)
					{
						if (i.GetType() == typeof(Base))
						{
							Base b = (Base)i.map.GetLocationByCoords(i.Coords[0], i.Coords[1]);
							if (b.Team == p.CurrentTeam && p.IsAtBase == false)
							{
								p.loc = i;
								p.IsAtBase = true;
								#if DEBUG
								Utils.PrintPlayerNameWithFormattingPlusMoreText(p, " is at " + b.Team.DisplayColor + " base");
								#endif
								break;
							}
						}
					}
				}
				index++;
			}
			foreach (Player pl in Players)
				{
					if (!pl.IsAtBase)
					{
						goto Retry;
					}
				}
		}
		
		public Location GetPlayerLoctionByIndex(int index)
		{
			return Players[index].loc;
		}
		
		public Player PlayerFromTurnIndex(int index)
		{
			return Players[index];
		}
		
		public void NextTurn()
		{
			IndexOfTurn = IndexOfTurn < Players.Count ? IndexOfTurn + 1 : 0;
			PlayerFromTurnIndex(IndexOfTurn).loc.ques.Ask(PlayerFromTurnIndex(IndexOfTurn));
			BWMap.Update();
		}
	}
}

