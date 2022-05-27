using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace auto_Enter
{
    public partial class writeTimeTable : Form
    {
        string startupPath = Application.StartupPath;
        string timeTableSavePath;

        bool isSaved = false;

        public writeTimeTable()
        {
            InitializeComponent();

            //Process.Start(startupPath);

            timeTableSavePath = startupPath + "\\timeTableSave.txt";

            FileInfo fileInfo = new FileInfo(timeTableSavePath);//classSave 파일이 존재하는지 확인    
            if (!fileInfo.Exists)                                             //존재하지 않을 경우                                            
            {
                fileInfo.Create(); //파일을 생성함
            }

            else //텍스트 파일에서 저장된 프리셋들을 불러옴
            {
                using (StreamReader inputFile = new StreamReader(timeTableSavePath)) //파일을 읽기 위해 변수 선언
                {
                    string line; //임시 변수 선언

                    while ((line = inputFile.ReadLine()) != null) //EOF까지 각 줄을 읽음
                    {
                        ListViewItem listViewItem = new ListViewItem(); //시간표 프리셋 창에 아이템을 추가하기 위해 변수 선언
                        listViewItem.Text = line.Split(';')[0]; //현재 줄에서 과목 이름을 분리해 변수에 추가
                        listViewItem.SubItems.Add(line.Split(';')[1]); //현재 줄에서 링크르르 분리해 변수에 추가

                        presetListview.Items.Add(listViewItem); //아이템 추가
                    }
                }
            }
        }

        private void addBtn_Click(object sender, EventArgs e) //"추가" 버튼 클릭 시
        {
            ListViewItem listViewItem = new ListViewItem(); //항목 1에 시간표를 추가하기 위한 코드
            listViewItem.Text = textBox1.Text;
            listViewItem.SubItems.Add(textBox4.Text);

            presetListview.Items.Add(listViewItem); //프리셋 시간표들을 저장하는 배열에 추가

            wirteToFile(); //추가된 시간표를 파일에 씀
        }

        private void deleteBtn_Click(object sender, EventArgs e) //"삭제" 버튼 클릭 시
        {
            if (presetListview.SelectedItems == null) //선택된 아이템이 없을 경우
                return;

            presetListview.Items.RemoveAt(presetListview.FocusedItem.Index); //선택된 항목을 지움
            wirteToFile(); //삭제된 시간표를 파일에서 지움
        }

        private void wirteToFile() //시간표 프리셋 저장용 텍스트 파일을 작성하는 함수
        {
            using (StreamWriter outputFile = new StreamWriter(timeTableSavePath, false)) //파일에 쓰기 위해 변수 선언
            {
                for (int i = 0; i < presetListview.Items.Count; i++) //presetListview의 크기 동안 반복
                {
                    outputFile.WriteLine(presetListview.Items[i].Text + ";" + presetListview.Items[i].SubItems[1].Text);
                    //presetListview의 각 항목을 형식에 맞추어 텍스트 파일에 작성함
                }
            }
        }

        private void timeVal_Changed(object sender, EventArgs e) //수업 시간, 쉬는 시간 등 설정값이 바뀌거나 시간표 표시 창에 아이템이 추가됐을 경우
        {
            isSaved = false; //저장 여부를 거짓으로 함
        }

        private void insertBtn_Click(object sender, EventArgs e) //"삽입 버튼"(UI 상에서는 <=)을 클릭했을 때
        {
            if (presetListview.SelectedItems == null) //선택된 아이템이 없을 경우
                return;

            int selectedIndex = presetListview.FocusedItem.Index; //선택된 아이템의 인덱스를 얻어옴

            ListViewItem listViewItem = new ListViewItem(); //시간표 표시 창에 선택된 아이템을 추가하기 위해 변수 선언
            listViewItem.Text = presetListview.Items[selectedIndex].Text; //선택된 아이템의 과목명 추가
            listViewItem.SubItems.Add(presetListview.Items[selectedIndex].SubItems[1].Text); //선택된 아이템의 링크 추가
            listViewItem.SubItems.Add(""); //시간 변수 초기화(추후에 새로고침을 통해 시간을 작성하기 때문에 비워둠)

            timeTableShowView.Items.Add(listViewItem); //시간표 표시 창에 아이템 추가

            timeVal_Changed(this, new EventArgs()); //값 변경을 알림(저장이 필요)
        }

        private void takeoutBtn_Click(object sender, EventArgs e) //"제거 버튼"(UI 상에서는 =>)을 클릭했을 때
        {
            if (timeTableShowView.SelectedItems == null)//선택된 아이템이 없을 경우
                return;

            timeTableShowView.Items.RemoveAt(timeTableShowView.FocusedItem.Index); //선택한 아이템 제거
        }

        private void refreashBtn_Click(object sender, EventArgs e) //"새로고침" 버튼 클릭 시
        {
            if (classTime.Text.Trim() == "" || restingTime.Text.Trim() == "") //만약 수업 시간 또는 쉬는 시간 입력란이 비어있을 경우
                return; //취소

            int hour = int.Parse(hourCombo.Text); //시작 시간에서 선택된 시간을 정수형으로 변환
            int min = int.Parse(minCombo.Text);   //시작 분에서 선택된 분을 정수형으로 변환

            int interval = int.Parse(classTime.Text) + int.Parse(restingTime.Text); //수업시간과 쉬는 시간을 합쳐 시간표 계산을 용이하게 만듦

            if (timeTableShowView.Items.Count == 0) //시간표 작성 창에 추가된 시간표가 없을 경우
                return;

            for (int i = 0; i < timeTableShowView.Items.Count; i++) //시간표 창에 존재하는 
            {
                min += interval; //min 변수에 interval을 더함

                if (min >= 60) //만약 min이 60을 넘을 경우(hour 변수에 1을 추가해야함)
                {
                    hour += min / 60; //min을 60으로 나눈 몫(더해야 하는 시간)을 시간에 더함
                    min = (min % 60); //min은 min을 60으로 나눈 나머지(더하고 남은 나머지)로 설정함
                }

                string _hour = hour.ToString();
                string _min = min.ToString();

                if(hour < 10)
                {
                    _hour = "0" + _hour;
                }

                if (min < 10)
                {
                    _min = "0" + _min;
                }


                timeTableShowView.Items[i].SubItems[2].Text = _hour + " : " + _min; //구해진 시간을 시간표 목록에 적용함
            }
        }

        private void addBtn_Click_1(object sender, EventArgs e) //"추가" 버튼을 누를 시
        {
            string path = startupPath + "\\timeTableSave\\" + fileName.Text + ".txt"; //시간표 파일명을 파일 이름으로 한 텍스트 파일 경로를 만듦

            if (File.Exists(path)) //만약에 같은 이름의 파일이 존재할경우
            {
                var msgBox = MessageBox.Show("이미 존재하는 파일명입니다. 덮어쓰시겠습니까?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning); //덮어쓰기 경고를 보냄

                if(msgBox == DialogResult.Yes) //덮어쓰기에 "예"를 누를경우
                {
                    File.Delete(path); //파일을 삭제함
                    //아래에서 새로 생성하므로 결과적으론 덮어쓰기의 결과를 얻을 수 있음
                }

                else
                {
                    return;
                }
            }

            using (StreamWriter sw = File.CreateText(path)) //텍스트 파일(경로 : path)을 만듦
            {
                try
                {
                    for (int i = 0; i < timeTableShowView.Items.Count; i++) //시간표 작성 창에 존재하는 과목의 수만큼 반복
                    {
                        string name = timeTableShowView.Items[i].Text;             //i번째 과목의 이름
                        string link = timeTableShowView.Items[i].SubItems[1].Text; //i번째 과목의 링크
                        string time = timeTableShowView.Items[i].SubItems[2].Text; //i번째 과목의 시간

                        sw.WriteLine(name + ";" + link + ";" + time); //파일에 (이름);(링크);(시간) 형식으로 작성함
                    }

                    isSaved = true; //저장 여부를 참으로 바꿈
                    MessageBox.Show("저장되었습니다.");
                }

                catch (Exception ex) //저장 과정 중 오류 발생 시
                {
                    MessageBox.Show("오류 발생\n" + ex.Message); //오류 메세지 출력
                }
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e) //"취소" 버튼 클릭 시
        {
            if(isSaved) //저장되었을 경우
            {
                var msgBox = MessageBox.Show("종료하시겠습니까?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question); //종료 확인 메시지 출력

                if(msgBox == DialogResult.Yes) //확인 메시지에서 "예"를 누를 경우
                {
                    this.Close(); //창을 닫음
                }
            }

            else
            {
                var msgBox = MessageBox.Show("저장되지 않은 내용이 있습니다.\n종료하시겠습니까?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //종료 확인 메시지 출력

                if (msgBox == DialogResult.Yes) //확인 메시지에서 "예"를 누를 경우
                {
                    this.Close(); //창을 닫음
                }
            }
        }
    }
}
