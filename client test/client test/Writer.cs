using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client_test
{
    public partial class Writer : Form
    {
        public Writer()
        {
            InitializeComponent();
        }

        private void doneBtn_Click(object sender, EventArgs e) //"완료" 버튼 클릭 시
        {
            string message = nameTxtBox.Text + "@" + indexCombo.Text + "@" + linkTxtBox.Text + "@" + hourCombo.Text + "#" + minCombo.Text;
            //수정한 정보를 통해 메시지 내용 구성
            ((Form1)(this.Owner)).temp = message; //클라이언트 창에 있는 temp변수에 메시지를 저장함
            this.DialogResult = DialogResult.OK;  //현재 창의 결과를 "예"로 반환함
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; //현재 창의 결과를 "아니오"로 반환함
        }
    }
}
