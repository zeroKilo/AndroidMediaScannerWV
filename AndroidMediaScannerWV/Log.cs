using System.IO;
using System.Text;

namespace AndroidMediaScannerWV
{
    public static class Log
    {
        private static FileStream fs;

        public static void Create(string filename)
        {
            if (File.Exists(filename))
                File.Delete(filename);
            fs = new FileStream(filename, FileMode.Create);
        }

        public static void PrintLine(string s)
        {
            Print(s + '\n');
        }
        public static void Print(string s)
        {
            byte[] data = Encoding.UTF8.GetBytes(s);
            fs.Write(data, 0, data.Length);
            fs.Flush();
        }
    }
}
