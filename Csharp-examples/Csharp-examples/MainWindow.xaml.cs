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
using TabWindow;

namespace Csharp_examples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public byte[] stream;
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Class1 a1 = new Class1();
            a1.a = img.Source as BitmapSource;
            stream = a1.ToStream();
        }

        /// <summary>
        /// 읽기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Class1 a2 = new Class1();
            a2.FromStream(stream);
            img.Source = a2.a;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new Window1().Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            TabWindow.TabWindow tw = new TabWindow.TabWindow();
            tw.Width = 500;
            tw.Height = 400;
            for (int i = 0; i < 5; i++)
            {
                string title = "Test " + i;
                Control tw1Ctrl = CreateTestControl(title);
                tw.AddTabItem(title, tw1Ctrl);
            }
            tw.Show();
        }

        private Control CreateTestControl(string content)
        {
            TextBox tx = new TextBox();
            tx.Text = content;
            return tx;
        }
    }
}
