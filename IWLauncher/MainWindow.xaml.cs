using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using MahApps;
using MahApps.Metro;
using MahApps.Metro.Controls;

namespace IWLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        
        string currentFolder = Directory.GetCurrentDirectory();
        string settingsFolder;

        public MainWindow()
        {
            InitializeComponent();
            settingsFolder = currentFolder + "\\Settings\\";
            if (File.Exists(settingsFolder + "settings.json") && File.Exists(settingsFolder + "GameInfo.json"))
            {
                //no problem
                string savedSettings = File.ReadAllText(settingsFolder + "\\GameInfo.json");
                JObject data = JObject.Parse(savedSettings);
                comboBox.Items.Add("CHALLENGER");
                comboBox.Items.Add("DIAMOND");
                comboBox.Items.Add("GOLD");
                comboBox.Items.Add("SILVER");
                comboBox.Items.Add("BRONZE");
                comboBox1.Items.Add("BLUE");
                comboBox1.Items.Add("PURPLE");
                comboBox2.Items.Add("0");
                comboBox2.Items.Add("1");
                comboBox2.Items.Add("2");
                comboBox.SelectedItem = data["players"][0]["rank"].ToString();
                comboBox1.SelectedItem = data["players"][0]["team"].ToString();
                nameBox.Text = data["players"][0]["name"].ToString();
                championBox.Text = data["players"][0]["champion"].ToString();
                skinBox.Text = data["players"][0]["skin"].ToString();
                sum1Box.Text = data["players"][0]["summoner1"].ToString();
                sum2Box.Text = data["players"][0]["summoner2"].ToString();
                iconBox.Text = data["players"][0]["icon"].ToString();
                comboBox2.SelectedItem = data["players"][0]["ribbon"].ToString();
                JObject data2 = JObject.Parse(File.ReadAllText(settingsFolder + "\\settings.json"));
                pathBox.Text = data2["radsPath"].ToString();
            }
            else
            {
                MessageBox.Show("Something went wrong, did you drop me in the bin folder? (Could not find some key files)");
                Environment.Exit(0);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            
            //CMD
            //Process.Start("IntWarsSharp.exe"); ---->uncomment when server can be executed from the .exe file
            //System.Threading.Thread.Sleep(1500);
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = false;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine("@cd /d " + pathBox.Text.Replace("RADS/projects/lol_game_client", "").Replace(@"/", @"\") + "RADS\\solutions\\lol_game_client_sln\\releases\\0.0.1.68\\deploy\"");
            cmd.StandardInput.WriteLine("@start \"\" \"League of Legends.exe\" \"8394\" \"LoLLauncher.exe\" \"\" \"127.0.0.1 5119 17BLOhi6KZsTtldTsizvHg== 1\"");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            //CMD
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            GameInfo info = new GameInfo();
            info.name = nameBox.Text;
            info.icon = iconBox.Text;
            info.rank = comboBox.SelectedItem.ToString();
            info.team = comboBox1.SelectedItem.ToString();
            info.summoner1 = sum1Box.Text;
            info.summoner2 = sum2Box.Text;
            info.ribbon = comboBox2.SelectedItem.ToString();
            info.skin = skinBox.Text;
            info.champion = championBox.Text;
            string text = JsonConvert.SerializeObject(info);
            text = "{\"players\": [ {" + text.Replace("{", "") + "],\"game\": {\"map\": 1}}";
            File.WriteAllText(settingsFolder + "\\GameInfo.json", text);
            string path = "{\"radsPath\":\"" + pathBox.Text + "\"}";
            File.WriteAllText(settingsFolder + "\\settings.json", path);
        }
    }


}
