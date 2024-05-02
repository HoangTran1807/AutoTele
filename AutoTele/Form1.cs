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
        Bitmap gr_chat = (Bitmap)Image.FromFile("data//gr_chat.png");
        Bitmap disablesend = (Bitmap)Image.FromFile("data//disablesend.png");




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
                    AutoJoinGr_Chat(deviceId,0);
                    break;
                case 2:
                    // do something
                    AutoJoinGr_Chat(deviceId,0);
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

            Task.Run(() => startMultiThread());
        }

        private async void startMultiThread()
        {
            int numberOfRound = 2;/*(int)LDInstanceNames.Count / ThreadPerRound;*/
            isRunning = true;

            for (int i = 0; i < numberOfRound; i++)
            {
                if (!isRunning)
                {
                    break;
                }
                int startIndex = i * ThreadPerRound;
                int endIndex = (i + 1) * ThreadPerRound;
                closeAll();
                for (int j = startIndex; j < endIndex; j++)
                {
                    OpenLDPlayer(LDInstanceNames[j]);
                    this.Invoke((MethodInvoker)delegate
                    {
                        rtxt_console.AppendText(LDInstanceNames[j] + " is opened...\n");
                    });
                    Thread.Sleep(6000);
                }
                Thread.Sleep(120000);
                List<String> devices = KAutoHelper.ADBHelper.GetDevices();
                List<Task> tasks = new List<Task>();
                foreach (String device in devices)
                {
                    if (String.IsNullOrEmpty(device))
                    {
                        break;
                    }
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
                this.Invoke((MethodInvoker)delegate
                {
                    rtxt_console.AppendText("Round " + i + " is done...\n");
                });
            }

            this.Invoke((MethodInvoker)delegate
            {
                rtxt_console.AppendText("All done...\n");
            });
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

        public async void AutoJoinGr_Chat(String deviceID,int i)
        {
            if (i == 4)
            {
                return;
            }
            KAutoHelper.ADBHelper.Tap(deviceID, 998, 145);
            //string[] gr = { "cryto gr", "freefire", "riot", "alumine", "golden star", "doremon" };
            string[] gr = { "cryto gr", "freefire", "riot", "alumine" };
            string[] chat = { "hi", "how are u", "lol", "haha", "555", "tui la con cho ngu" };
            // Tạo một đối tượng Random
            Random rand = new Random();

            // Chọn một chỉ mục ngẫu nhiên trong mảng
            int indexgr = rand.Next(0, gr.Length);
            int indexchat = rand.Next(0, chat.Length);

            // Lấy phần tử tại chỉ mục đã chọn
            string choosegr = gr[indexgr];
            string choosechat = chat[indexchat];
            KAutoHelper.ADBHelper.InputText(deviceID, choosegr);
            KAutoHelper.ADBHelper.Delay(2000);
            proc task1 = new proc();
            // click on the gr_chat icon
            if (task1.clickChildImage(gr_chat, deviceID))
            {
                Console.WriteLine("Click on the gr_chat icon");
                KAutoHelper.ADBHelper.Delay(2000);
                KAutoHelper.ADBHelper.Tap(deviceID, 530, 1852);
                KAutoHelper.ADBHelper.Delay(5000);
                Bitmap screen = KAutoHelper.ADBHelper.ScreenShoot(deviceID);
                screen.Save("aa.png");
                Point? checkdisable = task1.FindOutPoint(screen, disablesend);
                if (checkdisable == null)
                {
                    KAutoHelper.ADBHelper.Tap(deviceID, 276, 1855);
                    KAutoHelper.ADBHelper.InputText(deviceID, choosechat);
                    KAutoHelper.ADBHelper.Tap(deviceID, 1004, 1842);
                }
                else
                {
                    KAutoHelper.ADBHelper.Tap(deviceID, 71, 155);
                    AutoJoinGr_Chat(deviceID,i+1);
                }

            }
            else
            {
                Console.WriteLine("Cannot click on the gr_chat icon");
                AutoJoinGr_Chat(deviceID,i+1);
            }
        }
    }
}
