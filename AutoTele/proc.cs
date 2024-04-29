using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoTele
{
    internal class proc
    {

        public bool clickChildImage(Bitmap screen, Bitmap child, String deviceID)
        {
            // tap in arrow icon 
            try
            {
                Point p = (Point)FindOutPoint(screen, child, 0.9);
                if (p != null)
                {
                    KAutoHelper.ADBHelper.Tap(deviceID, p.X, p.Y);
                }
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool clickChildImage(Bitmap child, String deviceID)
        {
            try
            {
                Bitmap screen = KAutoHelper.ADBHelper.ScreenShoot(deviceID);
                Point p = (Point)FindOutPoint(screen, child, 0.9);
                if (p != null)
                {
                    KAutoHelper.ADBHelper.Tap(deviceID, p.X, p.Y);
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool clickChildImage(List<Point> childs, String deviceID, String imageIndex)
        {
            foreach(char c in imageIndex)
            {
                int index = int.Parse(c.ToString());
                KAutoHelper.ADBHelper.TapByPercent(deviceID, childs[index].X, childs[index].Y);

            }
            return true;
        }


        public bool clickChildImage(Bitmap screen, Bitmap child,  String deviceID, int number)
        {
            try
            {
                Point p = (Point)FindOutPoint(screen, child, 0.9);
                if (p != null)
                {
                    for(int i = 0; i < number; i++){
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

        public Point? FindOutPoint(Bitmap mainBitmap, Bitmap subBitmap, double percent = 0.9)
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

        public bool findOutPoint(Bitmap screen, Bitmap child, double percent)
        {
            for (int i = 0; i < screen.Width - child.Width; i++)
            {
                for (int j = 0; j < screen.Height - child.Height; j++)
                {
                    if (isChild(screen, child, i, j, percent))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool isChild(Bitmap screen, Bitmap child, int x, int y, double percent)
        {
            int count = 0;
            for (int i = 0; i < child.Width; i++)
            {
                for (int j = 0; j < child.Height; j++)
                {
                    if (isSimilar(screen.GetPixel(x + i, y + j), child.GetPixel(i, j)))
                    {
                        count++;
                    }
                }
            }
            return count * 1.0 / (child.Width * child.Height) >= percent;
        }

        public bool isSimilar(Color c1, Color c2)
        {
            return Math.Abs(c1.R - c2.R) + Math.Abs(c1.G - c2.G) + Math.Abs(c1.B - c2.B) < 30;
        }




    }
}
