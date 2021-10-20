using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace auto_Enter
{
    public partial class manageTimeTable : Form
    {
        bool isAdd = false;

        public manageTimeTable(string name, string link, string time, bool isAdd)
        {
            InitializeComponent();

            nameTxtbox.Focus();
            nameTxtbox.Text = name;
            linkTxtbox.Text = link;

            string hour = time.Split(':')[0].Trim();
            string min = time.Split(':')[1].Trim();

            textBox2.Text = hour;
            textBox1.Text = min;
        }

        public manageTimeTable()
        {
            InitializeComponent();

            button1.Text = "추가";
            isAdd = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string editHour = textBox2.Text;
            string editmin = textBox1.Text;

            if(Int32.Parse(textBox2.Text) > 24 || Int32.Parse(textBox1.Text) > 59)
            {
                MessageBox.Show("시간 입력이 잘못되었습니다. \n시간은 24시간 형식이고, 분은 최대 59분까지입니다. (정각은 00으로 표기합니다)\n예) 5시 23분, 23시 11분");
                return;
            }

            /*if(textBox2.Text.Trim().Length == 1)
            {
                editHour = "0" + textBox2.Text;
            }

            if(textBox1.Text.Trim().Length == 1)
            {
                editmin = "0" + textBox1.Text;
            }*/

            string time = editHour + " : " + editmin;

            if (!isAdd)
            {
                ((AutoEnter)(this.Owner)).setListViewItem(nameTxtbox.Text, linkTxtbox.Text, time);
                this.Close();
            }

            else
            {
                ((AutoEnter)(this.Owner)).timeSaveList.Add(time);
                ((AutoEnter)(this.Owner)).realignTimeList();
                ((AutoEnter)(this.Owner)).addListViewItem(nameTxtbox.Text, linkTxtbox.Text, time);
                this.Close();
            }
        }
    }
}
