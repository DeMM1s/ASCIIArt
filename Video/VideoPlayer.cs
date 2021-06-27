using Accord.Video.FFMPEG;
using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Threading;

namespace ASCIIArt.Video
{
	public class VideoPlayer
	{
		private const double WIDTH_OFFSET = 2.3;
		private const int MAX_WIDTH = 200;
		private readonly VideoFileReader videoFile;
		private readonly FileStream musicStream;
		private readonly SoundPlayer soundPlayer;
		private readonly Thread musicThread;
		private readonly Stopwatch stopwatchRender;
		private readonly Stopwatch stopwatchDisplay;
		private readonly Stopwatch stopwatchFrame;
		private string[] currentFrame;
		private int currentFrameIndex;
		private long elapsedRender;
		private long elapsedDisplay;
		private long elapsedFrame;
		private int timeForFrame;
		private int delay;
		public VideoPlayer()
		{
			videoFile = new VideoFileReader();
			videoFile.Open("video.mp4");
			musicStream = File.OpenRead("music.wav");
			soundPlayer = new SoundPlayer(musicStream);
			musicThread = new Thread(new ThreadStart(soundPlayer.Play));
			stopwatchRender = new Stopwatch();
			stopwatchDisplay = new Stopwatch();
			stopwatchFrame = new Stopwatch();
			timeForFrame = 1000 / (int)Math.Truncate(videoFile.FrameRate.Value) - 2;
		}

		public void Start()
		{
			Console.CursorVisible = false;
			Console.Clear();
			musicThread.Start();
			while (currentFrameIndex < videoFile.FrameCount)
			{
				stopwatchFrame.Restart();
				stopwatchRender.Restart();
				currentFrame = ConvertToString(videoFile, currentFrameIndex);
				currentFrameIndex++;
				elapsedRender = stopwatchRender.ElapsedMilliseconds;
				stopwatchDisplay.Restart();
				DisplayFrame(currentFrame);
				elapsedDisplay = stopwatchDisplay.ElapsedMilliseconds;
				delay = CalculateDelay(elapsedDisplay, elapsedRender);
				Thread.Sleep(timeForFrame - delay);
				elapsedFrame = stopwatchFrame.ElapsedMilliseconds;
				DisplayStats(elapsedDisplay, elapsedRender, elapsedFrame);

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
		
		private int CalculateDelay(long elapsedDisplay, long elapsedRender)
		{
			
			return (elapsedRender + elapsedDisplay) > timeForFrame ? timeForFrame : (int)(elapsedRender + elapsedDisplay);
		}
		private void DisplayStats(long elapsedDisplay, long elapsedRender, long elapsedFrame)
		{
			Console.CursorVisible = false;
			Console.WriteLine();
			Console.WriteLine($"Render:{elapsedRender} ms ");
			Console.WriteLine($"Display:{elapsedDisplay} ms ");
			Console.WriteLine($"Frame:{elapsedFrame} ms ");
		}
		public void DisplayFrame(string[] frame)
		{
			Console.SetCursorPosition(0, 0);
			foreach (var row in frame)
			{
				Console.WriteLine(row);
			}
		}
	}
}
