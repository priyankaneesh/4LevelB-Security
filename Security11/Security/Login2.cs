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
namespace Security
{
    public partial class Login2 : Form
    {
        public Login2()
        {
            InitializeComponent();
        }

        Class1 c = new Class1();
        public static int count;
        public static bool flag;
        private void btnBrowse_Click(object sender, EventArgs e)

        {
            if (System.IO.File.Exists(@"D:\Security\Security\Temp\output.png"))

                // Use a try block to catch IOExceptions, to
                // handle the case of the file already being
                // opened by another process.
                try
                {
                    System.IO.File.Delete(@"D:\Security\Security\Temp\output.png");
                }
                catch (System.IO.IOException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            if (System.IO.File.Exists(@"C:\windowSecImages\Images\Combined\output.png"))

                // Use a try block to catch IOExceptions, to
                // handle the case of the file already being
                // opened by another process.
                try
                {
                    System.IO.File.Delete(@"C:\windowSecImages\Images\Combined\output.png");
                }
                catch (System.IO.IOException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"C:\";

            openFileDialog1.Title = "Browse Image Files";

            openFileDialog1.CheckFileExists = true;

            openFileDialog1.CheckPathExists = true;

            openFileDialog1.DefaultExt = "jpg";

            openFileDialog1.Filter = "Image Files(*.jpeg;*.bmp;*.png;*.jpg)|*.jpeg;*.bmp;*.png;*.jpg";

            openFileDialog1.FilterIndex = 2;

            openFileDialog1.RestoreDirectory = true;

            openFileDialog1.ReadOnlyChecked = true;

            openFileDialog1.ShowReadOnly = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)

            {


                string Imgpath2 = @"D:\Security\Security\Images1\" + "Img2" + ".png";

                txtUploadFile.Text = openFileDialog1.FileName;

                PbImg1.Image = new Bitmap(openFileDialog1.FileName);
                PbImg1.SizeMode = PictureBoxSizeMode.StretchImage;

                pbImg2.Image = new Bitmap(Imgpath2);
                pbImg2.SizeMode = PictureBoxSizeMode.StretchImage;
                #region Error

                //Bitmap bmp = new Bitmap(pbCombined.Width, pbCombined.Height);
                //pbCombined.DrawToBitmap(bmp, new Rectangle(0, 0, pbCombined.Width, pbCombined.Height));
                //PbImg1.DrawToBitmap(bmp, new Rectangle(PbImg1.Location.X - pbCombined.Location.X, PbImg1.Location.Y - pbCombined.Location.Y, PbImg1.Width, PbImg1.Height));
                //pbImg2.DrawToBitmap(bmp, new Rectangle(pbImg2.Location.X - pbCombined.Location.X, pbImg2.Location.Y - pbCombined.Location.Y, pbImg2.Width, pbImg2.Height));
                //bmp.Save(@"C:\Temp\output.png", System.Drawing.Imaging.ImageFormat.Png);
                //string imgcombined = @"C:\Temp\output.png";
                //pbCombined.Image =new Bitmap(imgcombined);
                #endregion

                String jpg1 = txtUploadFile.Text;
                String jpg2 = @"D:\Security\Security\Images1\Img2.png";
                String jpg3 = @"D:\Security\Security\Temp\output.png";

                Image img1 = Image.FromFile(jpg1);
                Image img2 = Image.FromFile(jpg2);

                int width = img1.Width + img2.Width;
                int height = Math.Max(img1.Height, img2.Height);

                Bitmap img3 = new Bitmap(width, height);
                Graphics g = Graphics.FromImage(img3);

                g.Clear(Color.Black);
                g.DrawImage(img1, new Point(0, 0));
                g.DrawImage(img2, new Point(img1.Width, 0));

                g.Dispose();
                //img1.Dispose();
                //img2.Dispose();
                string ImgOrginal = @"D:\Security\Security\Original\Upload.png";
                String ImgCombined = @"D:\Security\Security\Temp\output.png";
                img3.Save(jpg3, System.Drawing.Imaging.ImageFormat.Png);
                pbCombined.Image = new Bitmap(ImgCombined);
                pbCombined.SizeMode = PictureBoxSizeMode.StretchImage;

                pbOrginalImage.Image = new Bitmap(ImgOrginal);
                pbOrginalImage.SizeMode = PictureBoxSizeMode.StretchImage;
                Bitmap bm1 = (Bitmap)pbCombined.Image;
                Bitmap bm2 = (Bitmap)pbOrginalImage.Image;
                // Make a difference image.
                int wid = Math.Min(bm1.Width, bm2.Width);
                int hgt = Math.Min(bm1.Height, bm2.Height);
                Bitmap bm3 = new Bitmap(wid, hgt);

                // Create the difference image.
                bool are_identical = true;
                Color eq_color = Color.White;
                Color ne_color = Color.Red;
                for (int x = 0; x < wid; x++)
                {
                    for (int y = 0; y < hgt; y++)
                    {
                        if (bm1.GetPixel(x, y).Equals(bm2.GetPixel(x, y)))
                            bm3.SetPixel(x, y, eq_color);
                        else
                        {
                            bm3.SetPixel(x, y, ne_color);
                            are_identical = false;
                        }
                    }
                }

                // Display the result.
                //picResult.Image = bm3;

                this.Cursor = Cursors.Default;
                if ((bm1.Width != bm2.Width) || (bm1.Height != bm2.Height)) are_identical = false;
                if (are_identical)
                {
                    DialogResult result = MessageBox.Show("The images are identical", "Message", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    if (result == DialogResult.OK)
                    {
                        Login3 l = new Login3();
                        l.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("The images are different.. Sorry You Can't Proceed");
                    MainPage mp = new MainPage();
                    mp.Show();
                    this.Hide();
                }
            }
        }

        private void Login2_Load(object sender, EventArgs e)
        {
            pbCombined.Hide();
            PbImg1.Hide();
            pbImg2.Hide();
            panel1.Size = new Size(476, 111);
            panel1.Location = new Point(307, 216);
            string a = @"D:\Security\Security\Images\";
            string b = @"D:\Security\Security\Images1\";
            string c = @"D:\Security\Security\Original\";
            string d = @"D:\Security\Security\Temp\";
            bool folderExists = Directory.Exists(a);
            bool folderExists1 = Directory.Exists(b);
            bool folderExists2 = Directory.Exists(c);
            bool folderExists3 = Directory.Exists(d);
            if (!folderExists)
            {
                Directory.CreateDirectory(a);

                Image bitmap = Image.FromFile(@"C:\WindowSecurity\Images\image1\" + "Img1" + ".png");
                bitmap.Save(@"D:\Security\Security\Images\" + "Img1" + ".png");
            }
            if (!folderExists1)
            {
                Directory.CreateDirectory(b);
            
            Image bitmap = Image.FromFile(@"C:\WindowSecurity\Images\image2\" + "Img2" + ".png");
                bitmap.Save(@"D:\Security\Security\Images1\" + "Img2" + ".png");
            }
            if (!folderExists2)
            {
                Directory.CreateDirectory(c);

                Image bitmap = Image.FromFile(@"C:\WindowSecurity\Images\Original\" + "Upload" + ".png");
                bitmap.Save(@"D:\Security\Security\Original\" + "Upload" + ".png");

            }
           
            if (!folderExists3)
            {
                Directory.CreateDirectory(d);
            }

        }

        private void Login2_FormClosing(object sender, FormClosingEventArgs e)
        {
          
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Do you really want to Close Login?", "4 Level B-Secure System", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    MainPage r = new MainPage();
                    r.Show();
                    this.Hide();
                }
                else
                {


                    Login2 r = new Login2();
                    r.Show();
                    this.Hide();




                }
            }
        }
    }
}