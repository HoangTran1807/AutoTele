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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace AutoTele
{
    public partial class Form1 : Form
    {
        int ThreadPerRound = 3;
        List<String> LDInstanceNames = new List<string>();
        List<String> ListUsername = new List<string>();
        private static readonly ThreadLocal<Random> threadLocalRandom = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));
        static bool isRunning = false;




        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getListNameLDPlayer();
            LoadData();
        }

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

            int states_thead = LDInstanceNames.Count / ThreadPerRound;

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
            startMultiThread();
        }

        private async void startMultiThread()
        {
            
        }

        private async void btn_getList_Click(object sender, EventArgs e)
        {

            int numberOfRound = 3;/*(int)LDInstanceNames.Count / ThreadPerRound;*/
            isRunning = true;
            for (int i = 0; i < numberOfRound; i++)
            {
                if(!isRunning)
                {
                    break;
                }
                int startIndex = i * ThreadPerRound;
                int endIndex = (i + 1) * ThreadPerRound;
                for (int j = startIndex; j < endIndex; j++)
                {
                    OpenLDPlayer(LDInstanceNames[j]);
                }
                Thread.Sleep(30000);
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
                closeAll();


            }
        }

        private void OpenLDPlayer(String device)
        {
            string targetDir = @"C:\LDPlayer\LDPlayer9"; // Replace with your target directory path
            string command = "launchex --name "+device+" --packagename org.telegram.messenger.web"; // Replace with your actual command

            Process process = new Process();
            process.StartInfo.FileName = targetDir + @"\ldconsole.exe";
            process.StartInfo.Arguments = command;
            process.StartInfo.WorkingDirectory = null; // Use current directory
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;

            process.Start();

            StreamReader reader = process.StandardOutput;
            string outputLine;
            while ((outputLine = reader.ReadLine()) != null)
            {
                Console.WriteLine(outputLine);
            }

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                Console.WriteLine("Error: Process exited with code " + process.ExitCode);
            }
        }

        private void getListNameLDPlayer()
        {
            
            string targetDir = @"C:\LDPlayer\LDPlayer9"; // Replace with your target directory path
            string command = "list"; // Replace with your actual command

            Process process = new Process();
            process.StartInfo.FileName = targetDir + @"\ldconsole.exe";
            process.StartInfo.Arguments = command;
            process.StartInfo.WorkingDirectory = null; // Use current directory
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;

            process.Start();

            StreamReader reader = process.StandardOutput;
            string outputLine;
            while ((outputLine = reader.ReadLine()) != null)
            {
                LDInstanceNames.Add(outputLine);
            }

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                Console.WriteLine("Error: Process exited with code " + process.ExitCode);
            }
        }

        private void closeAll()
        {
            string targetDir = @"C:\LDPlayer\LDPlayer9"; // Replace with your target directory path
            string command = "quitall"; // Replace with your actual command

            Process process = new Process();
            process.StartInfo.FileName = targetDir + @"\ldconsole.exe";
            process.StartInfo.Arguments = command;
            process.StartInfo.WorkingDirectory = null; // Use current directory
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;

            process.Start();

            StreamReader reader = process.StandardOutput;
            string outputLine;
            while ((outputLine = reader.ReadLine()) != null)
            {
                LDInstanceNames.Add(outputLine);
            }

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                Console.WriteLine("Error: Process exited with code " + process.ExitCode);
            }
        }

        private void btn_getLDName_Click(object sender, EventArgs e)
        {
            getListNameLDPlayer();
        }

        private void LoadData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeviceName");
            dt.Columns.Add("UserName");

            foreach (String device in LDInstanceNames)
            {
                DataRow row = dt.NewRow();
                row["DeviceName"] = device;
                row["UserName"] = "Hoang";
                dt.Rows.Add(row);
            }

            dtgv_device_account.DataSource = dt;
        }

        private void dtgv_device_account_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dtgv_device_account_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            isRunning = false;
        }

        private void btn_end_Click(object sender, EventArgs e)
        {
            isRunning = false;
        }
    }
}
