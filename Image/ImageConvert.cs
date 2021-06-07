using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace ASCIIArt.Image
{
    public class ImageConvert
    {
        private const double WIDTH_OFFSET = 3.15;
        private const int MAX_WIDTH = 400;

        public static void Start()
		{
            Bitmap image = new Bitmap("image.jpg");
            image = image.Resize(MAX_WIDTH, WIDTH_OFFSET);
            image.ToGrayscale();
            char[][] imageAscii = BitmapToASCIIConverter.Convert(image);
            char[][] imageNegativeAscii = BitmapToASCIIConverter.ConvertNegative(image);
            File.WriteAllLines("imageAscii.txt", imageAscii.Select(r => new string(r)));
            File.WriteAllLines("imageNegativeAscii.txt", imageNegativeAscii.Select(r => new string(r)));
        }
        
       
    }
}
