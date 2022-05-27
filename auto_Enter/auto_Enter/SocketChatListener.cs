using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace auto_Enter
{
    public partial class SocketChatListener : Form
    {
        TcpClient clientSocket = new TcpClient();
        NetworkStream stream = default(NetworkStream);
        string _message = string.Empty;

        public delegate void FormSendDataHandler(string message);
        public event FormSendDataHandler FormSendEvent;

        public SocketChatListener()
        {
            InitializeComponent();

            this.Visible = false;
            this.ShowInTaskbar = false;

            new Thread(delegate ()
            {
                InitSocket();
            }).Start();


            Thread t_handler = new Thread(GetMessage);
            t_handler.IsBackground = true;
            t_handler.Start();
        }

        private void InitSocket()
        {
            try
            {
                try
                {
                    clientSocket.Connect(IPCheck, 9999);
                }
                catch
                {
                    //MessageBox.Show("서버에 연결하지 못했습니다.");
                }

                try
                {

                    stream = clientSocket.GetStream();

                    byte[] buffer = Encoding.Unicode.GetBytes("" + "$");

                    stream.Write(buffer, 0, buffer.Length);

                    stream.Flush();
                }
                catch
                {

                }
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message, "Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        public static string IPCheck

        {

            get

            {

                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

                string getIP = string.Empty;

                for (int i = 0; i < host.AddressList.Length; i++)

                {

                    if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)

                    {

                        getIP = host.AddressList[i].ToString();

                        break;  //볼일끝나면 바로 순환문을 나가게 해야 최적화에 도움이 되요.

                    }

                }

                return getIP;

            }

        }

        private void GetMessage() //서버에서 메세지를 받아왔을때 실행되는 함수
        {
            while (true)
            {
                try //아래 코드 실행 중 오류가 발생할 경우 catch문을 실행
                {
                    //-----------서버에서 보낸 메세지를 문자열로 받아옴--------------
                    stream = clientSocket.GetStream();
                    int BUFFERSIZE = clientSocket.ReceiveBufferSize;
                    byte[] buffer = new byte[BUFFERSIZE];
                    int bytes = stream.Read(buffer, 0, buffer.Length);
                    string message = Encoding.Unicode.GetString(buffer, 0, bytes);  //서버에서 보낸 메세지 저장
                    //------------------------------------------------------------

                    //MessageBox.Show(message);
                    this.FormSendEvent(message); //메세지 처리 함수에 메세지 전달
                }

                catch (Exception ex) //오류가 발생할 경우
                {
                    MessageBox.Show(ex.Message);
                }

                Thread.Sleep(1000); //반복문 1초(1000ms)동안 정지
            }
        }

        private void SocketChatListener_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (clientSocket != null)
                clientSocket.Close();
        }
    }
}
