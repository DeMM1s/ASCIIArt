using Accord.Video.FFMPEG;
using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Threading;

namespace ASCIIArt.Video
{
	public class VideoConvert
	{
		private const double WIDTH_OFFSET = 2.3;
		private const int MAX_WIDTH = 200;
		private const int magicNumber = 32;
		private readonly FileStream musicStream;
		private readonly SoundPlayer soundPlayer;
		private readonly Thread musicThread;
		private readonly VideoFileReader videoFile;
		private readonly ASCIIPlayer asciiPlayer;
		private readonly Stopwatch stopwatch;
		static long elapsedRenderAvg = 0;
		static long elapsedDisplayAvg = 0;
		static int currentFrame = 0;
		static int delay;
		public VideoConvert()
		{
			videoFile = new VideoFileReader();
			videoFile.Open("video.mp4");
			musicStream = File.OpenRead("music.wav");
			soundPlayer = new SoundPlayer(musicStream);
			musicThread = new Thread(new ThreadStart(soundPlayer.Play));
			asciiPlayer = new ASCIIPlayer();
			stopwatch = new Stopwatch();
		}
		public void StartWithoutLoading()
		{
			Console.CursorVisible = false;
			Console.Clear();
			long elapsedRender;
			long elapsedDisplay;
			string[] buffer;

			musicThread.Start();
			while (currentFrame < videoFile.FrameCount)
			{
				stopwatch.Restart();
				buffer = ConvertToString(videoFile, currentFrame);
				currentFrame++;
				elapsedRender = stopwatch.ElapsedMilliseconds;
				stopwatch.Restart();
				asciiPlayer.Display(buffer);
				elapsedDisplay = stopwatch.ElapsedMilliseconds;
				delay = CalculateDelay(elapsedDisplay, elapsedRender);
				DisplayStats(elapsedDisplay, elapsedRender, magicNumber - delay);
				Thread.Sleep(magicNumber - delay);

			}

			musicStream.Close();
			videoFile.Close();
		}

		public void StartWithLoading()
		{
			Console.CursorVisible = false;
			Console.Clear();
			long elapsedDisplay;
			string[][] frames = new string[videoFile.FrameCount][];

			while (currentFrame < videoFile.FrameCount)
			{
				Console.WriteLine($"Prepare video:{(currentFrame * 1.0 / videoFile.FrameCount * 100):f2}");
				frames[currentFrame] = ConvertToString(videoFile, currentFrame);
				currentFrame++;
				Console.SetCursorPosition(0 , 0);
			}

			Console.Clear();
			currentFrame = 0;
			musicThread.Start();
			while (currentFrame < videoFile.FrameCount)
			{
				stopwatch.Restart();
				asciiPlayer.Display(frames[currentFrame]);
				currentFrame++;
				elapsedDisplay = stopwatch.ElapsedMilliseconds;
				Thread.Sleep(magicNumber - (int)elapsedDisplay);

			}

			musicStream.Close();
			videoFile.Close();
		}


		private string[] ConvertToString(VideoFileReader videoFile, int currentFrame)
		{
			char[][] temp = BitmapToASCIIConverter.Convert(videoFile.ReadVideoFrame(currentFrame).Resize(MAX_WIDTH, WIDTH_OFFSET));
			string[] frame = new string[temp.Length];
			for (int i = 0; i < temp.Length; i++)
			{
				frame[i] = new string(temp[i]);
			}
			return frame;
		}
		
		private static int CalculateDelay(long elapsedDisplay, long elapsedRender)
		{
			
			return (elapsedRender + elapsedDisplay) > magicNumber ? magicNumber : (int)(elapsedRender + elapsedDisplay);
		}
		private static void DisplayStats(long elapsedDisplay, long elapsedRender, long delayTime)
		{
			elapsedRenderAvg += elapsedRender;
			elapsedDisplayAvg += elapsedDisplay;
			Console.WriteLine($"Render:{elapsedRender}   ");
			Console.WriteLine($"Display:{elapsedDisplay}   ");
			Console.WriteLine($"Added delay:{magicNumber - delay}   ");
			Console.WriteLine($"Time for frame:{elapsedRender + elapsedDisplay + delayTime}   ");
			if (currentFrame % 10 == 0)
			{
				Console.WriteLine($"Average Render:{elapsedRenderAvg / currentFrame}   ");
				Console.WriteLine($"Average Display:{elapsedDisplayAvg / currentFrame}   ");
				Console.WriteLine($"All Average:{(elapsedRenderAvg + elapsedDisplayAvg) / currentFrame}   ");
			}
		}
	}
}
