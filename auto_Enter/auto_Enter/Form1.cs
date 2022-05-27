using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Text;
using System.Threading;
using System.Net;
using System.Security.Cryptography;
using System.IO.Compression;

namespace auto_Enter
{
    public partial class AutoEnter : Form
    {
        [DllImport("user32.dll")]
        private static extern int RegisterHotKey(int hwnd, int id, int fsModifiers, int vk); //글로벌 핫키 등록 함수

        [DllImport("user32.dll")]
        private static extern int UnregisterHotKey(int hwnd, int id); //글로벌 핫키 등록 해제 함수

        int X, Y; //윈도우 창 드래그를 위한 좌표

        public List<string> timeSaveList = new List<string>(); //"새로고침"버튼을 위해 시간을 저장한 변수(리스트)
        List<string> hotswapSaveList = new List<string>(); //핫스왑등 기능에서 사용하는 과목 추가 기록 변수(리스트)
        List<string> filteringHeaderList = new List<string>();
        Dictionary<string, string> getFileRouteList = new Dictionary<string, string>();

        Bitmap btMain; //스크린샷을 위한 비트맵 변수

        string notifyTxt;

        string startupPath = Application.StartupPath; //프로그램 시작 경로

        public AutoEnter()
        {
            InitializeComponent();

            //updateAsync();
            CheckForIllegalCrossThreadCalls = false; //스레드 관련 에러 내용 무시

            timer1.Start(); //시간 체크용 타이머 시작(30초마다 실행)

            FileInfo fileInfo = new FileInfo(startupPath + "\\classSave.txt");//classSave 파일이 존재하는지 확인    
            if (!fileInfo.Exists)                                             //존재하지 않을 경우                                            
            {
                fileInfo.Create(); //파일을 생성함
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(startupPath + "\\timeTableSave"); //timeTableSavve 폴더가 존재하는지 확인
            if (!directoryInfo.Exists)                                             //존재하지 않을 경우                                            
            {
                directoryInfo.Create(); //폴더를 생성함
            }

            else //존재할경우
            {
                string[] temp = File.ReadAllLines(startupPath + "\\classSave.txt"); //classSave 파일의 모든 줄을 읽어와 배열에 저장

                foreach (string item in temp) //배열에 들어있는 모든 값에 대하여
                {
                    string strTemp = item.Replace(';', ' '); //;을 공백으로 바꿈
                    hotswapSaveList.Add(strTemp);                  //classSave(리스트)에 값을 더함
                }
                setReplaceBtn();    //메뉴창의 "교환" 버튼에 마우스를 올렸을때 나오는 버튼들은 추가함
                //wirteclassSavefile();
            }

            addTimetableItems(startupPath + "\\timeTableSave"); //파일 선택 창에 아이템ㅇㄹ 더함

            SocketChatListener socketChatListener = new SocketChatListener();
            socketChatListener.Owner = this;
            socketChatListener.FormSendEvent += new SocketChatListener.FormSendDataHandler(handleMessage);
            socketChatListener.Show();
        }

        public void addTimetableItems(string fileRoute) //"파일 불러오기" 클릭 시 나오는 파일 선택 창에 아이템을 추가함
        {
            contextMenuStrip2.Items.Add("로컬 파일에서 불러오기"); //"로컬 파일에서 불러오기" 추가
            contextMenuStrip2.Items.Add(new ToolStripSeparator()); //파일 선택 창에 구분선 추가

            string[] files = Directory.GetFiles(fileRoute, "*.txt", SearchOption.AllDirectories); //혹시 모를 섞여 들어가있는 비 텍스트 파일들을 걸러냄

            foreach (string txtFileRoute in files) //files 배열 안에 있는 텍스트 파일 모두에게 적용
            {
                FileInfo fi = new FileInfo(txtFileRoute); //텍스트 파일 경로를 통해 텍스트 파일의 정보를 얻어옴

                getFileRouteList.Add(fi.Name, txtFileRoute); //getFileRouteList에 파일의 이름과 경로를 추가함
                contextMenuStrip2.Items.Add(fi.Name); //파일 선택 창에 파일의 이름을 아이템으로 추가
            }
        }

        public void handleMessage(string message)
        {
            string commandHeader = message.Split(';')[0]; //메시지 종류
            string sender = message.Split(';')[1];        //발신자
            string msgContent = message.Split(';')[2];    //메시지 내용
            string senderHeader = message.Split(';')[3];  //발신자 코드

            if (commandHeader == "NOTICE") //메시지 종류가 공지일 경우
            {
                notifyTxt = sender + ";" + msgContent; //공지 내용과 발신자를 전역 변수에 저장(합치는 이유는 변수 수를 줄이기 위함)
                notifyIcon1.ShowBalloonTip(3000, sender, msgContent, ToolTipIcon.None); //윈도우 알림을 띄움.
            }

            else if (commandHeader == "CHANGE") //메시지 종류가 시간표 변경일 경우
            {
                new Thread(delegate () //새로운 스레드 생성
                      {
                          changeTimetableItem(msgContent); //메시지 내용을 2차 가공 함수에 넘겨줌
                      }).Start(); //스레드 시작
            }
        }

        private void changeTimetableItem(string msgContent) //메시지 2차 가공 함수
        {
            string targetName = msgContent.Split('@')[0]; //바뀔 시간표 이름
            int targetIndex = Int32.Parse(msgContent.Split('@')[1]) - 1; //바꿀 시간표 순서
            string link = msgContent.Split('@')[2]; //바뀔 링크
            string time = msgContent.Split('@')[3]; //바뀔 시간

            string broadcastContent = targetIndex + "번째 과목이 " + targetName + "으로 교체되었습니다.";
            notifyIcon2.ShowBalloonTip(3000, "시간표 변경", broadcastContent, ToolTipIcon.None); //윈도우 알림을 띄워 시간표가 수정되었음을 알림

            if (listView3.InvokeRequired) //스레드에서 UI관련 처리를 진행하기 위해서는 특별 절차 필요
            {
                listView3.Items[targetIndex].Text = targetName; //바꿀 순서의 시간표 이름을 주어진 시간표 이름으로 바꿈
                listView3.Items[targetIndex].SubItems[1].Text = link; //바꿀 순서의 시간표 링크를 주어진 시간표 링크로 바꿈
                listView3.Items[targetIndex].SubItems[2].Text = time.Replace("#", " : "); //바꿀 순서의 시간표 시간을 주어진 시간표 시간으로 바꿈
            }
            else
            {
                listView3.Items[targetIndex].Text = targetName; //바꿀 순서의 시간표 이름을 주어진 시간표 이름으로 바꿈
                listView3.Items[targetIndex].SubItems[1].Text = link; //바꿀 순서의 시간표 링크를 주어진 시간표 링크로 바꿈
                listView3.Items[targetIndex].SubItems[2].Text = time.Replace("#", " : "); //바꿀 순서의 시간표 시간을 주어진 시간표 시간으로 바꿈
            }
        }

        private void panel2_Click(object sender, EventArgs e) //종료버튼 클릭
        {
            closeConfirm cF = new closeConfirm();                               //위의 사진에서 나온 선택창 변수 생성
            cF.callHideForm += new closeConfirm.FormSendDataHandler(hideForm);  //선택창에서 본 코드에 있는 함수를 호출하기 위한 작업
            cF.Show();                                                          //선택창을 보임
        }

        public void hideForm() //프로그램을 숨기는 함수 (선택창에서 백그라운드에서 실행을 선택했을 시 호출)
        {
            this.WindowState = FormWindowState.Minimized; //창의 상태를 최소화시킴
            this.ShowInTaskbar = false;                   //윈도우 작업 표시줄에서 보이지 않게 함
        }

        private void addBtn_Click(object sender, EventArgs e) //"추가" 버튼 클릭시
        {
            manageTimeTable manageTimeTable = new manageTimeTable();
            manageTimeTable.Owner = this;
            manageTimeTable.Show();
        }

        private void deleteBtn_Click(object sender, EventArgs e) // 삭제 버튼 클릭시
        {
            if (listView3.Items.Count == 0)  //추가된 시간표가 없을때
                return; //함수 리턴

            if (listView3.Focus() == false) //시간표 표시 창 중 선택된 항목이 없을경우
                return;

            if (MessageBox.Show("시간표 내에서 " + "[" + listView3.Items[listView3.FocusedItem.Index].Text + "]" + "를 제거하시겠습니까?", "",
                MessageBoxButtons.YesNo) == DialogResult.Yes) //삭제 확인 메세지
            {
                listView3.Items.RemoveAt(listView3.FocusedItem.Index); //선택된 항목 삭제
                timeSaveList.RemoveAt(listView3.FocusedItem.Index); //timeSaveList에서 선택한 아이템 삭제
            }
        }

        public void realignTimeList() //timeSaveList에 들어있는 항목들을 시간 순으로 재정렬하는 함수
        {
            //예비 변수 선언------------------
            string temp;
            int least;
            int count = timeSaveList.Count; //timeSaveList의 항목 수
            //-------------------------------

            for (int i = 0; i < count; i++) //count만큼 반복
            {
                least = i;
                for (int j = i + 1; j < count; j++) //count만큼 반복
                {
                    if (Int32.Parse(timeSaveList[j].Replace(" : ", "")) < Int32.Parse(timeSaveList[least].Replace(" : ", ""))) //최소값을 찾는다
                        least = j;
                }
                temp = timeSaveList[i]; //temp를 이용해 원래 위차한 값과 최소값을 교환시킨다
                timeSaveList[i] = timeSaveList[least]; //값을 서로 변경함
                timeSaveList[least] = temp; //값을 서로 변경함
            }
        }

        private void timer1_Tick(object sender, EventArgs e) //주어진 초마다 실행되는 함수
        {
            checkTime();
        }

        private void checkTime()
        {
            string now = DateTime.Now.ToString("HH : mm");   //현재 시간을 string 형식으로 받아옴

            for (int i = 0; i < listView3.Items.Count; i++)  //현재 불러와진 시간표의 목록 수 만큼 반복
            {
                string link = listView3.Items[i].SubItems[1].Text.ToString(); //i번째 시간표의 링크를 string형식으로 받아옴
                string time = listView3.Items[i].SubItems[2].Text.ToString(); //i번째 시간표의 시간을 string형식으로 받아옴


                if (time.Equals(now)) //현재 시간과 시간표에 등록된 시간이 같을 경우
                {
                    //ProcessStartInfo pro = new ProcessStartInfo("iexplore.exe", link); //인터넷 익스플로러를 통해 링크를 열 변수 지정
                    //Process.Start(pro);                                                //위의 변수를 실행하여 링크를 엶

                    Uri myUri = new Uri(link, UriKind.Absolute); //string을 내장 브라우저에 맞는 변수 형식으로 변환

                    webBrowser1.Url = myUri;      //내장 브라우저에 링크를 넘겨줌
                    listView3.Items.RemoveAt(i);  // 링크를 연 후 해당 항목은 삭제
                }
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                X = e.X;
                Y = e.Y;
            }
        }

        private void button1_Click(object sender, EventArgs e) //파일 불러오기 버튼
        {
            Button btnSender = (Button)sender; //"파일 불러오기 버튼"을 변수로 받아옴
            Point ptLowerLeft = new Point(0, btnSender.Height); //"파일 불러오기"버튼의 위치 구함
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft); //버튼의 위치를 전체 화면에서의 위치로 변환함
            contextMenuStrip2.Show(ptLowerLeft); //파일 선택 창을 해당 위치에 보여줌
        }

        private void getFile(string fileRoute) //파일 경로를 인수로 전달받아 해당 파일을 불러옴
        {
            string[] lines = File.ReadAllLines(fileRoute); //받은 경로상의 txt 파일 안에 있는 모든 줄을 불러옴

            foreach (string str in lines) //lines 배열에 있는 모든 아이템 당
            {
                string hotswapStr = str.Replace(';', ' ');

                if (!hotswapSaveList.Contains(hotswapStr))
                {
                    hotswapSaveList.Add(hotswapStr);
                }

                string[] temp = str.Split(';'); //';'를 기점으로 텍스트를 나눔 (시간표 파일은 (과목명);(링크);시간 순으로 구성되어있기 때문

                ListViewItem listViewItem = new ListViewItem(); //항목 1에 시간표를 추가하기 위한 코드
                listViewItem.Text = temp[0];
                listViewItem.SubItems.Add(temp[1]);
                listViewItem.SubItems.Add(temp[2]);

                timeSaveList.Add(temp[2]); //temp[2]는 현재 추가하는 과목의 시간을 말함

                listView3.Items.Add(listViewItem); //항목 1에 추가
            }
            setReplaceBtn();
            wirteclassSavefile();
        }

        private void notifyIcon1_Click(object sender, EventArgs e) //작업표시줄 상 아이콘을 눌렀을때 프로그램을 보임
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e) //프로그램의 진회색 부분을 잡고 드래그했을때 폼 움직임 코드 구현
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Location = new Point(this.Location.X + (e.X - X), this.Location.Y + (e.Y - Y));
            }
        }

        private void captureScreen() //스크린 캡처 함수
        {
            PictureBox pictureBox = new PictureBox(); //임시적인 사진 저장을 위한 변수 선언
            btMain = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height); //비트맵 변수(btMain)을 화면의 가로, 세로 비율로 초기화
            using (Graphics g = Graphics.FromImage(btMain))
            {
                g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, btMain.Size, CopyPixelOperation.SourceCopy); //화면을 캡처하여 btMain에 저장

                string dir = Application.StartupPath + "\\screenCapture"; //프로그램 시작 경로에 screenCapture이란 폴더 경로를 덧붙힘
                DirectoryInfo di = new DirectoryInfo(dir); //경로(dir)의 정보를 가져옴

                if (!di.Exists) //dir에 저장된 경로가 존재하지 않을 경우
                {
                    di.Create(); //dir에 저장된 경로를 생성함
                    //Process.Start(dir); //dir에 저장된 경로를 엶(테스트 용)
                }

                pictureBox.Image = btMain; //사진 저장을 위해 임시적으로 변수에 비트맵 이미지 저장
                pictureBox.Image.Save(dir + "\\" + DateTime.Now.ToString("yy년 MM월 dd일 tt hh시 mm분 ss초") + ".png", System.Drawing.Imaging.ImageFormat.Png);//주어진 경로(dir)에 현재 시간을 파일명으로 하여 사진을 저장
            }
        }

        private void AutoEnter_Load(object sender, EventArgs e) //앱이 실행될 때
        {
            RegisterHotKey((int)this.Handle, 0, 0x1, (int)Keys.C); //Alt + C로 핫키를 등록(핫키 아이디를 0으로 설정)
        }

        private void AutoEnter_FormClosing(object sender, FormClosingEventArgs e) //앱이 종료될 때
        {
            UnregisterHotKey((int)this.Handle, 0); //아이디가 0인 핫키를 등록 취소함
            notifyIcon1.Visible = false;
        }

        protected override void WndProc(ref Message m) //키가 눌릴때 호출됨
        {
            base.WndProc(ref m);

            if (m.Msg == (int)0x312) //핫키가 눌러지면 312 정수 메세지를 받게됨
            {
                if (m.WParam == (IntPtr)0x0) // 그 핫키의 ID가 0이면(위에서 등록한 핫키가 눌렸을 경우)
                {
                    captureScreen(); //스크린을 캡쳐하는 함수 호출
                }
            }
        }

        private void 수정ToolStripMenuItem_Click(object sender, EventArgs e) //메뉴에서 "수정" 버튼을 눌렀을 경우
        {
            string name = listView3.Items[listView3.FocusedItem.Index].Text;            //현재 선택된 시간표 항목의 이름을 가져옴
            string link = listView3.Items[listView3.FocusedItem.Index].SubItems[1].Text;//현재 선택된 시간표 항목의 링크를 가져옴
            string time = listView3.Items[listView3.FocusedItem.Index].SubItems[2].Text;//현재 선택된 시간표 항목의 시간을 가져옴

            manageTimeTable editTimeTable = new manageTimeTable(name, link, time, false); //별도의 창(이하 editTimeTable)에 가져온 이름, 링크, 시간을 넘겨줌
                                                                                          //데이터를 넘겨주며 변수 초기화 진행 
            editTimeTable.Owner = this; //editTimeTable의 부모를 이 창(메인 윈도우)으로 설정
            editTimeTable.Show(); //editTimeTable을 보여줌
        }

        private void 삭제ToolStripMenuItem_Click(object sender, EventArgs e)//메뉴에서 "삭제" 버튼을 눌렀을 경우
        {
            deleteBtn.PerformClick(); //선택한 항목을 삭제함
        }

        private void button3_Click_1(object sender, EventArgs e) //"새로고침"버튼 클릭 시
        {
            realignTimeList();
            for (int i = 0; i < listView3.Items.Count; i++)  //현재 불러와진 시간표의 목록 수 만큼 반복
            {
                listView3.Items[i].SubItems[2].Text = timeSaveList[i]; //과목의 시간 부분을 temp_TimeList의 i번째 항목으로 바꿈
            }
        }

        public void setListViewItem(string name, string link, string time) //현재 선택된 시간표 과목을 데이터를 받아서 수정하는 함수
        {
            int i = listView3.FocusedItem.Index; //현재 선택된 과목의 번호(인덱스)

            listView3.Items[i].Text = name;            //선택된 과목의 이름을 name 변수로 바꿈
            listView3.Items[i].SubItems[1].Text = link;//선택된 과목의 링크를 link 변수로 바꿈
            listView3.Items[i].SubItems[2].Text = time;//선택된 과목의 시간을 time 변수로 바꿈

            timeSaveList[i] = time;
        }

        public void addListViewItem(string name, string link, string time) //현재 선택된 시간표 과목을 데이터를 받아서 시간표를 수정하는 함수
        {
            ListViewItem listViewItem = new ListViewItem(); //ListViewItem 변수를 만듦
            listViewItem.Text = name; //수정한 이름을 listViewItem에 추가
            listViewItem.SubItems.Add(link); //수정한 링크를 listViewItem에 추가
            listViewItem.SubItems.Add(time); //수정한 시간을 listViewItem에 추가

            listView3.Items.Add(listViewItem);
        }

        private void setReplaceBtn() //메뉴창에서 "교환" 탭에 마우스를 올렸을 때 나오는 버튼들을 추가함
        {
            int i = 0; //배열 번호용 변수

            (contextMenuStrip1.Items[0] as ToolStripMenuItem).DropDownItems.Clear();

            foreach (string items in hotswapSaveList) //classSave(리스트)안에 있는 모든 값에 대하여
            {
                (contextMenuStrip1.Items[0] as ToolStripMenuItem).DropDownItems.Add(items); //탭에 리스트에 있는 값을 버튼 형식으로 더함

                (contextMenuStrip1.Items[0] as ToolStripMenuItem).DropDownItems[i].Click += new EventHandler(contextMenustripSubitem_Click);
                //해당 버튼 클릭 시 실행될 함수를 설정
                i++;//배열 인덱스 증가
            }
        }

        private void contextMenustripSubitem_Click(object sender, EventArgs e) //"교환"탭에 있는 버튼 클릭 시 실행
        {
            string s = (sender as ToolStripMenuItem).Text.Replace(' ', ';').Replace(";:;", " : "); //클릭된 버튼에 쓰인 텍스트(이하 s)를 가져옴

            int i = listView3.FocusedItem.Index; //현재 선택된 과목의 인덱스(번호)

            listView3.Items[i].Text = s.Split(';')[0];            //현재 선택된 과목의 이름을 s(그 중 과목 이름만 분리함)로 바꿈
            listView3.Items[i].SubItems[1].Text = s.Split(';')[1];//현재 선택된 과목의 이름을 s(그 중 과목 링크만 분리함)로 바꿈
            listView3.Items[i].SubItems[2].Text = s.Split(';')[2];//현재 선택된 과목의 이름을 s(그 중 과목 시간만 분리함)로 바꿈
        }

        private void listView3_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) //만약 마우스 오른쪽으로 클릭했다면
            {
                var focusedItem = listView3.FocusedItem; //클릭된 항목을 가져와 변수에 저장
                if (focusedItem != null && focusedItem.Bounds.Contains(e.Location)) //변수가 비어있지 않고 특정 과목을 클릭했을 경우
                {
                    contextMenuStrip1.Show(Cursor.Position); //커서의 위치에서 메뉴창을 보임
                }
            }
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e) //알림창을 클릭했을 때
        {
            NotifyForm notifyForm = new NotifyForm(notifyTxt); //알림창에 현재 띄어저 있는 내용을 통해 알림 세부 창초기화
            notifyForm.Show(); //알림 세부 창 표시
        }

        private void contextMenuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        //"파일 불러오기" 클릭 시 나오는 파일 선택 창의 아이템 클릭 시
        //파일 선택 창 UI는 작품 설명서 16페이지를 참고
        {
            string clickedItemName = e.ClickedItem.Text; //클릭한 항목의 텍스트를 가져옴

            if (clickedItemName == "로컬 파일에서 불러오기") //"로컬에서 불러오기" 선택 시
            {
                contextMenuStrip2.Close(); //파일 선택 창 닫음
                OpenFileDialog openFileDialog = new OpenFileDialog(); //파일 선택 다이얼로그(윈도우 내장)를 엶
                if (openFileDialog.ShowDialog() == DialogResult.OK) //만약 불러오기를 했을 경우
                {
                    getFile(openFileDialog.FileName); //불러온 파일의 경로를 getFile함수에 인수로 전달함
                }
                return;
            }
            else //하단에 표시되는 파일 중 하나 클릭 시
            {
                foreach (var item in getFileRouteList) //getFileRouteList에 있는 항목들 중
                {
                    if (clickedItemName == item.Key) //getFileRouteList에 현재 선택한 아이템의 이름과 일치하는 항목이 있을 경우
                    {
                        getFile(item.Value); //선택한 파일의 경로를 getFile함수에 인수로 전달함
                    }
                }
            }
        }

        private void wirteclassSavefile() //최근에 추가한 과목을 저장하는 파일(classSave)에 추가한 항목을 쓰는 함수
        {
            string route = startupPath + "\\classSave.txt"; //프로그램 시작 경로에 classSave.txt라는 파일의 경로를 더함

            using (StreamWriter outputFile = new StreamWriter(route)) //파일에 쓰기 위해 변수 선언
            {
                foreach (string item in hotswapSaveList) //classSave(리스트)에 있는 모든 항목에 대하여
                {
                    outputFile.WriteLine(item); //classSave.txt파일에 classSave의 값을 씀
                }
            }
        }

        private void updateAsync() //업데이트 함수
        {
            string path = startupPath + @"\updateArgs"; //해쉬 파일을 저장할 경로

            var request = (HttpWebRequest)WebRequest.Create("https://multiground-bucket.s3.ap-northeast-2.amazonaws.com/v1.0.4/MultiGround-Client.zip.md5"); //해쉬 파일 다운로드 요청
            var response = request.GetResponse(); //해쉬 파일 다운로드 요청을 보내고 응답을 받음
            var responseStream = response.GetResponseStream(); //해쉬 파일을 다운받아 스트림으로 저장함

            SaveStreamAsFile(path, "MD5.txt", responseStream); //스트림을 파일로 저장함
            string downloadHash = File.ReadAllText(Path.Combine(path, "MD5.txt"), Encoding.UTF8); //해쉬 파일 값을 읽어옴
            string systemHash = getFileHash(Path.Combine(startupPath + "//" + "auto_enter.exe")); //현재 exe의 해쉬 파일을 얻어옴

            if (downloadHash != systemHash) //두 해쉬값이 다름(업데이트 필요)
            {
                request = (HttpWebRequest)WebRequest.Create("https://multiground-bucket.s3.ap-northeast-2.amazonaws.com/v1.0.4/MultiGround-Client.zip"); //서버에 업로드 되어있는 exe 파일(zip파일 형식임) 다운로드 요청
                response = request.GetResponse(); //exe 파일 다운로드 요청을 보내고 응답을 받음
                responseStream = response.GetResponseStream(); //exe 파일을 다운받아 스트림으로 저장함

                Stream data = responseStream; //압축 풀기 전 파일
                Stream unzippedEntryStream; //압축 푼 후의 파일을 저장할 스트림

                ZipArchive archive = new ZipArchive(data); //압축 풀기 전 파일을 압축해제함

                foreach (ZipArchiveEntry entry in archive.Entries) //압축 폴더 안에 들어있던 (지금은 압축 해제된) 파일 당
                {
                    unzippedEntryStream = entry.Open(); //압축을 푼 파일을 unzippedEntryStream; 에 저장함
                    SaveStreamAsFile(path, "auto_enter.exe", unzippedEntryStream); //압축을 푼 파일을 경로에 저장함(기존에 있던 파일을 덮어씀)
                }

                data.Close(); //data 스트림 닫음
            }
        }

        private string getFileHash(string filepath/*파일 경로*/) //주어진 파일의 해쉬 값을 얻어오는 함수
        {
            try
            {
                FileInfo item; //파일의 정보를 저장할 변수 선언
                byte[] hashcode; //해쉬값을 저장할 변수 선언
                item = new FileInfo(filepath); //주어진 파일 경로를 통해 item 초기화
                hashcode = MD5.Create().ComputeHash(item.OpenRead()); //item의 파일 정보를 통해 MD5형식 해쉬를 읽어옴
                return BytesToString(hashcode); //바이트 배열을 string형식으로 변환해 반환
            }
            catch //해쉬 값을 얻어올 수 없을 때
            {
                MessageBox.Show("파일의 해쉬값을 얻어올 수 없습니다"); //에러 메시지 출력
                return "Error"; //"오류" 반환
            }
        }

        public static void SaveStreamAsFile(string filePath, string fileName, Stream inputStream)
        {
            DirectoryInfo info = new DirectoryInfo(filePath);
            if (!info.Exists)
            {
                info.Create();
            }

            string path = Path.Combine(filePath, fileName);
            using (FileStream outputFileStream = new FileStream(path, FileMode.Create))
            {
                inputStream.CopyTo(outputFileStream);
            }

            inputStream.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            writeTimeTable writeTimeTable = new writeTimeTable();
            writeTimeTable.Show();
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void listView3_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            button3.PerformClick();
        }

        public static string BytesToString(byte[] bytes)
        {
            string result = "";
            foreach (byte b in bytes) result += b.ToString("x2");
            return result;
        }
    }
}


