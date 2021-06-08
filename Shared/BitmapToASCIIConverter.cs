using System.Drawing;

namespace ASCIIArt
{
	static class BitmapToASCIIConverter
	{
		private static readonly char[] asciiTable = { ' ', '.', '-', ':', '*', '+', '=', '%', '@' };
		private static readonly char[] asciiTableNegative = { '@', '%', '=', '+', '*', ':', '-', '.', ' ' };

		public static char[][] Convert(Bitmap bitmap)
		{
			return Convert(bitmap, asciiTable);
		}
		public static char[][] ConvertNegative(Bitmap bitmap)
		{
			return Convert(bitmap, asciiTableNegative);
		}

		public static char[][] ConvertToBlackAndWhite(Bitmap bitmap)
		{
			var result = new char[bitmap.Height][];

			for (int y = 0; y < bitmap.Height; y++)
			{
				result[y] = new char[bitmap.Width];
				for (int x = 0; x < bitmap.Width; x++)
				{
					char asciiPixel = bitmap.GetPixel(x, y).R == 255 ? '@' : ' ';
					result[y][x] = asciiPixel;
				}
			}
			return result;
		}
		private static char[][] Convert(Bitmap bitmap, char[] asciiTable)
		{
			var result = new char[bitmap.Height][];

			for (int y = 0; y < bitmap.Height; y++)
			{
				result[y] = new char[bitmap.Width];
				for (int x = 0; x < bitmap.Width; x++)
				{
					int mapIndex = (int)Map(bitmap.GetPixel(x, y).R, 0, 255, 0, asciiTable.Length - 1);
					result[y][x] = asciiTable[mapIndex];
				}
			}
			return result;
		}
		private static float Map(float valueToMap, float start1, float stop1, float start2, float stop2)
		{
			return (valueToMap - start1) / (stop1 - start1) * (stop2 - start2) + start2;
		}
	}
}
