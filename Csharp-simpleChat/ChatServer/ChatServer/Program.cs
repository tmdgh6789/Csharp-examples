using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            enterHostPort:
            UInt16 hostPort;
            Console.Write("수신 대기할 포트 입력: ");
            try
            {
                hostPort = UInt16.Parse(Console.ReadLine().Trim());
            }
            catch
            {
                Console.WriteLine("다시 입력하세요");
                goto enterHostPort;
            }

            ChatServer cs = new ChatServer();
            cs.StartServer(hostPort);

            while (true)
            {
                String msg;
                Console.Write("보낼 메세지 (종료키: X): ");
                msg = Console.ReadLine().Trim();

                // 입력받은 문자열이 null 인 경우, 다시 반복문의 처음으로 돌아간다.
                if (String.IsNullOrEmpty(msg))
                    continue;

                // 입력받은 문자열이 X 인 경우, 프로그램을 종료한다.
                if (msg.Equals("X"))
                {
                    cs.StopServer();
                    return;
                }

                // 그 외의 경우엔 메세지를 보낸다.
                cs.SendMessage(msg);
            }
        }
    }
}
