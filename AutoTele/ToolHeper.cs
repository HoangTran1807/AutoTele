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
using System.Windows.Forms;
using OpenTK.Graphics.ES10;
using Emgu.CV.Util;
using KAutoHelper;
using Tesseract;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;

namespace AutoTele
{
    internal class ToolHeper
    {
        private String deviceID;
        String authorization = "1905|Xa2QnQFRF2YHwguPvN3KjRaB4Ukcwc7nBTvp8NnF370d5afa";
        Bitmap tele = (Bitmap)Image.FromFile("data//tele.png");
        //Bitmap banned = (Bitmap)Image.FromFile("data//banned.png");
        Bitmap CheckCode = (Bitmap)Image.FromFile("data//CheckCode.png");
        Point arrow = new Point(85, 55);
        Point delete = new Point(82, 94);
        List<Point> numbers = new List<Point>();

        public void loadNumbers()
        {
            numbers.Add(new Point(50, 93));
            numbers.Add(new Point(18, 68));
            numbers.Add(new Point(50, 68));
            numbers.Add(new Point(82, 68));
            numbers.Add(new Point(18, 77));
            numbers.Add(new Point(50, 77));
            numbers.Add(new Point(82, 77));
            numbers.Add(new Point(18, 86));
            numbers.Add(new Point(50, 86));
            numbers.Add(new Point(82, 86));

        }

        public ToolHeper(String deviceID, String authorization)
        {
            this.deviceID = deviceID;
            this.authorization = authorization;
            tele = (Bitmap)Image.FromFile("data//tele.png");
            CheckCode = (Bitmap)Image.FromFile("data//CheckCode.png");
            arrow = new Point(85, 55);
            delete = new Point(82, 94);
            numbers = new List<Point>();
            loadNumbers();
        }
        private async void CreateNewAccount(String deviceID)
        {
            proc task1 = new proc();
            // click on the tele icon
            if (task1.clickChildImage(tele, deviceID))
                Console.WriteLine("Click on the tele icon");
            else
            {
                Console.WriteLine("Cannot click on the tele icon");
                return;
            }
            // click on the start messaging button
            KAutoHelper.ADBHelper.Delay(4000);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 48.2, 86.5);
            // click in text box field
            KAutoHelper.ADBHelper.TapByPercent(deviceID, 77.4, 49.6);

            // click on the delete button
            for (int i = 0; i < 12; i++)
            {
                KAutoHelper.ADBHelper.Tap(deviceID, delete.X, delete.Y);
            }

            // paste phone number;
            String phoneAndTransid = await getPhoneNumberAPIAsync(); //85254712144 //639750783448
            String[] phoneAndTransidArr = phoneAndTransid.Split(':');
            if (phoneAndTransid.Length < 0)
            {
                Console.WriteLine("cant not get phone number from API");
                return;
            }
            if (String.IsNullOrEmpty(phoneAndTransidArr[0]))
            {
                Console.WriteLine("cant not get phone number from API");
                return;
            }
            KAutoHelper.ADBHelper.InputText(deviceID, phoneAndTransidArr[0]);

            // click on the arrow icon
            KAutoHelper.ADBHelper.TapByPercent(deviceID, arrow.X, arrow.Y);
            KAutoHelper.ADBHelper.TapByPercent(deviceID, arrow.X, arrow.Y);

            KAutoHelper.ADBHelper.Delay(3000);
            // check code
            if (phoneAndTransid.Length < 2)
            {
                Console.WriteLine("cant not get transaction number from API");
                return;
            }
            //waitting verify code
            Console.WriteLine("Waitting verify code");
            KAutoHelper.ADBHelper.Delay(8000);
            String code = await GetPhoneCodeAsync(phoneAndTransidArr[1]);
            if (String.IsNullOrEmpty(code))
            {
                Console.WriteLine("cant not get phone code from API");
                return;
            }
            KAutoHelper.ADBHelper.InputText(deviceID, code);

            Console.Write("create Account success");

            // wa
        }



        private async Task<double> getBalance()
        {
            String url = "https://dailyotp.com/api/my-balance";
            double balance = 0;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                // Set authorization header
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + authorization);
                // Set accept header
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                try
                {
                    // Make the GET request asynchronously
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Check for successful response
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();

                        // Parse the JSON response
                        dynamic jsonData = JsonConvert.DeserializeObject(responseString);

                        // Extract phone number and transId
                        balance = jsonData.data.balance;
                    }
                    else
                    {
                        Console.WriteLine($"Error occurred: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Message}");
                }
                return balance;

            }
        }
        private async Task<string> getPhoneNumberAPIAsync()
        {
            String result = "";
            string url = "https://dailyotp.com/api/rent-number/?";
            var queryParams = new Dictionary<string, string>()
            {
                ["appBrand"] = "Telegram",
                ["countryCode"] = "US",
                ["serverName"] = "Server 1"
            };

            // Build the URL with query string parameters
            url = url + string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={kvp.Value}"));

            using (HttpClient client = new HttpClient())
            {
                // Set authorization header
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authorization}");

                // Set accept header
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                try
                {
                    // Make the GET request asynchronously
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Check for successful response
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();

                        // Parse the JSON response
                        dynamic jsonData = JsonConvert.DeserializeObject(responseString);

                        // Extract phone number and transId
                        string phoneNumber = jsonData.data.phoneNumber;
                        string transId = jsonData.data.transId;

                        // Display extracted data (replace with your logic)
                        result = phoneNumber + ":" + transId;
                    }
                    else
                    {
                        Console.WriteLine($"Error occurred: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Message}");
                }
            }
            return result;
        }

        private async Task<string> GetPhoneCodeAsync(String transId)
        {
            string code = "";
            String url = "https://dailyotp.com/api/get-messages?transId=" + transId;
            using (HttpClient client = new HttpClient())
            {
                // Set authorization header
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authorization}");

                // Set accept header
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                try
                {
                    // Make the GET request asynchronously
                    HttpResponseMessage response = await client.GetAsync(url);

                    // Check for successful response
                    if (response.IsSuccessStatusCode)
                    {
                        string responseString = await response.Content.ReadAsStringAsync();

                        // Parse the JSON response
                        dynamic jsonData = JsonConvert.DeserializeObject(responseString);

                        // Extract phone number and transId
                        code = jsonData.data.code;
                    }
                    else
                    {
                        Console.WriteLine($"Error occurred: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error occurred: {ex.Message}");
                }
            }
            return code;
        }






    }
}
