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
    public partial class closeConfirm : Form
    {
        public delegate void FormSendDataHandler();
        public event FormSendDataHandler callHideForm;

        public closeConfirm()
        {
            InitializeComponent();
        }

        private void radioButtonCheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                radioButton2.Checked = false;
            }

            else
            {
                radioButton2.Checked = true;
            }
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                Application.Exit();
            }

            else
            {
                this.Close();
                //AutoEnter aE = (AutoEnter)Owner;
                this.callHideForm();
            }
        }
    }
}
