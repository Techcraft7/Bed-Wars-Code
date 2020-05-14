using System;
using System.Linq;
using System.Collections.Generic;

namespace Bed_Wars_Code
{
	internal class Utils
	{
		public static T[][] CreateMatrix<T>(int w, int h)
		{
			ThrowIfOutOfBounds(w, 1, int.MaxValue - 1);
			ThrowIfOutOfBounds(h, 1, int.MaxValue - 1);
			List<T[]> list = new List<T[]>();
			for (int i = 0; i < h; i++)
			{
				list.Add(new T[w]);
			}
			return list.ToArray();
		}

		public static Dictionary<UpgradeType, int> GetEmptyUpgradeDict()
		{
			var list = new Dictionary<UpgradeType, int>();
			foreach (UpgradeType ut in Enum.GetValues(typeof(UpgradeType)).Cast<UpgradeType>())
			{
				list.Add(ut, 0);
			}
			return list;
		}

		public static bool IsInRange(int v, int min, int max)
		{
			if (min > max)
			{
				throw new ArgumentException("Minimum cannot be greater than maximum", nameof(min));
			}
			return v >= min && v <= max;
		}

		public static void ThrowIfAnyNull<T>(IEnumerable<T> list)
		{
			foreach (T v in list)
			{
				//ignore value types because they are not nullable
				if (v.GetType().IsValueType)
				{
					continue;
				}
				if (v == null)
				{
					throw new ArgumentNullException($"One of the elements of {nameof(list)} is null!");
				}
			}
		}

		public static string FormatEnumByUnderscores<T>(T v) where T : Enum
		{
			string[] split2 = v.ToString().Split('_');
			for (int i = 0; i < split2.Length; i++)
			{
				split2[i] = split2[i][0].ToString().ToUpper() + split2[i].Substring(1).ToLower();
			}
			return string.Join(" ", split2);
		}

		public static void ThrowIfOutOfBounds(int v, int min, int max)
		{
			if (!IsInRange(v, min, max))
			{
				throw new ArgumentException($"{nameof(v)} must be between {min} and {max}! (inclusive)", nameof(v));
			}
		}

		public static IEnumerable<T> ExtendWithNull<T>(IEnumerable<T> list, int len)
		{
			while (list.Count() < len)
			{
				list = list.Concat(new object[1].Cast<T>());
			}
			return list;
		}

		public static void PrintPlayerNameInText(string txt, Player p)
		{
			string[] split = txt.Split(new string[] { "%PLAYER%" }, StringSplitOptions.None);
			for (int i = 0; i < split.Length; i++)
			{
				Console.Write(split[i]);
				if (i < split.Length - 1)
				{
					p.WriteToConsole();
				}
			}
		}
	}
}