using Microsoft.Win32;
using NAudio.Wave;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AudioPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //MediaPlayer player;

        public MainWindow()
        {
            InitializeComponent();
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|WAV files (*.wav)|*.wav|All files (*.*)|*.*";

        //    if (openFileDialog.ShowDialog() == true)
        //    {
        //        player = new MediaPlayer();
        //        player.Stop();
        //        player.Open(new Uri(openFileDialog.FileName));
        //    }

        //}

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    if (player != null)
        //    {
        //        player.Play();
        //    }
        //}

        //private void Button_Click_2(object sender, RoutedEventArgs e)
        //{
        //    if (player != null)
        //    {
        //        player.Stop();
        //        player.Position = new TimeSpan(0, 0, 10);
        //        player.Play();
        //    }
        //}

        //private void Button_Click_3(object sender, RoutedEventArgs e)
        //{
        //    if (player != null)
        //    {
        //        player.Pause();
        //    }
        //}

        //private void Button_Click_4(object sender, RoutedEventArgs e)
        //{
        //    if (player != null)
        //    {
        //        player.Stop();
        //    }
        //}
    }
}
