using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IWLauncher
{
    class Icons
    {
        static string pasta = Directory.GetCurrentDirectory();
        static string temp;

        public static string GetAllIcons()
        {

            string icons;
            WebClient net = new WebClient();
            string DATA = net.DownloadString("http://l3cdn.riotgames.com/releases/live/projects/lol_game_client/releases/0.0.1.7/packages/files/packagemanifest"); //0.0.1.119 4.20
            File.WriteAllText(pasta + "\\teste.txt", DATA);
            int i = 0;
            foreach (string line in File.ReadAllLines(pasta + "\\teste.txt"))
            {

                if (i == 0) {/*do nothing*/ i++; }
                else
                {
                    if (line.Contains("profileIcon"))
                    {
                        string url = line.Split(',')[0].Split('/')[9].Split('.')[0].Replace("profileIcon", "").Replace("profileicon", "") + ".png";
                        temp += url + Environment.NewLine;
                        
                        //net.DownloadFile(url, pasta + "\\Icons\\" + line.Split(',')[0].Split('/')[9].Split('.')[0].Replace("profileIcon", "") + ".png");
                        //Console.WriteLine(line.Split(',')[0].Split('/')[9].Split('.')[0].Replace("profileIcon", "") + ".png" + " downloaded!");
                    }
                }
            }
            File.Delete(pasta + "\\teste.txt");
            File.WriteAllText(pasta + "\\iconList.txt", temp);
            //Console.Write(temp);
            icons = temp;
            return icons;
        }

        public static void GetAllIconsGitHub()
        {
            using (WebClient net = new WebClient())
            {
                net.DownloadFile("https://raw.githubusercontent.com/TheWebs/IWLauncher/master/IWLauncher/iconList.txt", pasta + "\\iconList.txt");
            }
        }
    }
}
