using System;

namespace ASCIIArt
{
	public class Player
	{
		public static void Display(char[][] rows)
		{
			Console.Clear();
			foreach (var row in rows)
			{
				Console.WriteLine(row);
			}
		}
	}
}
