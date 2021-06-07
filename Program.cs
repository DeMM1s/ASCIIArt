using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Imaging;
using ASCIIArt.Image;
using ASCIIArt.Video;

namespace ASCIIArt
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("С чем будем работать?");
			Console.WriteLine("1. Изображение");
			Console.WriteLine("2. Видео");
			int choose = Int32.Parse(Console.ReadLine());
			switch (choose)
			{
				case 1:
					ImageConvert.Start();
					break;
				case 2:
					VideoConvert.Start();
					break;
				default:
					break;
			}
		}
	}
}
