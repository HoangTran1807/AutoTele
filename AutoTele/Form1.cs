using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoTele
{
    public partial class Form1 : Form
    {
        int ThreadPerRound = 3;
        int TotalThread = 10;





        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }


        private static readonly ThreadLocal<Random> threadLocalRandom = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));

        public static int GetRandomNumber(int min, int max)
        {
            return threadLocalRandom.Value.Next(min, max);
        }

        private int dosomething(String deviceId)
        {
            int randomAction = GetRandomNumber(1, 3);
            int sleepTime = GetRandomNumber(1000, 5000);
            switch (randomAction)
            {
                case 1:
                    // do something
                    KAutoHelper.ADBHelper.Swipe(deviceId, 500, 500, 500, 1000);
                    break;
                case 2:
                    // do something
                    KAutoHelper.ADBHelper.Swipe(deviceId, 500, 1000, 500, 500);
                    break;
                default:
                    break;
            }
            Thread.Sleep(sleepTime);
            return randomAction;
            
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                String ldpath = "C:\\LDPlayer\\ldmutiplayer\\dnmultiplayerex.exe";

                if(ldpath != null)
                {
                    Process ldplayer = new Process();
                    ldplayer.StartInfo.FileName = ldpath;
                    ldplayer.Start();
                }
                else
                {
                    MessageBox.Show("Please install LDPlayer first!");
                }


            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            // start first ldplayer


            
        }


        private async void btn_createNew_Click(object sender, EventArgs e)
        {
            int states_thead = TotalThread / ThreadPerRound;

            for (int i = 0; i < states_thead; i++)
            {
                List<String> devices = KAutoHelper.ADBHelper.GetDevices();
                List<Task> tasks = new List<Task>();

                foreach (String device in devices)
                {
                    tasks.Add(Task.Run(() =>
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            rtxt_console.AppendText(device + " is running...\n");
                        });
                        // Run dosomething on a different thread
                        var result = dosomething(device);
                        this.Invoke((MethodInvoker)delegate
                        {
                            rtxt_console.AppendText(device + " do " + result + "\n");
                        });


                        this.Invoke((MethodInvoker)delegate
                        {
                            rtxt_console.AppendText(device + " is done...\n");
                        });
                    }));
                }

                await Task.WhenAll(tasks);
                rtxt_console.AppendText("Round " + i + " is done...\n");
            }
        }
    }
}
