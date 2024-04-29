using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTele
{
    internal class FileReader
    {
        public static List<string> GetListUsernam(String dir)
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(dir);
                return lines.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
