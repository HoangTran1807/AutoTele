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
using ZedGraph;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace AutoTele
{
    public partial class Form1 : Form
    {
        // ========================================== VARIABLE ==========================================
        #region variable
        int ThreadPerRound = 2;

        private static readonly ThreadLocal<Random> threadLocalRandom = new ThreadLocal<Random>(() => new Random(Guid.NewGuid().GetHashCode()));
        static bool isRunning = false;

        private static readonly Random random = new Random();

        Bitmap gr_chat = (Bitmap)Image.FromFile("data//gr_chat.png");
        Bitmap disablesend = (Bitmap)Image.FromFile("data//disablesend.png");
        Bitmap dropdown = (Bitmap)Image.FromFile("data//dropdown.png");
        Bitmap login = (Bitmap)Image.FromFile("data//login.png");

        List<String> LDInstanceNames = new List<string>();
        List<String> SelectedLD = new List<string>();
        List<String> ListUsername = new List<string>();

        List<String> listChat = new List<string>();
        List<String> listGr = new List<string>();
        #endregion

        // ========================================== PATH ==========================================
        #region "Path"
        String START_UP = "data//startUp.txt";
        String ADB_FOLDER_PATH = "";
        String LDPLAYER_FOLDER_PATH = "";
        String CHAT_PATH = "";
        String GROUP_PATH = "";
        #endregion

        #region CountDown
        int TIME_WAIT_DRIVER = 12; // 12 * 10000
        int TIME_WAIT_TELEGRAM = 10; // 10 * 10000
        #endregion



        public Form1()
        {
            InitializeComponent();
            
        }

        public void startUp()
        {
           
            try
            {
                string[] lines = File.ReadAllLines(START_UP);
                foreach (String line in File.ReadAllLines(START_UP))
                {
                    if (line.Contains("ADB_FOLDER_PATH"))
                    {
                        ADB_FOLDER_PATH = line.Split('=')[1];

                    }
                    if (line.Contains("LDPLAYER_FOLDER_PATH"))
                    {

                        LDPLAYER_FOLDER_PATH = line.Split('=')[1];
                        if(!String.IsNullOrEmpty(LDPLAYER_FOLDER_PATH))
                        {
                            getListNameLDPlayer();
                            txt_ldplayer_path.Text = LDPLAYER_FOLDER_PATH;
                        }
                    }
                    if (line.Contains("CHAT_PATH"))
                    {
                        CHAT_PATH = line.Split('=')[1];
                        if(!String.IsNullOrEmpty (CHAT_PATH))
                        {
                            LoadListChat();
                            txt_chatpath.Text = CHAT_PATH;
                        }
                    }
                    if (line.Contains("GROUP_PATH"))
                    {
                        GROUP_PATH = line.Split('=')[1];
                        if(!String.IsNullOrEmpty(GROUP_PATH))
                        {
                            LoadListGroup();
                            txt_grouppath.Text = GROUP_PATH;
                        }
                                
                    }
                    if (line.Contains("THREAD_PER_ROUND"))
                    {
                        ThreadPerRound = Int32.Parse(line.Split('=')[1]);
                        ThreadPerRound = ThreadPerRound == 0 ? 2 : ThreadPerRound;
                        numericUpDown1.Value = this.ThreadPerRound;
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Error: File not found - " + START_UP);
            }

            
            

        }

        public void saveStartUp()
        {
            List<String> lines = new List<string>();
            lines.Add("ADB_FOLDER_PATH=" + ADB_FOLDER_PATH);
            lines.Add("LDPLAYER_FOLDER_PATH=" + LDPLAYER_FOLDER_PATH);
            lines.Add("CHAT_PATH=" + CHAT_PATH);
            lines.Add("GROUP_PATH=" + GROUP_PATH);
            lines.Add("THREAD_PER_ROUND=" + ThreadPerRound);
            File.WriteAllLines(START_UP, lines);
        }

        // ========================================== EVENT ==========================================
        #region event

        private void Form1_Load(object sender, EventArgs e)
        {
            startUp();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            saveStartUp();
            isRunning = false;
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            if(ThreadPerRound == 0)
            {
                MessageBox.Show("Please input number of thread per round");
                return;
            }
            Task.Run(() => startMultiThread());
        }
        private void btn_end_Click(object sender, EventArgs e)
        {
            isRunning = false;
            MessageBox.Show("process will be stopped after all threads are done");
        }

        public void btn_checkTele_Click(object sender, EventArgs e)
        {
            List<String> devices = KAutoHelper.ADBHelper.GetDevices();
            foreach (String device in devices)
            {
                if (IsInstalledTelegram(device))
                {
                    MessageBox.Show(device + " is installed");
                }
                else
                {
                    MessageBox.Show(device + " is not installed");
                }
            }
        }

        private void btn_selectChatPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "All files (*.txt*)|*.txt*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                txt_chatpath.Text = openFileDialog1.FileName;
            if (!String.IsNullOrEmpty(txt_chatpath.Text.Trim()))
            {
                CHAT_PATH = txt_chatpath.Text;
                rtxt_chats.Clear();
                LoadListChat();
               
            }
        }




        private void btn_selectGroupPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "All files (*.txt*)|*.txt*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                txt_grouppath.Text = openFileDialog1.FileName;
            if(!String.IsNullOrEmpty(txt_grouppath.Text.Trim()))
            {
                GROUP_PATH = txt_grouppath.Text;
                rtxt_groups.Clear();
                LoadListGroup();
            }

        }

        private void btn_selectLD_Click(object sender, EventArgs e)
        {
            string folderPath = "";
            FolderBrowserDialog directchoosedlg = new FolderBrowserDialog();
            if (directchoosedlg.ShowDialog() == DialogResult.OK)
            {
                folderPath = directchoosedlg.SelectedPath;
            }
            txt_ldplayer_path.Text = folderPath;
            if(!String.IsNullOrEmpty(txt_ldplayer_path.Text.Trim()))
            {
                LDPLAYER_FOLDER_PATH = txt_ldplayer_path.Text;
                getListNameLDPlayer();

            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ThreadPerRound = (int)numericUpDown1.Value;
        }

        private void installTeleForAllDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ThreadPerRound == 0)
            {
                MessageBox.Show("Please input number of thread per round");
                return;
            }
            // return if no device is selected
            if (LDInstanceNames.Count == 0)
            {
                MessageBox.Show("Please select LDPlayer first!");
                return;
            }
            Task.Run(() => InstallTeleForAll());
        }

        private void btn_selectedLD_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeviceName");
            SelectedLD.Clear();
            foreach (DataGridViewRow row in dtgv_device_account.Rows)
            {
                // Kiểm tra xem dòng hiện tại có được chọn không
                DataGridViewCheckBoxCell cell = row.Cells["Select"] as DataGridViewCheckBoxCell;
                if (cell != null && cell.Value != null && (bool)cell.Value == true)
                {
                    // Lấy giá trị của cột "DeviceName" và in ra console
                    SelectedLD.Add(row.Cells["DeviceName"].Value.ToString());
                    dt.Rows.Add(row.Cells["DeviceName"].Value.ToString());
                }
            }
            dataGridView1.DataSource = dt;
        }

        private void configForAllDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {


            foreach (String device in SelectedLD)
            {
                configForDriver(device);
            }
        }

        private void btn_selectedAll_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dtgv_device_account.DataSource;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    row["Select"] = true;
                }
            }

            dataGridView1.DataSource = dt;
            btn_selectedLD_Click(sender, e);
        }

        private void btn_ignoreLD_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dtgv_device_account.DataSource;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    row["Select"] = false;
                }
            }

            dataGridView1.DataSource = dt;
            btn_selectedLD_Click(sender, e);
        }

        private void addMoreInstanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!BoolMessageBox("You want add 5 instance of LDPlayer"))
                return;
            for (int i = 0; i < 5; i++)
            {
                AddMoreInstance();

            }
            getListNameLDPlayer();
        }

        

        #endregion

        // ========================================== METHOD ==========================================
        #region method

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
                    AutoJoinGr_Chat(deviceId, 0,1,true);
                    break;
                case 2:
                    // do something
                    AutoJoinGr_Chat(deviceId, 0,1,true);
                    break;
                default:
                    break;
            }
            Thread.Sleep(sleepTime);
            return randomAction;

        }



        private List<string> TXTReader(String dir)
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
            return new List<string>();
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        String ldpath = "C:\\LDPlayer\\ldmutiplayer\\dnmultiplayerex.exe";

        //        if (ldpath != null)
        //        {
        //            Process ldplayer = new Process();
        //            ldplayer.StartInfo.FileName = ldpath;
        //            ldplayer.Start();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Please install LDPlayer first!");
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //    }
        //}


        private async void startMultiThread()
        {
            int deviceCount = SelectedLD.Count;
            // return if no device is selected
            if (deviceCount == 0)
            {
                MessageBox.Show("Please open LDPlayer first!");
                return;
            }

            int numberOfRound = (int) Math.Ceiling((double)deviceCount / ThreadPerRound);
            isRunning = true;

            for (int i = 0; i < numberOfRound; i++)
            {
                // exit if stop button is clicked
                if (!isRunning)
                {
                    break;
                }
                int startIndex = i * ThreadPerRound;
                int endIndex = (i + 1) * ThreadPerRound;
                if(endIndex > deviceCount)
                {
                    endIndex = deviceCount;
                }
                closeAll();
                for (int j = startIndex; j < endIndex; j++)
                {
                    OpenLDPlayer(SelectedLD[j]);
                    this.Invoke((MethodInvoker)delegate
                    {
                        rtxt_console.AppendText(SelectedLD[j] + " is opened...\n");
                    });
                }

                int numberOfDevice = endIndex - startIndex;
                List<String> devices = new List<String>();
                int count = 0;
                while (devices.Count < numberOfDevice)
                {
                    if (count >= TIME_WAIT_DRIVER)
                    {
                        break;
                    }
                    devices = KAutoHelper.ADBHelper.GetDevices();
                    Thread.Sleep(10000);
                    Console.WriteLine("Waiting for devices all device online");
                    count++;
                }

                List<Task> tasks = new List<Task>();
                foreach (String device in devices)
                {
                    // exit if no device
                    if (String.IsNullOrEmpty(device))
                        break;
                    // run if device is not installed telegram
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

            closeAll();
        }

        private async void InstallTeleForAll()
        {
            
            int deviceCount = SelectedLD.Count;
            

            int numberOfRound = (int)Math.Ceiling((double)deviceCount / ThreadPerRound);
            isRunning = true;

            for (int i = 0; i < numberOfRound; i++)
            {
                // exit if stop button is clicked
                if (!isRunning)
                {
                    break;
                }
                int startIndex = i * ThreadPerRound;
                int endIndex = (i + 1) * ThreadPerRound;
                if(endIndex > deviceCount)
                {
                    endIndex = deviceCount;
                }
                closeAll();
                for (int j = startIndex; j < endIndex; j++)
                {
                    OpenLDPlayer(SelectedLD[j]);
                    this.Invoke((MethodInvoker)delegate
                    {
                        rtxt_console.AppendText(SelectedLD[j] + " is opened...\n");
                    });
                }

                int numberOfDevice = endIndex - startIndex;
                List<String> devices = new List<String>();
                int count = 0;
                while (devices.Count < numberOfDevice)
                {
                    if(count >= TIME_WAIT_DRIVER)
                    {
                        break;
                    }
                    devices = KAutoHelper.ADBHelper.GetDevices();
                    Thread.Sleep(10000);
                    Console.WriteLine("Waiting for devices all device online");
                    count++;
                }
                
                List<Task> tasks = new List<Task>();
                foreach (String device in devices)
                {
                    // exit if no device
                    if (String.IsNullOrEmpty(device))
                        break;
                    // run if device is not installed telegram
                    tasks.Add(Task.Run(() =>
                    {
                        Console.WriteLine(device + "is online");
                        if(!IsInstalledTelegram(device))
                        {
                            int ncount = 0;
                            autoInstallTelegram(device);
                            while(!IsInstalledTelegram(device))
                            {
                                if(ncount >= TIME_WAIT_TELEGRAM)
                                {
                                    break;
                                }
                                Thread.Sleep(1000);
                                ncount++;
                            }
                        }
                        if(IsInstalledTelegram(device))
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                rtxt_console.AppendText(device + " is installed...\n");
                            });
                        }
                        else
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                rtxt_console.AppendText(device + " is not installed...\n");
                            });
                        }
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

            closeAll();
        }

        private void OpenLDPlayer(String device)
        {

            string targetDir = LDPLAYER_FOLDER_PATH; 
            string command = "launchex --name " + device + " --packagename org.telegram.messenger.web";
            LDHelper.ExecuteCMD(LDPLAYER_FOLDER_PATH, command);
        }

        private bool BoolMessageBox(String info, String title = "")
        {
            var option = MessageBox.Show(info, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (option == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }

        private void getListNameLDPlayer()
        {
            LDInstanceNames.Clear();
            string command = "list";
            List<String> ldnames =  LDHelper.ExecuteCMD(LDPLAYER_FOLDER_PATH, command);
            if(ldnames.Count > 0)
            {
                foreach(String device in ldnames)
                {
                    LDInstanceNames.Add(device);
                    RenderLDInstance();
                }
            }

        }

        private void closeAll()
        {
            string command = "quitall";
            LDHelper.ExecuteCMD(LDPLAYER_FOLDER_PATH, command);
        }


        




        //i là biến để tính số lần lặp mặc định truyền vô là 0, numberacc là số thứ tự tài khoản mặc định truyền vô là 1, switchacc mặc định truyền vô là true
        public async void AutoJoinGr_Chat(String deviceID, int i, int numberAcc, bool switchacc)
        {
            //check thu tu tai khoan
            if (numberAcc > 3)
            {
                return;
            }

            //check so lan chat của từng tai khoan
            if (i >= 4)
            {
                AutoJoinGr_Chat(deviceID, 0, numberAcc + 1, true);
                return;
            }
            proc task1 = new proc();

            //check có cần đổi tài khoản hay ko
            if (switchacc == true)
            {
                KAutoHelper.ADBHelper.Tap(deviceID, 71, 155);
                KAutoHelper.ADBHelper.Delay(7000);
                task1.clickChildImage(dropdown, deviceID);
                KAutoHelper.ADBHelper.Delay(2000);
                switch (numberAcc)
                {
                    case 1:
                        KAutoHelper.ADBHelper.Tap(deviceID, 305, 610);
                        break;
                    case 2:
                        KAutoHelper.ADBHelper.Tap(deviceID, 302, 763);
                        break;
                    case 3:
                        KAutoHelper.ADBHelper.Tap(deviceID, 413, 912);
                        break;
                    default:
                        return;
                }
                KAutoHelper.ADBHelper.Delay(2000);
            }
            KAutoHelper.ADBHelper.Tap(deviceID, 998, 145);

            // Tạo một đối tượng Random
            Random rand = new Random();

            // Chọn một chỉ mục ngẫu nhiên trong mảng
            int indexgr = rand.Next(0, listGr.Count);
            int indexchat = rand.Next(0, listChat.Count);

            // Lấy phần tử tại chỉ mục đã chọn
            string choosegr = listChat[indexgr];
            string choosechat = listGr[indexchat];


            KAutoHelper.ADBHelper.InputText(deviceID, choosegr);
            KAutoHelper.ADBHelper.Delay(10000);
            // tìm gr chat 

            Bitmap screen4 = KAutoHelper.ADBHelper.ScreenShoot(deviceID);
            Point? checkgr_chat = task1.FindOutPoint(screen4, gr_chat);


            if (checkgr_chat != null )
            {
                task1.clickChildImage(gr_chat, deviceID);
                KAutoHelper.ADBHelper.Delay(7000);
                Bitmap screen1 = KAutoHelper.ADBHelper.ScreenShoot(deviceID);
                Point? checklogin = task1.FindOutPoint(screen1, login);
                if (checklogin != null)
                {
                    return;
                }
                Console.WriteLine("Click on the gr_chat icon");
                KAutoHelper.ADBHelper.Delay(2000);
                KAutoHelper.ADBHelper.Tap(deviceID, 530, 1852);
                KAutoHelper.ADBHelper.Delay(10000);
                Bitmap screen2 = KAutoHelper.ADBHelper.ScreenShoot(deviceID);
                Point? checkdisable = task1.FindOutPoint(screen2, disablesend);
                if (checkdisable == null)
                {
                    KAutoHelper.ADBHelper.Delay(random.Next(10000, 30001));
                    KAutoHelper.ADBHelper.Tap(deviceID, 276, 1855);
                    KAutoHelper.ADBHelper.Delay(random.Next(1000, 2000));
                    KAutoHelper.ADBHelper.InputText(deviceID, choosechat);

                    KAutoHelper.ADBHelper.Delay(random.Next(1000, 2000));
                    KAutoHelper.ADBHelper.Tap(deviceID, 1004, 1842);
                    KAutoHelper.ADBHelper.Delay(random.Next(1000, 2000));
                    KAutoHelper.ADBHelper.Tap(deviceID, 71, 155);
                    this.Invoke((MethodInvoker)delegate
                    {
                        rtxt_console.AppendText(device + " is not installed...\n");
                    });
                    KAutoHelper.ADBHelper.Delay(random.Next(120000, 300000));
                    AutoJoinGr_Chat(deviceID, i + 1, numberAcc, false);
                }
                else
                {
                    KAutoHelper.ADBHelper.Delay(random.Next(1000, 2000));
                    KAutoHelper.ADBHelper.Tap(deviceID, 71, 155);
                    KAutoHelper.ADBHelper.Delay(random.Next(1000, 2000));
                    AutoJoinGr_Chat(deviceID, i + 1, numberAcc, false);
                }

            }
            else
            {
                Console.WriteLine("Cannot click on the gr_chat icon");
                KAutoHelper.ADBHelper.Tap(deviceID, 71, 155);
                KAutoHelper.ADBHelper.Delay(random.Next(1000, 2000));
                AutoJoinGr_Chat(deviceID, i + 1, numberAcc, false);
            }
        }


        private void LoadListChat()
        {
            listChat = TXTReader(CHAT_PATH);
            RenderChat();
        }

        private void LoadListGroup()
        {
            listGr = TXTReader(GROUP_PATH);
            RenderGroup();
        }

        public void RenderChat()
        {

            if (listChat.Count > 0)
                foreach (String chat in listChat)
                {
                    rtxt_chats.AppendText(chat + "\n");
                }
        }

        public void RenderGroup()
        {
            if (listGr.Count > 0)
                foreach (String group in listGr)
                {
                    rtxt_groups.AppendText(group + "\n");
                }
        }
        private void RenderLDInstance()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DeviceName");
            dt.Columns.Add("Select", typeof(bool));

            foreach (String device in LDInstanceNames)
            {
                DataRow row = dt.NewRow();
                row["DeviceName"] = device;
                row["Select"] = false;
                dt.Rows.Add(row);
            }

            dtgv_device_account.DataSource = dt;
        }

        public String ExecuteCMD(string cmdCommand)
        {
            try
            {
                Process process = new Process();
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.WorkingDirectory = ADB_FOLDER_PATH;
                processStartInfo.FileName = "cmd.exe";
                processStartInfo.CreateNoWindow = true;
                processStartInfo.UseShellExecute = false;
                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processStartInfo.RedirectStandardInput = true;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.Verb = "runas";
                process.StartInfo = processStartInfo;
                process.Start();
                process.StandardInput.WriteLine(cmdCommand);
                process.StandardInput.Flush();
                process.StandardInput.Close();
                process.WaitForExit();
                return process.StandardOutput.ReadToEnd();
            }
            catch
            {
                return null;
            }
        }

        public bool autoInstallTelegram(String deviceID)
        {
            string command = "adb -s " + deviceID + " install Telegram.apk";

            String result = ExecuteCMD(command).Replace(command, "");
            if (result.Contains("Success"))
            {
                return true;
            }
            return false;
        }

        public bool IsInstalledTelegram(String deviceID)
        {
            string command = "adb -s " + deviceID + " shell pm list packages | findstr org.telegram.messenger.web";

            String result = ExecuteCMD(command).Replace(command, "");
            if (result.Contains("org.telegram.messenger.web"))
            {
                return true;
            }
            return false;
        }

        public void InstallTelegramForAllDevices()
        {
            List<String> devices = KAutoHelper.ADBHelper.GetDevices();
            foreach (String device in devices)
            {
                if (!IsInstalledTelegram(device))
                {
                    if (autoInstallTelegram(device))
                    {
                        MessageBox.Show("Install success on " + device);
                    }
                    else
                    {
                        MessageBox.Show("Install failed on " + device);
                    }
                }
            }
        }

        

        private void configForDriver(String deviceID)
        {

            string command = "modify --name " + deviceID + @" --resolution ""1080,1920,480"" --cpu 4 --memory 2048 --manufacturer samsung --model SM-G973N"; ;
            LDHelper.ExecuteCMD(LDPLAYER_FOLDER_PATH, command);

        }

        private void AddMoreInstance()
        {
            string command = "add LDPlayer";
            LDHelper.ExecuteCMD(LDPLAYER_FOLDER_PATH, command);
            getListNameLDPlayer();
            String lastDevice = LDInstanceNames.LastOrDefault();
            if(lastDevice != null)
            {
                configForDriver(lastDevice);
            }
        }









        #endregion

    }
}
