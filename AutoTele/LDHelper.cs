using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTele
{
    internal class LDHelper
    {
        public static List<String> ExecuteCMD(String LDPLAYER_FOLDER_PATH, String cmd)
        {
            List<string> lines = new List<string>();
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = LDPLAYER_FOLDER_PATH + @"\ldconsole.exe";
                process.StartInfo.Arguments = cmd;
                process.StartInfo.WorkingDirectory = null;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;

                process.Start();

                StreamReader reader = process.StandardOutput;
                string outputLine;
                
                while ((outputLine = reader.ReadLine()) != null)
                {
                    lines.Add(outputLine);
                    Console.WriteLine(outputLine);
                }

                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    Console.WriteLine("Error: Process exited with code " + process.ExitCode);
                }
                return lines;

            }
            catch (Exception ex)
            {
                Console.WriteLine("LDPlayer had not a instance take correct LDPlayer path");
            }
            return lines;
        }

    }
}
