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
using System.Net;
using System.Threading;

namespace IWLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        
        string currentFolder = Directory.GetCurrentDirectory();
        string settingsFolder;
        string map;

        public MainWindow()
        {
            InitializeComponent();
            if (IWLauncher.Properties.Settings.Default.FirstTime == true)
            {
                DefinePath temp = new DefinePath();
                temp.Topmost = true;
                temp.Show();
                this.Close();
            }
            else
            {
                
                settingsFolder = currentFolder + "\\Settings\\";
                if (File.Exists(settingsFolder + "GameInfo.json"))
                {
                    //no problem
                    //if there isn't an icons list get one (takes a while)
                    if (!File.Exists(currentFolder + "\\iconList.txt"))
                    {

                        Icons.GetAllIconsGitHub();

                    }
                    foreach (string line in File.ReadAllLines(currentFolder + "\\iconList.txt"))
                    {
                        iconComboBox.Items.Add(line.Replace(".png", ""));
                    }

                    string savedSettings = File.ReadAllText(settingsFolder + "\\GameInfo.json");
                    JObject data = JObject.Parse(savedSettings);
                    comboBox.Items.Add("CHALLENGER");
                    comboBox.Items.Add("MASTER");
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
                    iconComboBox.SelectedItem = data["players"][0]["icon"].ToString();
                    comboBox2.SelectedItem = data["players"][0]["ribbon"].ToString();
                    pathBox.Text = IWLauncher.Properties.Settings.Default.PathLol420;
                    //Check for available gamemodes
                    foreach (string folder in Directory.GetDirectories(currentFolder + "\\Content\\GameMode"))
                    {
                        DirectoryInfo info = new DirectoryInfo(folder);
                        gameModeCombo.Items.Add(info.Name);
                    }

                    mapCombo.Items.Add("Summoner's Rift");
                    mapCombo.Items.Add("Twisted Treeline");
                    mapCombo.Items.Add("Howling Abyss");
                    mapCombo.Items.Add("Crystal Scar");
                    if (data["game"]["map"].ToString() == "1") { mapCombo.SelectedIndex = 0; } //SR
                    if (data["game"]["map"].ToString() == "8") { mapCombo.SelectedIndex = 3; } //CS
                    if (data["game"]["map"].ToString() == "10") { mapCombo.SelectedIndex = 1; } //TT
                    if (data["game"]["map"].ToString() == "12") { mapCombo.SelectedIndex = 2; } //HA
                    gameModeCombo.SelectedItem = data["game"]["gameMode"].ToString();

                    manaCombo.Items.Add("False");
                    manaCombo.Items.Add("True");
                    cooldownsCombo.Items.Add("False");
                    cooldownsCombo.Items.Add("True");
                    cheatsCombo.Items.Add("False");
                    cheatsCombo.Items.Add("True");
                    minionsCombo.Items.Add("False");
                    minionsCombo.Items.Add("True");

                    manaCombo.SelectedItem = data["gameInfo"]["MANACOSTS_ENABLED"].ToString();
                    cooldownsCombo.SelectedItem = data["gameInfo"]["COOLDOWNS_ENABLED"].ToString();
                    cheatsCombo.SelectedItem = data["gameInfo"]["CHEATS_ENABLED"].ToString();
                    minionsCombo.SelectedItem = data["gameInfo"]["MINION_SPAWNS_ENABLED"].ToString();

                    ipBox.Text = IWLauncher.Properties.Settings.Default.ip;
                    portBox.Text = IWLauncher.Properties.Settings.Default.port;

                }
                else
                {
                    MessageBox.Show("Something went wrong, did you drop me in the bin folder? (Could not find some key files)");
                    Environment.Exit(0);
                }
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("@cd /d \"" + pathBox.Text.Replace("RADS/projects/lol_game_client", "").Replace(@"/", @"\") + "RADS\\solutions\\lol_game_client_sln\\releases\\0.0.1.68\\deploy\"");
            //CMD
            Process.Start("LeagueSandboxGameServer.exe");
            System.Threading.Thread.Sleep(1500);
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = false;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine("@cd /d " + pathBox.Text.Replace("RADS/projects/lol_game_client", "").Replace(@"/", @"\") + "RADS\\solutions\\lol_game_client_sln\\releases\\0.0.1.68\\deploy\"");
            cmd.StandardInput.WriteLine("@start \"\" \"League of Legends.exe\" \"8394\" \"LoLLauncher.exe\" \"\" \"" + IWLauncher.Properties.Settings.Default.ip + " " + IWLauncher.Properties.Settings.Default.port + " 17BLOhi6KZsTtldTsizvHg== 1\"");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            //Console.WriteLine(cmd.StandardOutput.ReadToEnd()); ---> enable it if you want the program to wait for lol to exit
            cmd.Dispose();
            cmd.Close();
            //CMD

        }

        private void button1_Click(object sender, RoutedEventArgs e) //Save button ----> Save settings
        {
            if (mapCombo.SelectedIndex == 0) { map = "1"; } //SR
            if (mapCombo.SelectedIndex == 3) { map = "8"; } //CS
            if (mapCombo.SelectedIndex == 1) { map = "10"; } //TT
            if (mapCombo.SelectedIndex == 2) { map = "12"; } //HA
            GameInfo info = new GameInfo();
            info.name = nameBox.Text;
            info.icon = int.Parse(iconComboBox.SelectedItem.ToString());
            info.rank = comboBox.SelectedItem.ToString();
            info.team = comboBox1.SelectedItem.ToString();
            info.summoner1 = sum1Box.Text;
            info.summoner2 = sum2Box.Text;
            info.ribbon = int.Parse(comboBox2.SelectedItem.ToString());
            info.skin = int.Parse(skinBox.Text);
            info.champion = championBox.Text;

            GameInfo2 extras = new GameInfo2();
            extras.MANACOSTS_ENABLED = manaCombo.SelectedItem.ToString();
            extras.COOLDOWNS_ENABLED = cooldownsCombo.SelectedItem.ToString();
            extras.CHEATS_ENABLED = cheatsCombo.SelectedItem.ToString();
            extras.MINION_SPAWNS_ENABLED = minionsCombo.SelectedItem.ToString();

            

            //Write to file
            string text = JsonConvert.SerializeObject(info, Formatting.Indented);
            string text2 = JsonConvert.SerializeObject(extras, Formatting.Indented);

            //lmao this is so bad but i did this in the middle of the night
            text2 = text2.Replace("\"True\"", "true");
            text2 = text2.Replace("\"False\"", "false");

            text = "{\"players\": [ {" + text.Replace("{", "").Replace("}", "") + runes + "],\"game\": {\"map\": " + map + ",\"gameMode\": \"" + gameModeCombo.SelectedItem + "\"}," + "\"gameInfo\": " + text2 + "}";
            File.WriteAllText(settingsFolder + "\\GameInfo.json", text);
            IWLauncher.Properties.Settings.Default.PathLol420 = pathBox.Text;
            IWLauncher.Properties.Settings.Default.ip = ipBox.Text;
            IWLauncher.Properties.Settings.Default.port = portBox.Text;
            IWLauncher.Properties.Settings.Default.Save();
            MessageBox.Show("New settings have been saved!", "IWLauncher", MessageBoxButton.OK, MessageBoxImage.Information);
            //end writing
        }

        private void iconComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string url = "http://ddragon.leagueoflegends.com/cdn/6.7.1/img/profileicon/" + iconComboBox.SelectedItem + ".png";
            using (WebClient net = new WebClient())
            {
                net.DownloadFile(url, currentFolder + "\\temp.png");
            }
            image.Source = LoadBitmapImage(currentFolder + "\\temp.png");
            File.Delete(currentFolder + "\\temp.png");
        }

        public static BitmapImage LoadBitmapImage(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                return bitmapImage;
            }
        }


        string runes = @",""runes"": {
              //DO NOT CHANGE THESE IF YOU DONT KNOW WHAT YOU ARE DOING.
                ""1"": 5245,
                ""2"": 5245,
                ""3"": 5245,
                ""4"": 5245,
                ""5"": 5245,
                ""6"": 5245,
                ""7"": 5245,
                ""8"": 5245,
                ""9"": 5245,
                ""10"": 5317,
                ""11"": 5317,
                ""12"": 5317,
                ""13"": 5317,
                ""14"": 5317,
                ""15"": 5317,
                ""16"": 5317,
                ""17"": 5317,
                ""18"": 5317,
                ""19"": 5289,
                ""20"": 5289,
                ""21"": 5289,
                ""22"": 5289,
                ""23"": 5289,
                ""24"": 5289,
                ""25"": 5289,
                ""26"": 5289,
                ""27"": 5289,
                ""28"": 5335,
                ""29"": 5335,
                ""30"": 5335
            }
}";
    }

    


}
