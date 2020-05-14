using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bed_Wars_Code
{
	//Not all upgrades from the real game will be implemented for simplicity
	internal enum UpgradeType
	{
		//See the Minecraft wiki for enchantment values
		//Level 1 = Sharpness I (+1 Damage*)
		//*1 HP = 1 half-heart
		SHARPENED_SWORDS = 0,
		//Level 1 = Protection I   (-4%  Damage)
		//Level 2 = Protection II  (-8%  Damage)
		//Level 3 = Protection III (-12% Damage) 
		//Level 4 = Protection IV  (-16% Damage)
		PROTECTION = 1,
		//Level 1 = Haste I  (+10% Combo* + 20% Mining Speed)
		//Level 2 = Haste II (+20% Combo* + 40% Mining Speed)
		//*Combo is a measurement of the player's attack speed
		//In 1.9 haste increases attack speed, which decreases
		//the attack cooldown, allowing for 1.8-style pvp
		//in 1.9, given that the level of haste is high enough
		HASTE,
		//Level 1 = It's A Trap!       (Blindness - make player miss more)
		//Level 2 = Alarm Trap         (Remove invisibility)
		//Level 3 = Miner Fatigue Trap (Makes rushing harder OR negates haste)
		TRAP,
	}
}
