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

namespace auto_enter_socketSERVER
{
    public partial class Form1 : Form
    {
        TcpListener server = null; // 서버
        TcpClient clientSocket = null; // 소켓
        static int counter = 0; // 사용자 수
        // 각 클라이언트 마다 리스트에 추가

        public List<TcpClient> clientList = new List<TcpClient>(); //클라이언트 목록

        public Form1()
        {
            InitializeComponent();

            Threading();
        }

        public void Threading()
        {
            Thread t = new Thread(InitSocket); //스레드 생성
            t.IsBackground = true; //백그라운드 실행 활성화
            t.Start(); //스레드 시작
        }

        private void InitSocket()
        {
            server = new TcpListener(IPAddress.Any, 9999); //포트를 열음
            clientSocket = default(TcpClient); //소켓을 초기화함
            server.Start(); //서버를 시작함

            addText(">Server online"); //서버에 메시지 출력

            while(true)
            {
                try
                {
                    counter++;
                    clientSocket = server.AcceptTcpClient(); //클라이언트의 연결 수락
                    addText(">>Accecpted connection from Client"); //메시지 출력

                    NetworkStream stream = clientSocket.GetStream(); //스트림을 엶
                    byte[] buffer = new byte[1024]; //버퍼 배열 초기화
                    int bytes = stream.Read(buffer, 0, buffer.Length); //버퍼를 통해 스트림에서 데이터를 읽어옴

                    clientList.Add(clientSocket); // cleint 리스트에 추가

                    handleClient h_client = new handleClient(); // 클라이언트 추가
                    h_client.OnReceived += new handleClient.MessageDisplayHandler(OnReceived); //클라이언트로부터의 메시지 수신을 담당할 함수 정의
                    h_client.OnDisconnected += new handleClient.DisconnectedHandler(h_client_OnDisconnected); //클라이언트가 연결을 끊었을 때 실행될 함수 정의
                    h_client.startClient(clientSocket, counter); //클라이언트와의 연결을 시작함
                }

                catch (SocketException) { break; } //에러 발생
                catch (Exception) { break; } //에러 발생
            }
        }

        private void OnReceived(string message) //클라이언트로부터 매시지를 받았을때
        {
            if (message.Equals("leaveChat")) //만약 메시지가 서버와의 연결을 끊는다는 메시지일 경우
            {
                addText("A user disconnected"); //서버에 유저가 연결을 끊었다는 메시지 출력
            }

            else
            {
                string proMessage = message.Split(';')[0] + " " + message.Split(';')[1] + " : " + message.Split(';')[2]; //받은 메시지의 형식을 바꿈

                addText(proMessage); // 받은 메시지를 서버에 출력
                SendMessageAll(message, "", true); //메시지를 모든 클라이언트들에게 전송
            }

        }

        void h_client_OnDisconnected(TcpClient clientSocket) // 클라이언트가 연결을 끊었을 경우
        {
            clientList.Remove(clientSocket); //연결 리스트 중 연결을 끊은 클라이언트를 제거함
        }

        public void SendMessageAll(string message, string user_name, bool flag) //클라이언트들에게 메시지를 보내는 코드
        { 
            foreach (var pair in clientList) //클라이언트 목록에 있는 클라이언트에 대해서 실행함
            {
                TcpClient client = pair; //TCP 클라이언트 정의
                NetworkStream stream = client.GetStream(); //TCP 클라이언트로부터 스트림을 얻어옴
                byte[] buffer = Encoding.Unicode.GetBytes(string.Format(message)); //전송할 메시지의 버퍼 크기를 구함

                stream.Write(buffer, 0, buffer.Length); // 버퍼 쓰기(클라이언트에게 메시지 전송)
                stream.Flush(); //스트림 초기화
            }
        }

        private void addText(string str)// 서버에 메시지를 출력하는 함수
        {
            if (textBox1.InvokeRequired) //스레드를 통해 UI를 수정해야 할 경우
            {
                textBox1.BeginInvoke(new MethodInvoker(delegate //스레드를 통해 명령어 실행
                {
                    textBox1.AppendText("Client >> " + str + Environment.NewLine); //서버 메시지 목록에 메시지를 추가함
                }));
            }

            else
                textBox1.AppendText(str + Environment.NewLine); //서버 메시지 목록에 메시지를 추가함
        }
    }
}
