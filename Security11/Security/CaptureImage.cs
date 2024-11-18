using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebCam_Capture;
using WinFormCharpWebCam;
using System.Data.SqlClient;


namespace Security
{

    public partial class CaptureImage : Form
    {
        Class1 c = new Class1();
        public CaptureImage()
        {
            InitializeComponent();
        }
        WebCam webcam;
        private void CaptureImage_Load(object sender, EventArgs e)
        {
            pbCaptureImage.InitialImage = null;
            webcam = new WebCam();
            webcam.InitializeWebCam(ref pbCaptureImage);
            webcam.Start();
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //string path = @"F:/Neethu/Security/Security/LogUsers/";
                string name = "image";

                c.cmd = new SqlCommand("select isnull(max(Lgid),0)+1 from tb_LogUsers", c.connect());
                string id = Convert.ToString(c.cmd.ExecuteScalar());
                
               string filename = name + id;
                
                string filepath = "D:/Security/Security/LogUsers/" + filename + ".png";
                //pbCaptureImage.
                pbCaptureImage.Image.Save(@"D:/Security/Security/LogUsers/" + filename + ".png");
                pbCaptureImage.Image.Save(@"C:/WindowSecurity/Images/LogUser/" + filename + ".png");
                //pbCaptureImage.Image.Save(filepath);
                c.cmd = new SqlCommand("insert into tb_LogUsers values(@Lfile,@Ltime)", c.connect());
                c.cmd.Parameters.AddWithValue("@Lfile", filepath);
                c.cmd.Parameters.AddWithValue("@Ltime", System.DateTime.Now);
               int i= c.cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    //timer1.Stop();
                    //pbCaptureImage.Dispose();
                    Login1 l = new Login1();
                    l.Show();
                    this.Close();
                    webcam.Stop();
                }
               
                
            }
            catch (Exception ex)
            {
                Login1 l = new Login1();
                l.Show();
                this.Close();
            }
        }

    }
}