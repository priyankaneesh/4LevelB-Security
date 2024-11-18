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

namespace Security
{
    public partial class Checking : Form
    {
        Class1 c = new Class1();
        Thread threadBtooth;
        public string getmac, key, mac;
        public string wepath, dpath, depath, getpswd;

        private void Checking_Load(object sender, EventArgs e)
        {
            StartBTThread();
        }

        InTheHand.Net.BluetoothAddress[] address_array = new BluetoothAddress[1000];
        public Checking(string pswd)
        {
            getpswd = pswd;
            InitializeComponent();
        }
        private void StartSearchDvce()
        {
            ThreadStart thrStart = new ThreadStart(SearchDevice);
            threadBtooth = new Thread(thrStart);
            threadBtooth.Start();
        }

        private void Checking_FormClosing(object sender, FormClosingEventArgs e)
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
                        //MessageBox.Show("Source" + source);

                        //cbDrivelist.SelectedItem + curntnode.FullPath;



                        string destination = @"C:\WindowSecurity\Zip\" + source;
                        //MessageBox.Show("destination" + destination);
                        //MessageBox.Show(destination);
                        //string destDirectory = Path.GetDirectoryName(destination);
                        //if (File.Exists(source) && Directory.Exists(destDirectory))
                        //{
                        //    File.Copy(source, destination);
                        //}
                        //else
                        //{
                        //    Throw error or alert
                        //}
                       
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
                        if (MessageBox.Show("Folder Encryption On Process", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
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
                        }
                        label1.Show();
                        label1.Text = "Encryption Completed!";
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

        private void Checking_Load_1(object sender, EventArgs e)
        {
            label1.Hide();
        }

        private void SearchDevice()
        {


            int count = -1;
            try
            {
                c.cmd = new SqlCommand("select Upswd4,Upswd3 from tb_UserReg where Uid=1", c.connect());
                SqlDataReader dr = c.cmd.ExecuteReader();
                if (dr.Read())
                {
                    mac = dr[0].ToString();
                    key = dr[1].ToString();
                }
                BluetoothClient bc = new BluetoothClient();

                BluetoothDeviceInfo[] array = bc.DiscoverDevices();
                for (int i = 0; i < array.Length; i++)
                {
                    this.address_array[i] = array[i].DeviceAddress;
                    this.lbDevices.Items.Add(array[i].DeviceAddress);
                }

                //c.cmd = new SqlCommand("select mac from tb_reg where status='Success'", c.connect());
                //string mac = c.cmd.ExecuteScalar().ToString();
                for (int i = 0; i < lbDevices.Items.Count; i++)
                {
                    getmac = lbDevices.Items[i].ToString();

                    if (getmac == mac)
                    {
                        count = 0;
                        break;
                    }


                    if (i == lbDevices.Items.Count - 1)
                        break;
                }

                if (count == -1)
                {
                    c.cmd = new SqlCommand("select Wfpath from tb_WFolder where Uid=1", c.connect());
                    wepath = c.cmd.ExecuteScalar().ToString();
                    timer1.Start();

                    string to = wepath.ToString() + "1";
                    string To = to + ".zip";

                    string output = to + "1" + ".zip";
                    string ZipFileToCreate = To;
                    string DirectoryToZip = wepath.ToString();
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
                        if (System.IO.File.Exists(destination))
                        {
                            System.IO.File.Delete(destination);

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
                        //if (MessageBox.Show("Folder Encryption Completed", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        //{
                        // Directory.Delete(encpath.ToString());
                        System.IO.DirectoryInfo di = new DirectoryInfo(wepath.ToString());

                        foreach (FileInfo file in di.GetFiles())
                        {
                            file.Delete();
                        }
                        foreach (DirectoryInfo dir in di.GetDirectories())
                        {
                            dir.Delete(true);
                        }
                        if (MessageBox.Show("Folder Encryption On Process...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            label1.Show();
                            label1.Text = "Encryption Completed!";
                            this.Close();
                            timer1.Stop();
                            this.Close();
                            //MainPage mp = new MainPage();
                            //mp.Show();

                        }
                    }

                    catch (Exception ex)
                    {
                    }
                    threadBtooth.Abort();
                }
                else
                {
                    lbDevices.Items.Clear();
                    StartBTThread();
                }


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            StartBTThread();
        }


        private void Login4_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }
        private void StartBTThread()
        {
            ThreadStart thrStart = new ThreadStart(SearchDevice);
            threadBtooth = new Thread(thrStart);
            threadBtooth.Start();
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
