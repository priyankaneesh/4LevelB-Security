using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net.Mail;

using System.IO;
using System.Security.Cryptography;
using Ionic.Zip;

namespace Security
{
    public partial class ResetPswd : Form
    {
        Class1 c = new Class1();
        public ResetPswd()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            /*----- Generate Code-----*/
            Random r = new Random();
            string number = r.Next().ToString();
            /*----- End Generate Code-----*/

            c.cmd = new SqlCommand("select Uemail from tb_UserReg where Uid=1", c.connect());
            string email = c.cmd.ExecuteScalar().ToString();

            c.cmd = new SqlCommand("update tb_UserReg set Uvcode=@vcode where Uid=1", c.connect());
            c.cmd.Parameters.AddWithValue("@vcode", number);
            c.cmd.ExecuteNonQuery();
            //string filePath = @"D:\pics\ble.jpg";
            //LinkedResource inline = new LinkedResource(filePath);
            //inline.ContentId = Guid.NewGuid().ToString();
            //Attachment att = new Attachment(filePath);
            //att.ContentDisposition.Inline = true;
            MailMessage MyMailMessage = new MailMessage();

            MyMailMessage.From = new MailAddress("amminipmg@gmail.com");
            MyMailMessage.To.Add(email);
            
            MyMailMessage.Subject = "4 LEVEL B-SECURITY  -Reset Code";
            MyMailMessage.IsBodyHtml = true;

            //Mail.AlternateViews.Add(htmlView);


            MyMailMessage.Body = "<head><b><h1><font color='blue'>4 LEVEL B-SECURITY</h1></head></b> \"Your System Reset Code is \"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;----><b><i><font size='3' color='red'>" + number + "</fond></i></b>\n";

            MyMailMessage.IsBodyHtml = true;
            //MyMailMessage.Attachments.Add(att);
            SmtpClient SMTPServer = new SmtpClient("smtp.gmail.com");
            SMTPServer.Port = 587;
            SMTPServer.Credentials = new System.Net.NetworkCredential("amminipmg@gmail.com", "3265969p@");
            SMTPServer.EnableSsl = true;
            try
            {

                SMTPServer.Send(MyMailMessage);
                if (MessageBox.Show("Mail Sent Successfully", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    panel2.Visible = true;
                    panel1.Visible = false;
                }


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //Literal1.Text = "Sorry! pls chech the entered email ";

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            c.cmd = new SqlCommand("select Uvcode from tb_UserReg where Uid=1", c.connect());
            string vcode = c.cmd.ExecuteScalar().ToString();

            if (vcode == txtrcode.Text)
            {
                try
                {
                    c.cmd = new SqlCommand("SELECT dbo.tb_WFolder.Wepath, dbo.tb_UserReg.Upswd3 FROM dbo.tb_UserReg INNER JOIN dbo.tb_WFolder ON dbo.tb_UserReg.Uid = dbo.tb_WFolder.Uid", c.connect());
                    //string wepath = c.cmd.ExecuteScalar().ToString();
                    SqlDataReader dr = c.cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        string wepath = Convert.ToString(dr[0]);
                        string key = Convert.ToString(dr[1]);

                        string dpath, depath;

                        string[] split = wepath.Split('.');

                        string fname = split[0].ToString();
                        int count = fname.Length;
                        dpath = fname.Substring(0, count - 2);
                        depath = dpath + ".zip";
                        //DecryptFile(wepath, depath, key);
                        // DirectoryInfo dfpath = new DirectoryInfo(@"D:\");
                        DirectoryInfo dfpath = new DirectoryInfo(@"D:\");


                        using (ZipFile zip = ZipFile.Read(depath))
                        {
                            zip.ExtractAll(dpath, ExtractExistingFileAction.DoNotOverwrite);
                        }
                        if (MessageBox.Show("Process Completed", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            File.Delete(wepath);
                            File.Delete(depath);

                            c.cmd = new SqlCommand("truncate table tb_UserReg", c.connect());
                            c.cmd.ExecuteNonQuery();

                            c.cmd = new SqlCommand("truncate table tb_WFolder", c.connect());
                            c.cmd.ExecuteNonQuery();
                            this.Close();

                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }
            else
            {
                MessageBox.Show("Incorrect Reset Code", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DecryptFile(string inputFile, string outputFile, string key)
        {
            try
            {
                byte[] keyBytes = Encoding.Unicode.GetBytes(key);

                Rfc2898DeriveBytes derivedKey = new Rfc2898DeriveBytes(key, keyBytes);

                RijndaelManaged rijndaelCSP = new RijndaelManaged();
                rijndaelCSP.Key = derivedKey.GetBytes(rijndaelCSP.KeySize / 8);
                rijndaelCSP.IV = derivedKey.GetBytes(rijndaelCSP.BlockSize / 8);
                rijndaelCSP.Padding = PaddingMode.Zeros;
                ICryptoTransform decryptor = rijndaelCSP.CreateDecryptor();

                FileStream inputFileStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read);

                CryptoStream decryptStream = new CryptoStream(inputFileStream, decryptor, CryptoStreamMode.Read);

                byte[] inputFileData = new byte[(int)inputFileStream.Length];
                decryptStream.Read(inputFileData, 0, (int)inputFileStream.Length);

                FileStream outputFileStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write);
                outputFileStream.Write(inputFileData, 0, inputFileData.Length);
                outputFileStream.Flush();

                rijndaelCSP.Clear();

                decryptStream.Close();
                inputFileStream.Close();
                outputFileStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Decryption Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("File Decryption Complete!");
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button1,"Sent reset code to your mail");
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            toolTip2.SetToolTip(button2, "Reset Your 4 Level B-Security Account");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ResetPswd_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainPage r2 = new MainPage();
            r2.Visible = true;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            c.cmd = new SqlCommand("select Uvcode from tb_UserReg where Uid=1", c.connect());
            string vcode = c.cmd.ExecuteScalar().ToString();

            if (vcode == txtrcode.Text)
            {
                try
                {
                    c.cmd = new SqlCommand("SELECT dbo.tb_WFolder.Wepath, dbo.tb_UserReg.Upswd3 FROM dbo.tb_UserReg INNER JOIN dbo.tb_WFolder ON dbo.tb_UserReg.Uid = dbo.tb_WFolder.Uid", c.connect());
                    //string wepath = c.cmd.ExecuteScalar().ToString();
                    SqlDataReader dr = c.cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        string wepath = Convert.ToString(dr[0]);
                        string key = Convert.ToString(dr[1]);

                        string dpath, depath;

                        string[] split = wepath.Split('.');

                        string fname = split[0].ToString();
                        int count = fname.Length;
                        dpath = fname.Substring(0, count - 2);
                        depath = dpath + ".zip";
                        DecryptFile(wepath, depath, key);
                        // DirectoryInfo dfpath = new DirectoryInfo(@"D:\");
                        DirectoryInfo dfpath = new DirectoryInfo(@"D:\");


                        using (ZipFile zip = ZipFile.Read(depath))
                        {
                            zip.ExtractAll(dpath, ExtractExistingFileAction.DoNotOverwrite);
                        }
                        if (MessageBox.Show("Process Completed", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            File.Delete(wepath);
                            File.Delete(depath);

                            c.cmd = new SqlCommand("truncate table tb_UserReg", c.connect());
                            c.cmd.ExecuteNonQuery();

                            c.cmd = new SqlCommand("truncate table tb_WFolder", c.connect());
                            c.cmd.ExecuteNonQuery();
                            this.Close();

                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }
            else
            {
                MessageBox.Show("Incorrect Reset Code", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

