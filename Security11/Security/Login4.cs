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
using Ionic.Zip;
using System.IO.Compression;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Structure;


namespace Security
{
    public partial class Login4 : Form
    {
        public Login4()
        {
            InitializeComponent();
        }
        Class1 c = new Class1();
        public string wepath, dpath, depath, key;
        InTheHand.Net.BluetoothAddress[] address_array = new BluetoothAddress[1000];

        private void btnGo_Click_1(object sender, EventArgs e)
        {
            c.cmd = new SqlCommand("select Upswd3 from tb_UserReg where Ustatus='Success'", c.connect());
            string pswd = c.cmd.ExecuteScalar().ToString();

            if (txtPswd.Text == pswd)
            {
                key = txtPswd.Text;
                try
                {
                    c.cmd = new SqlCommand("select Wepath from tb_WFolder", c.connect());
                    wepath = c.cmd.ExecuteScalar().ToString();
                    string fileName = Path.GetFileName(wepath);
                    string source = @"C:\WindowSecurity\Zip\" + fileName;
                    if (!(System.IO.File.Exists(wepath)))
                        //try {
                            try
                            {

                                File.Copy(source, wepath);
                            }
                            catch (System.IO.IOException ex)
                            {
                                MessageBox.Show(ex.Message);
                                return;
                            }

                        //    string fileName = Path.GetFileName(wepath);

                        //    string source = @"C:\WindowSecurity\Zip\" + fileName;

                        //    string destination = wepath;
                        //    MessageBox.Show(destination);
                        //    string destDirectory = Path.GetDirectoryName(destination);
                            
                        //        File.Copy(source, destination);
                            
                        //}
                        //catch(Exception)
                        //{

                        //}
                   // if ((System.IO.File.Exists(source)))
                        
                    string dpath, depath;

                        string[] split = wepath.Split('.');

                        string fname = split[0].ToString();
                        int count = fname.Length;
                        dpath = fname.Substring(0, count - 2);
                        depath = dpath + ".zip";

                        DecryptFile(wepath, depath, key);
                        // DirectoryInfo dfpath = new DirectoryInfo(@"D:\");
                        using (ZipFile zip = ZipFile.Read(depath))
                        {
                            zip.ExtractAll(dpath, ExtractExistingFileAction.DoNotOverwrite);
                        }
                        if (MessageBox.Show("Process Completed", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            File.Delete(wepath);
                            File.Delete(depath);
                            this.Hide();

                            Checking check = new Checking(txtPswd.Text);
                            check.Show();
                            //tmr_btooth.Start();

                        }

                    
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                MessageBox.Show("Invalid Password", "Message", MessageBoxButtons.OK);
            }

        }

        private void Login4_Load_1(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
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
    }
}