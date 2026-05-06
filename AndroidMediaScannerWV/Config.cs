using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AndroidMediaScannerWV
{
    public static class Config
    {
        public static Dictionary<string, string> Settings = new Dictionary<string, string>();
        public static void Load()
        {
            Settings = new Dictionary<string, string>();
            if (File.Exists("config.txt"))
            {
                string[] lines = File.ReadAllLines("config.txt");
                foreach (string line in lines)
                {
                    string[] parts = line.Split('=');
                    if (parts.Length == 2)
                        Settings.Add(parts[0].Trim().ToLower(), parts[1].Trim());
                }
            }
        }

        public static void Save()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> p in Settings)
                sb.AppendLine(p.Key + " = " + p.Value);
            File.WriteAllText("config.txt", sb.ToString());
        }

        public static string GetString(string key)
        {
            if(Settings.ContainsKey(key))
                return Settings[key];
            return "";
        }

        public static bool GetBoolean(string key)
        {
            if (Settings.ContainsKey(key))
                return Settings[key] == "1";
            return false;
        }

        public static int GetInt(string key)
        {
            if (Settings.ContainsKey(key))
                return int.Parse(Settings[key]);
            return 0;
        }
    }
}
