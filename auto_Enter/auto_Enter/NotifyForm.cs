using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace auto_Enter
{
    public partial class NotifyForm : Form
    {
        private HelpProvider hlpProvider;

        //double height = SystemParameters.;
        double width = SystemParameters.FullPrimaryScreenWidth;

        public NotifyForm(string msg)
        {
            InitializeComponent();

            sender.Text = msg.Split(';')[0];
            msgContents.Text = msg.Split(';')[1];

            
        }

        private void msgContents_MouseHover(object sender, EventArgs e)
        {
            hlpProvider = new HelpProvider();

            hlpProvider.SetShowHelp(msgContents, true);

            hlpProvider.SetHelpString(msgContents, msgContents.Text);
        }
    }
}
