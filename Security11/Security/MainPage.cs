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
using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System.Threading;
using System.IO;
using Ionic.Zip;
using System.IO.Compression;
using System.Security.Cryptography;

namespace Security
{

    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }
        Class1 c = new Class1();
        InTheHand.Net.BluetoothAddress[] address_array = new BluetoothAddress[1000];
        public string wepath, wfpath,key;
        private void btnRegister_MouseEnter(object sender, EventArgs e)
        {
            tt_Register.SetToolTip(btnRegister, "Register Now");
        }

        private void btnRegister_MouseHover(object sender, EventArgs e)
        {
            tt_Register.SetToolTip(btnRegister, "Register Now");
        }

        private void btnLogin_MouseEnter(object sender, EventArgs e)
        {
            tt_login.SetToolTip(btnLogin, "Login Now");
        }

        private void btnLogin_MouseHover(object sender, EventArgs e)
        {
            tt_login.SetToolTip(btnLogin, "Login Now");
        }

        private void btnReset_MouseHover(object sender, EventArgs e)
        {
            tt_Reset.SetToolTip(btnReset, "Reset Password");
        }

        private void btnReset_MouseEnter(object sender, EventArgs e)
        {
            tt_Reset.SetToolTip(btnReset, "Reset Password");
        }

        private void btnExit_MouseEnter(object sender, EventArgs e)
        {
            tt_Exit.SetToolTip(btnExit, "Exit");
        }

        private void btnExit_MouseHover(object sender, EventArgs e)
        {
            tt_Exit.SetToolTip(btnExit, "Exit");

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            c.cmd = new SqlCommand("select count(*) from tb_UserReg", c.connect());
            int count = Convert.ToInt32(c.cmd.ExecuteScalar());
            if (count > 0)
            {
                CaptureImage ci = new CaptureImage();
                ci.Show();
            }
            else
            {
                MessageBox.Show("Complete Your Registration First", "Message", MessageBoxButtons.OK);
            }
            this.Hide();
        }

        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form fc = Application.OpenForms["Checking"];

            if (fc != null)
            {
                fc.Close();
                MessageBox.Show("Try Again After All Other Windows Closed", "Message", MessageBoxButtons.OK);

                if (e.CloseReason == CloseReason.UserClosing)
                {

                    DialogResult result = MessageBox.Show("Do you really want to exit?", "Message", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {

                        c.cmd = new SqlCommand("SELECT dbo.tb_WFolder.Wepath, dbo.tb_UserReg.Upswd3,dbo.tb_WFolder.Wfpath FROM dbo.tb_UserReg INNER JOIN dbo.tb_WFolder ON dbo.tb_UserReg.Uid = dbo.tb_WFolder.Uid", c.connect());
                        //string wepath = c.cmd.ExecuteScalar().ToString();
                        SqlDataReader dr = c.cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            string wepath = Convert.ToString(dr[0]);
                            key = Convert.ToString(dr[1]);
                            string wfpath = Convert.ToString(dr[2]);

                            if (File.Exists(wepath))
                            {
                                Application.Exit();
                            }
                            else
                            {

                                string to = wfpath.ToString() + "1";
                               

                                string To = to + ".zip";

                                string output = to + "1" + ".zip";
                                string ZipFileToCreate = To;
                                string DirectoryToZip = wfpath.ToString();
                                try
                                {
                                    using (ZipFile zip = new ZipFile())
                                    {
                                        zip.StatusMessageTextWriter = System.Console.Out;
                                        zip.AddDirectory(DirectoryToZip); // recurses subdirectories
                                        zip.Save(ZipFileToCreate);
                                    }


                                    EncryptFile(To, output, key);
                                    string source = Path.GetFileName(output);

                                    //cbDrivelist.SelectedItem + curntnode.FullPath;



                                    string destination = @"C:\WindowSecurity\Zip\" + source;
                                    //MessageBox.Show(destination);
                                    string destDirectory = Path.GetDirectoryName(destination);
                                    //if (File.Exists(source) && Directory.Exists(destDirectory))
                                    //{
                                    //    File.Copy(source, destination);
                                    //}
                                    //else
                                    //{
                                    //    // Throw error or alert
                                    //}
                                    if (System.IO.File.Exists(destination))
                                    {
                                        System.IO.File.Delete(@"C:\WindowSecurity\Zip\");

                                    }
                                    if ((System.IO.File.Exists(source)))
                                        try
                                        {

                                            File.Copy(source, destination);
                                        }
                                        catch (System.IO.IOException ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                            return;
                                        }
                                    File.Delete(To);
                                    if (MessageBox.Show("Folder Encryption Completed", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                                    {
                                        // Directory.Delete(encpath.ToString());
                                        System.IO.DirectoryInfo di = new DirectoryInfo(wfpath.ToString());
                                        foreach (FileInfo file in di.GetFiles())
                                        {
                                            file.Delete();
                                        }
                                        foreach (DirectoryInfo dir in di.GetDirectories())
                                        {
                                            dir.Delete(true);
                                        }
                                    }
                                    this.Close();
                                }

                                catch (Exception ex)
                                {
                                    //MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("No Folder Added", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        Environment.Exit(0);
                    }
                }
            }
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            c.cmd = new SqlCommand("Select count(*) from tb_UserReg", c.connect());
            int count = Convert.ToInt32(c.cmd.ExecuteScalar());
            if (count == 1)
            {
                MessageBox.Show("Already Registered", "Message", MessageBoxButtons.OK);
            }

            else
            {
                Register r = new Register();
                r.Show();
                this.Hide();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ViewImage v = new ViewImage();
            v.Show();
            this.Hide();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            
           
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
           
            c.cmd = new SqlCommand("select count(*) from tb_UserReg", c.connect());
            int count = Convert.ToInt32(c.cmd.ExecuteScalar());
            if (count > 0)
            {

                ResetPswd rp = new ResetPswd();
                rp.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Complete Your Registration First", "Message", MessageBoxButtons.OK);
            }
}

        private void btnView_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnView, "View Previous Users");
        }

        private void btnView_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnView, "View Previous Users");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            
            c.cmd = new SqlCommand("SELECT dbo.tb_WFolder.Wepath, dbo.tb_UserReg.Upswd3,dbo.tb_WFolder.Wfpath FROM dbo.tb_UserReg INNER JOIN dbo.tb_WFolder ON dbo.tb_UserReg.Uid = dbo.tb_WFolder.Uid", c.connect());
            //string wepath = c.cmd.ExecuteScalar().ToString();
            SqlDataReader dr = c.cmd.ExecuteReader();
            if (dr.Read())
            {
                string wepath = Convert.ToString(dr[0]);
                key = Convert.ToString(dr[1]);
                string wfpath = Convert.ToString(dr[2]);

                if (File.Exists(wepath))
                {
                    Application.Exit();
                }
                else
                {

                    string to = wfpath.ToString() + "1";
                    string To = to + ".zip";

                    string output = to + "1" + ".zip";
                    string ZipFileToCreate = To;
                    string DirectoryToZip = wfpath.ToString();
                    try
                    {
                        using (ZipFile zip = new ZipFile())
                        {
                            zip.StatusMessageTextWriter = System.Console.Out;
                            zip.AddDirectory(DirectoryToZip); // recurses subdirectories
                            zip.Save(ZipFileToCreate);
                        }


                        EncryptFile(To, output, key);
                        File.Delete(To);
                        if (MessageBox.Show("Folder Encryption Completed", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            // Directory.Delete(encpath.ToString());
                            System.IO.DirectoryInfo di = new DirectoryInfo(wfpath.ToString());
                            foreach (FileInfo file in di.GetFiles())
                            {
                                file.Delete();
                            }
                            foreach (DirectoryInfo dir in di.GetDirectories())
                            {
                                dir.Delete(true);
                            }
                        }
                        this.Close();
                    }

                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No Folder Added", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Environment.Exit(0);

        }

        private void EncryptFile(string inputFile, string outputFile, string key)
        {

            try
            {
                byte[] keyBytes;
                keyBytes = Encoding.Unicode.GetBytes(key);

                Rfc2898DeriveBytes derivedKey = new Rfc2898DeriveBytes(key, keyBytes);

                RijndaelManaged rijndaelCSP = new RijndaelManaged();
                rijndaelCSP.Key = derivedKey.GetBytes(rijndaelCSP.KeySize / 8);
                
                rijndaelCSP.IV = derivedKey.GetBytes(rijndaelCSP.BlockSize / 8);

                ICryptoTransform encryptor = rijndaelCSP.CreateEncryptor();

                FileStream inputFileStream = new FileStream(inputFile, FileMode.Open, FileAccess.Read);

                byte[] inputFileData = new byte[(int)inputFileStream.Length];
                inputFileStream.Read(inputFileData, 0, (int)inputFileStream.Length);

                FileStream outputFileStream = new FileStream(outputFile, FileMode.Create, FileAccess.Write);

                CryptoStream encryptStream = new CryptoStream(outputFileStream, encryptor, CryptoStreamMode.Write);
                encryptStream.Write(inputFileData, 0, (int)inputFileStream.Length);
                encryptStream.FlushFinalBlock();

                rijndaelCSP.Clear();
                encryptStream.Close();
                inputFileStream.Close();
                outputFileStream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Encryption Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
    }
}