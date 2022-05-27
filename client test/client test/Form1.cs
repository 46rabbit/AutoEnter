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

namespace client_test
{
    public partial class Form1 : Form
    {
        TcpClient clientSocket = new TcpClient(); //소켓 변수
        NetworkStream stream = default(NetworkStream);  //스트림 변수
        public string temp = ""; //임시 변수

        public Form1()
        {
            InitializeComponent();

            new Thread(delegate () //새로운 스레드 생성
            {
                InitSocket(); //소켓 초기화 함수
            }).Start(); //스레드 소환

            Thread t_handler = new Thread(GetMessage); //스레드 생성
            t_handler.IsBackground = true; //백그라운드 실행
            t_handler.Start(); //스레드 시작
        }

        private void InitSocket()
        {
            try
            {
                try
                {
                    clientSocket.Connect(IPCheck, 9999); //IP주소를 통해 서버에 연결함
                    if (clientSocket.Connected) //연결 성공할경우
                    {
                        DisplayText("Client Started\n"); //클라이언트 메시지 창에 메시지 추가
                    }
                }
                catch
                {
                    MessageBox.Show("서버에 연결하지 못했습니다."); //"서버에 연결하지 못했음" 메시지 출력
                }

                stream = clientSocket.GetStream(); //스트림을 소켓을 통해 초기화함

                byte[] buffer = Encoding.Unicode.GetBytes("Ping!" + "$"); //핑을 보내 연결을 확인하기 위해 메시지 버퍼 크기 측정
                stream.Write(buffer, 0, buffer.Length); //핑 메시지를 보냄

                stream.Flush(); //스트림을 초기화함
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message, "Error"); //에러 발생
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error"); //에러 발생
            }
        }

        private void tryConnectServer() //연결 재시도를 위한 함수
        {
            try
            {
                clientSocket.Connect(IPCheck, 9999); //IP주소를 통해 서버에 연결
                if (clientSocket.Connected) //연결 성공할경우
                {
                    DisplayText("Client Started\n"); //클라이언트 메시지 창에 메시지 추가
                    return;
                }
            }

            catch(Exception e)
            {
                MessageBox.Show("서버에 연결하지 못했습니다."); //에러 메시지 출력
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




        private void DisplayText(string text) //클라이언트 메시지 창에 메시지를 출력하는 함수
        {
            if (richTextBox1.InvokeRequired) //스레드를 통해 UI를 수정해야 할 경우
            {
                richTextBox1.BeginInvoke(new MethodInvoker(delegate
                {
                    richTextBox1.AppendText(Environment.NewLine + " >> " + text); //메시지 출력
                }));
            }
            else
                richTextBox1.AppendText(Environment.NewLine + " >>" + text); //메시지 출력
        }

        private void sendBtn_Click(object sender, EventArgs e) //"전송" 버튼 클릭 시
        {
            if (textBox2.Text != "") //메시지 창이 비어있지 않을 경우
            {
                string header; //헤더를 저장할 변수(추후에 필터링에 사용)
                if(comboBox1.Text == "공지") //"공지 종류"가 공지일 경우
                {
                    header = "NOTICE";
                }
                else //"공지 종류"가 시간표 추가일 경우
                {
                    header = "CHANGE";
                }

                byte[] buffer = Encoding.Unicode.GetBytes(header + ";" + textBox2.Text + ";" + richTextBox2.Text + ";" + textBox1.Text + "$");
                //형식에 맞춰 메시지를 생성하고, 버퍼의 크기를 구함 (메시지 형식은 작품 설명서 중 "3차  개발 사항"을 참고)

                stream.Write(buffer, 0, buffer.Length); //스트림에 메시지 작성

                stream.Flush(); //스트림 초기화

                richTextBox2.Text = ""; //메시지 작성 창 초기화
            }

            else //메시지 창이 비어있을 경우
            {
                MessageBox.Show("입력되지 않은 항목이 있습니다!"); //에러 메시지 출력
            }
        }
            
        private void Form1_FormClosing(object sender, FormClosingEventArgs e) //클라이언트를 닫으려 할때
        {
            richTextBox2.Focus(); //메시지 작성 창에 포커스를 함

            byte[] buffer = Encoding.Unicode.GetBytes("leaveChat" + "$");
            //서버에 연결 종료 메시지를 보내기 위해 메시지 크기 측정

            stream.Write(buffer, 0, buffer.Length); //서버에 연결 종료 메시지 보냄

            stream.Flush(); //스트림 초기화

            if (clientSocket != null) //만약 소켓이 닫혀있지 않을 경우
                clientSocket.Close(); //소켓을 닫음
        }

        private void GetMessage() //서버로부터 메시지를 받았을때
        {
            while (true) 
            {
                try
                {
                    stream = clientSocket.GetStream(); //스트림을 소켓을 통해 초기화함
                    int BUFFERSIZE = clientSocket.ReceiveBufferSize; //소켓의 버퍼 크기를 구해 변수에 넣음
                    byte[] buffer = new byte[BUFFERSIZE]; //버퍼 배열을 BUFFERSIZE 변수를 이용해 초기화함
                    int bytes = stream.Read(buffer, 0, buffer.Length); //스트림에서 버퍼를 이용해 바이트를 읽어냄

                    string message = Encoding.Unicode.GetString(buffer, 0, bytes); //버퍼와 바이트를 이용해 메시지를 얻어냄
                    string proMessage = message.Split(';')[0] + " " + message.Split(';')[1] + " : " + message.Split(';')[2];
                    //얻어낸 메시지를 형식에 알맞게 바꿈
                    DisplayText(proMessage); //클라이언트 메시지 창에 표시함
                }
                
                catch(Exception e)
                {

                }
            }
        }

        private void button2_Click(object sender, EventArgs e) //"연결 재시도" 버튼을 눌렀을 때 
        {
            tryConnectServer(); //서버 재접속 시도 함수 실행
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) //"공지 종류" 선택 박스에서 선택된 아이템이 변경됐을 경우
        {
            if(comboBox1.SelectedIndex == 1) //선택한 아이템이 "시간표 추가"일 경우
            {
                Writer writer = new Writer(); //시간표 추가를 위한 텍스트 작성기를 엶
                writer.Owner = this; //텍스트 작성기 주인 설정

                if (writer.ShowDialog() == DialogResult.OK) //텍스트 작성기에서 "완료"를 눌렀을 경우
                {
                    richTextBox2.Text = temp; //메시지 내용 창에 텍스트 작성 창에서 수정한 정보를 바탕으로 메시지를 작성함
                }
            }
        }
    }
}
