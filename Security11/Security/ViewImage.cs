using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace Security
{
    public partial class ViewImage : Form
    {
        Class1 c = new Class1();
        private int clickNo = 0;
        public ViewImage()
        {
            InitializeComponent();
        }
        private bool CopyFolderContents(string SourcePath, string DestinationPath)
        {
            // DestinationPath = @"D:/Security/Security/LogUsers/";
            //SourcePath = @"C:\WindowSecurity/Images/LogUser/";
            SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
            DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";

            try
            {
                if (Directory.Exists(SourcePath))
                {
                    if (Directory.Exists(DestinationPath) == false)
                    {
                        Directory.CreateDirectory(DestinationPath);
                    }

                    foreach (string files in Directory.GetFiles(SourcePath))
                    {
                        FileInfo fileInfo = new FileInfo(files);
                        fileInfo.CopyTo(string.Format(@"{0}\{1}", DestinationPath, fileInfo.Name), true);
                    }

                    foreach (string drs in Directory.GetDirectories(SourcePath))
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(drs);
                        if (CopyFolderContents(drs, DestinationPath + directoryInfo.Name) == false)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void ViewImage_Load(object sender, EventArgs e)
        {
            string d1 = @"D:/Security/Security/LogUsers/";
            string a1 = @"C:\WindowSecurity/Images/LogUser/";
            bool folderExists = Directory.Exists(d1);

            if (!folderExists)
            {
                CopyFolderContents(a1, d1);
            }
            c.cmd = new SqlCommand("SELECT Lfile,Ltime FROM tb_LogUsers WHERE Lgid = (SELECT MAX(Lgid) FROM tb_LogUsers)", c.connect());
            SqlDataReader dr = c.cmd.ExecuteReader();
            if (dr.Read())
            {
                string image = dr[0].ToString();
                string time = dr[1].ToString();

                pbViewImage.Image = new Bitmap(image);
                pbViewImage.SizeMode = PictureBoxSizeMode.StretchImage;
                label1.Text = Convert.ToDateTime(time).ToShortTimeString();
            }
            else
            {
                label1.Hide();
                previous.Hide();
            }
        }

        private void previous_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == MouseButtons.Left)
            {
                clickNo++;

                c.cmd = new SqlCommand("SELECT Lfile,Ltime FROM tb_LogUsers WHERE Lgid = (SELECT MAX(Lgid)-'" + clickNo + "' FROM tb_LogUsers)", c.connect());
                SqlDataReader dr = c.cmd.ExecuteReader();
                if (dr.Read())
                {
                    string image = dr[0].ToString();
                    string time = dr[1].ToString();

                    pbViewImage.Image = new Bitmap(image);
                    pbViewImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    label1.Text = time;
                    
                }
            }
        }

        private void ViewImage_FormClosing(object sender, FormClosingEventArgs e)
        {

            this.Hide();
            MainPage p1 = new MainPage();
                    p1.Show();



            
            
                   




              
            }

        private void ViewImage_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainPage r2 = new MainPage();
            r2.Visible = true;
        }
    }
}
