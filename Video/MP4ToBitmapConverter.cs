using System.Drawing;
using Accord.Video.FFMPEG;

namespace ASCIIArt
{
    public class MP4ToBitmapConverter
    {
        public static Bitmap[] VideoToBitmapConvert(int start)
        {
            using (VideoFileReader vFReader = new VideoFileReader())
            {
                vFReader.Open("video.mp4");
                Bitmap[] frames = new Bitmap[vFReader.FrameCount];
                for (int i = start; i < start + 60; i++)
                {
                    frames[i] = vFReader.ReadVideoFrame(start);
                }
                vFReader.Close();
                return frames;
            }
        }
    }
}
