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
using System.Windows.Threading;

namespace WPFChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChatClient cc = null;
        public MainWindow()
        {
            InitializeComponent();
            msgPrint("서버 주소, 포트 입력");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {   
            string hostName = ip.Text;
            UInt16 hostPort;

            // null 값이 입력됬다면 (아무것도 입력되지 않았다면)
            if (string.IsNullOrEmpty(hostName))
            {
                msgPrint("다시 입력하세요");
            }
            else
            {
                hostPort = Convert.ToUInt16(port.Text);

                cc = new ChatClient(this);
                msgPrint("접속 중...");

                cc.ConnectToServer(hostName, hostPort);
                if (!cc.Connected)
                {
                    msgPrint("접속 중 오류 발생!");
                }
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (cc != null)
            {
                string msg;
                msg = message.Text.Trim();

                // 입력받은 문자열이 null 인 경우
                if (string.IsNullOrEmpty(msg))
                {
                    msgPrint("보낼 메시지를 입력해주세요.");
                    return;
                }
                // 그 외의 경우엔 메세지를 보낸다.
                else
                {
                    cc.SendMessage(msg);
                }
            }
        }

        public void msgPrint(string msg)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate 
            {
                RowDefinition row = new RowDefinition() { Height = new GridLength(20) };
                msgGrid.RowDefinitions.Add(row);
                TextBlock textBox = new TextBlock();
                textBox.VerticalAlignment = VerticalAlignment.Center;
                Grid.SetRow(textBox, msgGrid.Children.Count);
                msgGrid.Children.Add(textBox);
                textBox.Text = msg;
                message.Text = "";
            }));
        }
    }
}
