using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Diagnostics;

namespace Security
{
    public partial class Pswd2 : Form
    {
        public Pswd2()
        {
            InitializeComponent();
        }
        public static string ImgUpload, Imgpath1, Imgpath2;
        Class1 c = new Class1();
        private void Pswd2_Load(object sender, EventArgs e)
        {
            label4.Hide();
            btnRnxt.Hide();
            PbImg1.Hide();
            pbImg2.Hide();
            pbUpload.Hide();
            btnSplitShare.Hide();
            //if (System.IO.File.Exists(@"C:\windowSecImages\Images\image1\Img1.png"))
            //{
            //    System.IO.File.Delete(@"C:\windowSecImages\Images\image1\Img1.png");
               
            //}
            //if (System.IO.File.Exists(@"C:\windowSecImages\Images\image2\Img2.png"))
            //{
            //    System.IO.File.Delete(@"C:\windowSecImages\Images\image2\Img2.png");
                
            //}
            //if (System.IO.File.Exists(@"C:\windowSecImages\Images\Original\Upload.png"))
            //{
            //    System.IO.File.Delete(@"C:\windowSecImages\Images\Original\Upload.png");
                
            //}

            MainPage m = new Security.MainPage();
            m.Hide();
            Pswd3 m1 = new Security.Pswd3();
            m1.Hide();
            int Maxitem = 0, j;
            Maxitem = 100;
            progressBar1.Maximum = 100; // your loop max no. 
            progressBar1.Value = 0;

            for (j = 0; j < Maxitem; j++)
            {
                progressBar1.Value += 1;
                if (j == 50)
                {
                    label1.Show();
                    label1.Text = "Completed 50 %";
                    break;

                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            pbUpload.Visible = true;
            btnSplitShare.Visible = true;
            
           
            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.InitialDirectory = @"C:\Program Files\";



            openFileDialog1.Title = "Browse Text Files";

            openFileDialog1.CheckFileExists = true;

            openFileDialog1.CheckPathExists = true;

            openFileDialog1.DefaultExt = "jpg";

            openFileDialog1.Filter = "Image Files(*.jpeg;*.jpg)|*.jpeg;*.jpg";

            openFileDialog1.FilterIndex = 1;

            openFileDialog1.RestoreDirectory = true;

            openFileDialog1.ReadOnlyChecked = true;

            openFileDialog1.ShowReadOnly = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)

            {
                txtUploadFile.Text = openFileDialog1.FileName;
                pbUpload.Image = new Bitmap(openFileDialog1.FileName);
                pbUpload.SizeMode = PictureBoxSizeMode.StretchImage;
            }

        }

        private void Pswd2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Do you really want to Close Registration?", "4 Level B-Secure System", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                    c.cmd = new SqlCommand("truncate table tb_UserReg", c.connect());
                    c.cmd.ExecuteNonQuery();

                    c.cmd = new SqlCommand("truncate table tb_WFolder", c.connect());
                    c.cmd.ExecuteNonQuery();
                    MainPage p1 = new MainPage();
                    p1.Show();




                }
                else
                {


                    Pswd2 r = new Pswd2();
                    r.Show();
                    this.Hide();




                }

            }
            else
            {
                MainPage p1 = new MainPage();
                p1.Show();
            }

        }

        private void btnRnxt_Click(object sender, EventArgs e)
        {
            try

            {
                if (txtUploadFile.Text != null)
                {

                    c.cmd = new SqlCommand("update tb_UserReg set Upswd2=@pswd2  where Uid=1", c.connect());
                    c.cmd.Parameters.AddWithValue("@pswd2", ImgUpload);
                    int i = c.cmd.ExecuteNonQuery();
                    {
                        Pswd3 p = new Pswd3();
                        p.Show();
                        this.Hide();
                        MainPage m = new Security.MainPage();
                        m.Hide();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Must Select a Image File", "Message", MessageBoxButtons.OK);

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBrowse_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnBrowse, "Image Browser");
        }

        private void btnBrowse_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnBrowse, "Image Browser");
        }

        

        private void lbNext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)

        {
            //try

            //{
            //    if (txtUploadFile.Text != null)
            //    {

            //        c.cmd = new SqlCommand("update tb_UserReg set Upswd2=@pswd2  where Uid=1", c.connect());
            //        c.cmd.Parameters.AddWithValue("@pswd2", ImgUpload);
            //        int i = c.cmd.ExecuteNonQuery();
            //        {
            //            Pswd3 p = new Pswd3();
            //            p.Show();
            //            this.Hide();
            //            MainPage m = new Security.MainPage();
            //            m.Hide();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Must Select a Image File", "Message", MessageBoxButtons.OK);

            //}
        }



        private void btnSplitShare_Click(object sender, EventArgs e)
        {
            PbImg1.Visible = true;
            pbImg2.Visible = true;
            btnRnxt.Hide();
            label4.Show();
            label4.Text = "Your Share";


            try

            {
                if (txtUploadFile.Text != null)
                {
                    Bitmap originalImage = new Bitmap(Image.FromFile(txtUploadFile.Text));
                    originalImage.Save(@"D:\Security\Security\Original\" + "Upload" + ".png");
                    originalImage.Save(@"C:\WindowSecurity\Images\Original\" + "Upload" + ".png");
                    Rectangle rect = new Rectangle(0, 0, originalImage.Width / 2, originalImage.Height);

                    Bitmap img1 = originalImage.Clone(rect, originalImage.PixelFormat);

                    img1.Save(@"D:\Security\Security\Images\" + "Img1.png");
                    img1.Save(@"C:\WindowSecurity\Images\image1\" + "Img1.png");

                    rect = new Rectangle(originalImage.Width / 2, 0, originalImage.Width / 2, originalImage.Height);
                    Bitmap img2 = originalImage.Clone(rect, originalImage.PixelFormat);
                    img2.Save(@"D:\Security\Security\Images1\" + "Img2.png");
                    img2.Save(@"C:\WindowSecurity\Images\image2\" + "Img2.png");
                    PbImg1.Image = img1;
                    PbImg1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbImg2.Image = img2;
                    pbImg2.SizeMode = PictureBoxSizeMode.StretchImage;
                    ImgUpload = @"D:\Security\Security\Original\" + "Upload" + ".png";
                    //ImgUpload = @"D:\Security\Security\Original\" + "Upload" + ".png";
                    //Imgpath1 = @"D:\Security\Security\Images\" + "Img1" + ".png";
                    //Imgpath2 = @"D:\Security\Security\Images1\" + "Img2" + ".png";
                    btnRnxt.Visible = true;

                }
                else
                {
                    MessageBox.Show("Must Select a Image File", "Message", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Must Select a Image File", "Message", MessageBoxButtons.OK);

            }
        }
    }
}