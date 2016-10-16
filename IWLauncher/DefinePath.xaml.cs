using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Ookii.Dialogs.Wpf;

namespace IWLauncher
{
    /// <summary>
    /// Interaction logic for DefinePath.xaml
    /// </summary>
    public partial class DefinePath : Window
    {
        public DefinePath()
        {
            InitializeComponent();
           
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.Text != "Path to your League of Legends 4.20" && textBox.Text != "")
            {
                IWLauncher.Properties.Settings.Default.PathLol420 = textBox.Text + @"\";
                IWLauncher.Properties.Settings.Default.FirstTime = false;
                IWLauncher.Properties.Settings.Default.Save();
                MainWindow ze = new MainWindow();
                ze.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid path!");
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Ookii.Dialogs.Wpf.VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog();
            dialog.RootFolder = Environment.SpecialFolder.MyComputer;
            dialog.Description = "Please select your League of Legends 4.20 folder";
            dialog.ShowDialog();
            string result = dialog.SelectedPath.ToString();
            textBox.Text = result;
            
        }
    }
}
