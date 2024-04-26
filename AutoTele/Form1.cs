using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoTele
{
    public partial class Form1 : Form
    {
        String deviceID = "R9JN60HGC4J";
        Bitmap tele = (Bitmap) Image.FromFile("data//tele.png");
        List<Bitmap> numbers = ToolHeper.LoadImagesFromDirectory("data//numbers");
        Bitmap arrow = (Bitmap) Image.FromFile("data//arrow.png");
        Bitmap _checked = (Bitmap) Image.FromFile("data//checked.png");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //// click tele icon
            //try
            //{
            //    Bitmap screen = KAutoHelper.ADBHelper.ScreenShoot(deviceID);
            //    Point p = (Point)ToolHeper.FindOutPoint(screen, tele, 0.9);
            //    if (p != null)
            //    {
            //        KAutoHelper.ADBHelper.Tap(deviceID, p.X, p.Y);
            //    }
            //}catch(Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //// input number
            //try
            //{
            //    String number = "12323423";
            //    Bitmap screen = KAutoHelper.ADBHelper.ScreenShoot(deviceID);
            //    if(ToolHeper.ClickByImage(deviceID, screen, numbers, number))
            //    {
            //        Console.WriteLine("input text success");
            //    }
            //    else
            //    {
            //        Console.WriteLine("input text failed");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            //// tap in arrow icon 
            //try
            //{
            //    Bitmap screen = KAutoHelper.ADBHelper.ScreenShoot(deviceID);
            //    Point p = (Point)ToolHeper.FindOutPoint(screen, /*arrow*/_checked, 0.9);
            //    if (p != null)
            //    {
            //        KAutoHelper.ADBHelper.Tap(deviceID, p.X, p.Y);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            // get list username
            List<string> listUsername = GetListUsernam("data//usernames.txt");
            if(listUsername != null)
            {
                foreach(String username in listUsername)
                {
                    Console.WriteLine(username);
                }
            }
        }

        private List<string> GetListUsernam(String dir)
        {
            
            try
            {
                string[] lines = File.ReadAllLines(dir);
                return lines.ToList();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Error: File not found - " + dir);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading file: " + e.Message);
            }
            return null;
        }
    }
}
