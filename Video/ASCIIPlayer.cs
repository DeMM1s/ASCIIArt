using System;
using System.Threading;
using System.Diagnostics;

namespace ASCIIArt.Video
{
	public class ASCIIPlayer
	{
		public void Display(string[] frame)
		{
			Console.SetCursorPosition(0, 0);
			foreach (var row in frame)
			{
				Console.WriteLine(row);
			}
		}
	}
}
