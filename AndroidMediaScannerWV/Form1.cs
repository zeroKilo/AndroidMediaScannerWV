using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AndroidMediaScannerWV
{
    public partial class Form1 : Form
    {
        public class SearchEntry
        {
            public PortableDeviceFile File;
            public PortableDeviceFolder Drive;
            public PortableDevice Device;
            public string Path;

            public SearchEntry(PortableDeviceFile file, PortableDevice device)
            {
                File = file;
                Device = device;
                Path = file.FullPath();
            }
        }
        public PortableDeviceCollection Devices = new PortableDeviceCollection();
        public List<SearchEntry> SearchResult;
        public Dictionary<string, string[]> ScanProfiles;
        public Dictionary<string, string[]> Languages;
        public string[] CurrentLanguage;
        public string lastOutputFolder = "";

        public Form1()
        {
            InitializeComponent();
            Log.Create("log.txt");
            Log.PrintLine("Log started");
            Config.Load();
            LoadProfiles();
            LoadLanguages();
            RefreshDrives();
            lastOutputFolder = Config.GetString("last_path");
            textBox1.Text = lastOutputFolder;
            comboBox1.SelectedIndex = Config.GetInt("export_structure");
            comboBox2.SelectedIndex = Config.GetInt("export_selection");
            checkBox4.Checked = Config.GetBoolean("overwrite");
        }

        private void LoadLanguages()
        {
            Log.PrintLine("Loading languages...");
            Languages = new Dictionary<string, string[]>();
            string[] files = Directory.GetFiles("lang", "*.txt");
            toolStripComboBox2.Items.Clear();
            int selIndex = -1;
            int index = 0;
            string selLang = Config.GetString("lang");
            foreach (string file in files)
            {
                string p = Path.GetFileNameWithoutExtension(file);
                if (p == selLang)
                    selIndex = index;
                Log.PrintLine("Loading language " + p + " ...");
                Languages.Add(p, File.ReadAllLines(file));
                toolStripComboBox2.Items.Add(p);
                index++;
            }
            if (selIndex != -1)
                toolStripComboBox2.SelectedIndex = selIndex;
            else if (Languages.Count > 0)
                toolStripComboBox2.SelectedIndex = 0;
        }

        private void LoadProfiles()
        {
            ScanProfiles = new Dictionary<string, string[]>();
            Log.PrintLine("Loading profiles...");
            string[] files = Directory.GetFiles("scan_profiles", "*.txt");
            List<string> names = new List<string>();
            List<string> tmp;
            foreach (string file in files)
            {
                string name = Path.GetFileNameWithoutExtension(file).Replace("_", " ");
                string[] lines = File.ReadAllLines(file);
                tmp = new List<string>();
                foreach (string line in lines)
                    if (line.Trim() != "")
                        tmp.Add(line);
                ScanProfiles.Add(name, tmp.ToArray());
                names.Add(name);
            }
            names.Sort();
            names.Insert(0, "Alle");
            tmp = new List<string>();
            foreach(KeyValuePair<string, string[]> pair in ScanProfiles)
                foreach (string f in pair.Value)
                    if (!tmp.Contains(f))
                        tmp.Add(f);
            tmp.Sort();
            ScanProfiles.Add("Alle", tmp.ToArray());
            toolStripComboBox1.Items.Clear();
            foreach (string name in names)
                toolStripComboBox1.Items.Add(name);
            toolStripComboBox1.SelectedIndex = 0;
            Log.PrintLine("Loaded " + ScanProfiles.Count + " profiles");
        }

        private void SelectLanguage(string l)
        {
            CurrentLanguage = Languages[l];
            toolStripLabel2.Text = CurrentLanguage[0];
            toolStripLabel1.Text = CurrentLanguage[1];
            toolStripButton1.Text = CurrentLanguage[3];
            button1.Text = CurrentLanguage[4];
            checkBox5.Text = CurrentLanguage[5];
            tabPage1.Text = CurrentLanguage[6];
            tabPage2.Text = CurrentLanguage[7];
            label1.Text = CurrentLanguage[8];
            label2.Text = CurrentLanguage[9];
            checkBox1.Text = CurrentLanguage[10];
            checkBox2.Text = CurrentLanguage[11];
            checkBox3.Text = CurrentLanguage[12];
            checkBox6.Text = CurrentLanguage[13];
            label3.Text = CurrentLanguage[14];
            int tmp = comboBox1.SelectedIndex;
            comboBox1.Items.Clear();
            comboBox1.Items.Add(CurrentLanguage[15]);
            comboBox1.Items.Add(CurrentLanguage[16]);
            comboBox1 .SelectedIndex = tmp;
            label4.Text = CurrentLanguage[17];
            tmp = comboBox2.SelectedIndex;
            comboBox2.Items.Clear();
            comboBox2.Items.Add(CurrentLanguage[2]);
            comboBox2.Items.Add(CurrentLanguage[18]);
            comboBox2 .SelectedIndex = tmp;
            checkBox4.Text = CurrentLanguage[19];
            button3.Text = CurrentLanguage[20];
            tmp = toolStripComboBox1.SelectedIndex;
            toolStripComboBox1.Items.RemoveAt(0);
            toolStripComboBox1.Items.Insert(0, CurrentLanguage[2]);
            toolStripComboBox1.SelectedIndex = tmp;
            SaveConfig();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDrives();
        }

        private void RefreshDrives()
        {
            Log.PrintLine("Refreshing devices...");
            Devices.Refresh();
            Log.PrintLine("Saving previous checked items...");
            Dictionary<string, bool> wasChecked = new Dictionary<string, bool>();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                string text = checkedListBox1.Items[i].ToString();
                bool check = checkedListBox1.CheckedIndices.Contains(i);
                wasChecked[text] = check;
            }
            checkedListBox1.Items.Clear();
            Log.PrintLine("Refreshing drive list...");
            foreach (PortableDevice device in Devices)
            {
                device.Connect();
                Log.PrintLine("Getting friendly name...");
                string name = device.FriendlyName;
                Log.PrintLine("Refreshing drives for device " + name);
                PortableDeviceFolder root = device.GetRootContents();
                device.Disconnect();
                foreach (PortableDeviceFolder drive in root.Files)
                {
                    string text = name + " : " + drive.Name;
                    Log.PrintLine("Adding " + drive.Name);
                    if (wasChecked.ContainsKey(text))
                        checkedListBox1.Items.Add(text, wasChecked[text]);
                    else
                        checkedListBox1.Items.Add(text, true);
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            Log.PrintLine("Scan start");
            int index = 0;
            SearchResult = new List<SearchEntry>();
            string profile = toolStripComboBox1.SelectedItem.ToString();
            foreach (PortableDevice device in Devices)
            {
                device.Connect();
                PortableDeviceFolder root = device.GetRootContents();
                foreach (PortableDeviceFolder drive in root.Files)
                {
                    if (checkedListBox1.CheckedIndices.Contains(index))
                        Scan(device, drive, ScanProfiles[profile]);
                    index++;
                }
                device.Disconnect();
            }
            Log.PrintLine("Scan found " + SearchResult.Count + " results...");
            pb1.Value = 0;
            status.Text = CurrentLanguage[21];
            Application.DoEvents();
            Log.PrintLine("Sorting results...");
            SortResults();
            Log.PrintLine("Refreshing results...");
            RefreshResults();
            status.Text = CurrentLanguage[22] + SearchResult.Count;
            Log.PrintLine("Scan end");
        }

        private void Scan(PortableDevice device, PortableDeviceFolder drive, string[] folderToFind)
        {
            Log.PrintLine("Scanning device " + device.FriendlyName + " drive " + drive.Name + " ...");
            pb1.Value = 0;
            pb1.Maximum = folderToFind.Length;
            var content = device.GetDeviceContent();
            foreach (string path in folderToFind)
            {
                Log.PrintLine("Scanning for folder " + path + " ...");
                pb1.Value = pb1.Value + 1;
                string s = CurrentLanguage[23].Replace("$0", path);
                status.Text = s + SearchResult.Count;
                Application.DoEvents();
                var target = drive.Find(ref content, path) as PortableDeviceFolder;
                if (target == null)
                    continue;
                target.EnumerateContents(ref content, true);
                AddAllFiles(device, target);
            }
        }

        private void AddAllFiles(PortableDevice device, PortableDeviceFolder folder)
        {
            foreach(var item in folder.Files)
            {
                if (item is PortableDeviceFile)
                {
                    PortableDeviceFile file = item as PortableDeviceFile;
                    string end = Path.GetExtension(file.Name).ToLower();
                    if (Defines.AllowedFileTypes.ContainsKey(end))
                        SearchResult.Add(new SearchEntry(file, device));
                }
                else
                    AddAllFiles(device, item as PortableDeviceFolder);
            }
        }

        private void SortResults()
        {
            bool run = true;
            while (run)
            {
                run = false;
                for (int i = 0; i < SearchResult.Count - 1; i++)
                    if (SearchResult[i].Path.CompareTo(SearchResult[i + 1].Path) > 0)
                    {
                        var tmp = SearchResult[i];
                        SearchResult[i] = SearchResult[i + 1];
                        SearchResult[i + 1] = tmp;
                        run = true;
                    }
            }
        }

        private void RefreshResults()
        {
            listBox1.Items.Clear();
            List<string> tmp = new List<string>();
            foreach (SearchEntry r in SearchResult)
            {
                string t = r.Device.FriendlyName + r.Path;
                tmp.Add(t);
            }
            listBox1.Items.AddRange(tmp.ToArray());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = listBox1.SelectedIndex;
            if (n == -1 || !checkBox5.Checked)
                return;
            SearchEntry entry = SearchResult[n];
            PortableDevice device = entry.Device;
            string ext = Path.GetExtension(entry.Path).ToLower();
            if (!Defines.AllowedFileTypes.ContainsKey(ext))
                return;
            if (Defines.AllowedFileTypes[ext] != Defines.FileTypeCategory.Image)
                return;
            device.Connect();
            device.DownloadFile(entry.File, "temp.dat");
            device.Disconnect();
            if (File.Exists("temp.dat"))
            {
                pic1.Image = Image.FromStream(new MemoryStream(File.ReadAllBytes("temp.dat")));
                File.Delete("temp.dat");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog d = new FolderBrowserDialog();
            d.SelectedPath = lastOutputFolder;
            if (d.ShowDialog() == DialogResult.OK)
            {
                lastOutputFolder = d.SelectedPath;
                textBox1.Text = lastOutputFolder;
            }
        }

        private bool IsValidFileToExport(string path)
        {
            string ext = Path.GetExtension(path).ToLower();
            if (!Defines.AllowedFileTypes.ContainsKey(ext))
                return checkBox6.Checked;            
            switch (Defines.AllowedFileTypes[ext])
            {
                case Defines.FileTypeCategory.Image:
                    return checkBox1.Checked;
                case Defines.FileTypeCategory.Video:
                    return checkBox2.Checked;
                case Defines.FileTypeCategory.Audio:
                    return checkBox3.Checked;
                default:
                    return false;
            }
        }

        private void SaveConfig()
        {
            lastOutputFolder = textBox1.Text;
            Config.Settings["last_path"] = lastOutputFolder;
            Config.Settings["export_structure"] = comboBox1.SelectedIndex.ToString();
            Config.Settings["export_selection"] = comboBox2.SelectedIndex.ToString();
            Config.Settings["overwrite"] = checkBox4.Checked ? "1" : "0";
            Config.Settings["lang"] = toolStripComboBox2.SelectedItem.ToString();
            Config.Save();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Log.PrintLine("Export start");
            button3.Enabled = false;
            SaveConfig();
            Log.PrintLine("Output folder: " + lastOutputFolder);
            Log.PrintLine("Export structure: " + comboBox1.SelectedItem.ToString());
            Log.PrintLine("Export selection: " + comboBox2.SelectedItem.ToString());
            Log.PrintLine("Overwrite files: " + checkBox4.Checked);
            List<SearchEntry> selFiles;
            if (comboBox2.SelectedIndex == 0)
                selFiles = SearchResult;
            else
            {
                selFiles = new List<SearchEntry>();
                for (int i = 0; i < listBox1.SelectedIndices.Count; i++)
                {
                    int index = listBox1.SelectedIndices[i];
                    selFiles.Add(SearchResult[index]);
                }
            }
            List<SearchEntry> tmp = new List<SearchEntry>();
            foreach (SearchEntry f in selFiles)
                if (IsValidFileToExport(f.Path))
                    tmp.Add(f);
            selFiles = tmp;
            Log.PrintLine("Exporting " + selFiles.Count + " files...");
            pb1.Value = 0;
            pb1.Maximum = selFiles.Count;
            for (int i = 0; i < selFiles.Count; i++)
            {
                SearchEntry f = selFiles[i];
                PortableDevice d = f.Device;
                pb1.Value = i;
                string fileName = Path.GetFileName(f.Path);
                string s = CurrentLanguage[24].Replace("$0", (i + 1).ToString());
                s = s.Replace("$1", selFiles.Count.ToString());
                s = s.Replace("$2", fileName);
                status.Text = s;
                Application.DoEvents();
                string path;
                if (comboBox1.SelectedIndex == 0)
                    path = Path.Combine(lastOutputFolder, f.Path.Substring(1).Replace("/", "\\"));
                else
                    path = Path.Combine(lastOutputFolder, fileName);
                if (File.Exists(path) && checkBox4.Checked)
                    File.Delete(path);
                if (!File.Exists(path))
                {
                    Log.PrintLine("Exporting " + fileName + " ...");
                    string dir = Path.GetDirectoryName(path);
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);
                    d.Connect();
                    d.DownloadFile(f.File, path);
                    d.Disconnect();
                }
            }
            pb1.Value = 0;
            status.Text = CurrentLanguage[25];
            button3.Enabled = true;
            Log.PrintLine("Export end");
        }

        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox2.SelectedItem != null)
                SelectLanguage(toolStripComboBox2.SelectedItem.ToString());
        }
    }
}
