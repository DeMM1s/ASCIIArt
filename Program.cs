using System;
using System.Threading.Tasks;
using ASCIIArt.Image;
using ASCIIArt.Video;

namespace ASCIIArt
{
	class Program
	{
		static void Main()
		{
			bool work = true;
			VideoPlayer video = new VideoPlayer();
			Console.WriteLine("С чем будем работать?");
			Console.WriteLine("1. Изображение");
			Console.WriteLine("2. Видео");
			Console.WriteLine("q. Выход");
			while (work)
			{
				string choose = Console.ReadLine();
				switch (choose)
				{
					case "1":
						ImageConvert.Start();
						break;
					case "2":
						Console.WriteLine("Сейчас будет проигрываться видео в консоли в ASCII формате");
						Console.WriteLine("Уменьшите размер шрифта(желательно от 6 до 10)");
						Console.WriteLine("Для запуска нажмите любую клавишу");
						Console.ReadKey();
						video.Start();
						break;
					case "q":
						work = false;
						break;
					default:
						Console.WriteLine("Неверный ввод");
						break;
				}
			}
		}
	}
}
