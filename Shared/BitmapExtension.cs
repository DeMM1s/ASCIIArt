using System.Drawing;

namespace ASCIIArt
{
	static class BitmapExtension
	{
		public static void ToGrayscale(this Bitmap bitmap)
		{
			for (int y = 0; y < bitmap.Height - 1; y++)
			{
				for (int x = 0; x < bitmap.Width - 1; x++)
				{
					var pixel = bitmap.GetPixel(x, y);
					int avg = (pixel.R + pixel.G + pixel.B) / 3;
					bitmap.SetPixel(x, y, Color.FromArgb(pixel.A, avg, avg, avg));
				}
			}
		}
		public static Bitmap Resize(this Bitmap bitmap, int maxWidth, double widthOffset)
		{
			var newHeight = bitmap.Height / widthOffset * maxWidth / bitmap.Width;
			if (bitmap.Width > maxWidth || bitmap.Height > newHeight)
				bitmap = new Bitmap(bitmap, new Size(maxWidth, (int)newHeight));
			return bitmap;
		}
	}
}
