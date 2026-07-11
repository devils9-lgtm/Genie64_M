using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace 지니64
{
    internal class logout
    {
        public static void 접속종료()
        {
            try
            {
                 Form1.Console_print("접속종료알림()");
                string hostname = SystemInformation.ComputerName;
                IPAddress[] listIPAddress = Dns.GetHostAddresses(hostname);
                IPAddress[] listIPV4 = listIPAddress.Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToArray();
                string IPV4 = listIPV4[0].ToString();

                const int serverPort = 5425;

                long 예수금 = 0;
                long 실현손익 = 0;

                TcpClient client = new TcpClient();
                IPEndPoint serverAdress = new IPEndPoint(IPAddress.Parse(Form1.serverIp), serverPort);

                 Form1.Console_print($"클라이언트: {client.ToString()}, 서버: {serverAdress.ToString()}");

                client.Connect(serverAdress);
                NetworkStream stream = client.GetStream();

                예수금 = Form1.Acc.D2 + Form1.Acc.매입금;
                long 평가예수금 = Form1.Acc.D2 + Form1.Acc.평가금;

                if (평가예수금 < 예수금)
                    예수금 = 평가예수금;

                실현손익 = Form1.Acc.실현손익;

                string server_ = "모";
                if (Form1.server.Equals("실서버")) server_ = "실";

                string message = "Disconnect^" + GenieConfig.textBox_ID + "^" + IPV4 + "^" + 예수금 + "^" + 실현손익 + "^" + server_;
                byte[] data = Encoding.Default.GetBytes(message);
                stream.Write(data, 0, data.Length);
                 Form1.Console_print($"송신: {message}");

                stream.Close();
                client.Close();
            }
            catch
            {
                 Form1.Console_print("접속종료알림 서버오프");
            }
        }
    }
}
