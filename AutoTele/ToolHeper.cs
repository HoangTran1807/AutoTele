using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AutoTele
{
    internal class ToolHeper
    {
        public static Point? FindOutPoint(Bitmap mainBitmap, Bitmap subBitmap, double percent = 0.9)
        {
            if (subBitmap == null || mainBitmap == null)
            {
                return null;
            }

            if (subBitmap.Width > mainBitmap.Width || subBitmap.Height > mainBitmap.Height)
            {
                return null;
            }

            Image<Bgr, byte> image = new Image<Bgr, byte>(mainBitmap);
            Image<Bgr, byte> template = new Image<Bgr, byte>(subBitmap);
            Point? result = null;
            using (Image<Gray, float> image2 = image.MatchTemplate(template, TemplateMatchingType.CcoeffNormed))
            {
                image2.MinMax(out var _, out var maxValues, out var _, out var maxLocations);
                if (maxValues[0] > percent)
                {
                    result = maxLocations[0];
                }
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            return result;
        }


        public static List<Bitmap> LoadImagesFromDirectory(string directoryPath)
        {
            List<Bitmap> images = new List<Bitmap>();

            try
            {
                foreach (string filePath in Directory.EnumerateFiles(directoryPath, "*.png", SearchOption.AllDirectories))
                {
                    Bitmap image = new Bitmap(filePath);
                    images.Add(image);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading images: " + e.Message);
            }

            return images;
        }

        public static bool ClickByImage(string deviceID, Bitmap screen, List<Bitmap> bitmap, String inputs)
        {
            try
            {
                foreach (char c in inputs)
                {
                    int digit = int.Parse(char.ToString(c));
                    Bitmap numberBitmap = bitmap[digit];
                    Point p = (Point)ToolHeper.FindOutPoint(screen, numberBitmap, 0.9);
                    if (p != null)
                    {
                        KAutoHelper.ADBHelper.Tap(deviceID, p.X, p.Y);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }
    }
}
