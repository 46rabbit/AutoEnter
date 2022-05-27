using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpdateTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            deleteOldFile();
        }

        private void deleteOldFile()
        {
            string dir = Application.StartupPath;
            string[] fileList = Directory.GetFiles(dir, "*.Todelete");

            foreach(string f in fileList)
            {
                File.Delete(f);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            updateFunc();
        }

        private void updateFunc()
        {
            try
            {
                File.Move(Application.ProductName + ".exe", Path.ChangeExtension(Application.ProductName + ".exe", ".Todelete"));

                var request = (HttpWebRequest)WebRequest.Create("https://46rabbit.github.io/test/UpdateTest.exe");//https://api.github.com/repos/$OWNER/$REPO/contents/$PATH

                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();
                    
                SaveStreamAsFile(Application.StartupPath, Application.ProductName+".exe", responseStream);
            }

            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            this.Close();
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
            Display.Rotate(2, Display.Orientations.DEGREES_CW_90);
        }
    }
}
