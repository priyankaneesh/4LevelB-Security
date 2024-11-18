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
using System.IO;
using System.Net.Mail;
using System.Security.Cryptography;
using Ionic.Zip;
using System.Diagnostics;

namespace Security
{
    public partial class Pswd3 : Form
    {
        
        DirectoryInfo encpath;
        
        Class1 c = new Class1();
        InTheHand.Net.BluetoothAddress[] address_array = new BluetoothAddress[1000];
        public static int index;
        public static string mac, key;
        public Pswd3()
        {

            InitializeComponent();

        }

        private void Pswd3_Load(object sender, EventArgs e)
        {
            pictureBox3.Hide();
            label8.Hide();
            label1.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            label3.Hide();
            btnRnxt.Hide();
            cbDrivelist.Hide();
            tvFolderList.Hide();
            txtCode.Hide();
            MainPage m = new Security.MainPage();
            m.Hide();
            int Maxitem = 0, j;
            Maxitem = 100;
            progressBar1.Maximum = 100; // your loop max no. 
            progressBar1.Value = 0;

            for (j = 0; j < Maxitem; j++)
            {
                progressBar1.Value += 1;
                if (j == 75)
                {
                    label7.Show();
                    label7.Text = "Completed 75%";
                    break;

                }
            }



            c.cmd = new SqlCommand("select Upswd3 from tb_UserReg where Uid=1", c.connect());
            key = Convert.ToString(c.cmd.ExecuteScalar());




            try
            {
                BluetoothClient bc = new BluetoothClient();

                BluetoothDeviceInfo[] array = bc.DiscoverDevices();
                for (int i = 0; i < array.Length; i++)
                {
                    this.address_array[i] = array[i].DeviceAddress;
                    this.lbDevicesList.Items.Add(array[i].DeviceName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

       

        private void Pswd3_FormClosing(object sender, FormClosingEventArgs e)
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


                    Pswd3 r = new Pswd3();
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

     


       

        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitter1_SplitterMoved_1(object sender, SplitterEventArgs e)
        {

        }

        //private void cbDrivelist_SelectedIndexChanged_1(object sender, EventArgs e)
        //{

        //}

        private void lbDevicesList_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            cbDrivelist.ResetText();
            tvFolderList.ResetText();

            txtCode.Clear();
            label1.Hide();
            label4.Hide();

            label6.Hide();
            try
            {
                if (this.lbDevicesList.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a device.", "Warning", MessageBoxButtons.OKCancel);
                    return;
                }

                index = this.lbDevicesList.SelectedIndex;
                mac = this.address_array[index].ToString();

                Random r = new Random();
                string text1 = r.Next().ToString();
                string text = text1.Substring(0, 3);
                string fname = "C:\\Users\\aneesh\\1.txt";
                System.IO.StreamWriter objWriter;
                objWriter = new System.IO.StreamWriter(fname);
                objWriter.Write(text);
                objWriter.Close();
                var file = fname;
                var uri = new Uri("obex://" + mac + "/" + file);
                var request = new ObexWebRequest(uri);
                request.ReadFile(file);
                var response = (ObexWebResponse)request.GetResponse();

                //check response.StatusCode
                response.Close();
                c.cmd = new SqlCommand("update tb_UserReg set Ubmcode=@code where Uid=1", c.connect());
                c.cmd.Parameters.AddWithValue("@code", text);
                c.cmd.ExecuteNonQuery();
                
                DialogResult result = MessageBox.Show("File Transfered", "Message", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    // label1.Show();
                    //label4.Show();
                    //label5.Show();
                    //label6.Show();
                    // 
                    btnRnxt.Show();
                    //cbDrivelist.Show();
                    //tvFolderList.Show();
                    txtCode.Show();

                }
            }
            /////////////////////////////////////////////////////////////
            catch
            {
                MessageBox.Show("Please Make Sure Your Bluetooth connection");
            }

        }

        private void btnRnxt_Click(object sender, EventArgs e)
        {
            if (txtCode.Text == string.Empty)
            {
                if (lbDevicesList.SelectedIndex < 0)
                {
                    cbDrivelist.Show();
                    label3.Show();
                    label6.Show();
                    label6.ForeColor = Color.Red;
                    label6.Text = "Warning!  Select Your Device First";
                }
                else
                {
                    label4.Show();
                    label4.ForeColor = Color.Red;
                    label4.Text = "Warning!  Complete this Fied First";
                    
                }
            
            }
            try
            {

                c.cmd = new SqlCommand("select ubmcode from tb_UserReg where Uid=1", c.connect());
                string enteredText = c.cmd.ExecuteScalar().ToString();
                string text = txtCode.Text;
                if (text == enteredText)
                {

                    label4.Show();
                    label4.ForeColor = Color.Blue;
                    label4.Text = "Successfull!,\nPlease Select Proper Drive From Below.. ";

                    ///////////////////////////////////////////
                    cbDrivelist.Show();
                    label3.Show();
                    cbDrivelist.Items.Clear();
                    System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();
                    for (int i = 0; i <= drives.Length - 1; i++)
                    {
                        cbDrivelist.Items.Add(drives[i].Name);
                    }
                }
                else
                {
                    label4.Show();
                    label4.ForeColor = Color.Red;
                    label4.Text = "Failed !,Complete this Fied First";
                }
            }
            catch (Exception ex)
            {
                label4.Show();
                label4.ForeColor = Color.Red;
                label4.Text = "Failed !";

            }

        
        }

        private void tvFolderList_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
           
            string Upswd11;
            string Upswd12;
            string Upswd13;
            TreeNode curntnode = e.Node;
            encpath = new DirectoryInfo(cbDrivelist.SelectedItem + curntnode.FullPath);
            
            DialogResult dresult = MessageBox.Show("Do You Selected " + encpath + " as your Working Folder to be Secure", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dresult == DialogResult.Yes)
            {
                if (encpath != null)
                {

                    string to = encpath.ToString() + "1";
                    string To = to + ".zip";
                   
                    string output = to + "1" + ".zip";
                    string ZipFileToCreate = To;
                    string DirectoryToZip = encpath.ToString();
                    try
                    {
                        using (ZipFile zip = new ZipFile())
                        {
                            //zip.Password = "yourpassword";
                            //zip.Encryption = EncryptionAlgorithm.PkzipWeak;
                            zip.StatusMessageTextWriter = System.Console.Out;
                            zip.AddDirectory(DirectoryToZip); // recurses subdirectories
                            zip.Save(ZipFileToCreate);
                            // File.SetAttributes(ZipFileToCreate, FileAttributes.Hidden);
                        }

                        /*---- Generate Random No:-------*/
                        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                        byte[] buffer = new byte[4];

                        rng.GetBytes(buffer);
                        int result1 = Math.Abs(BitConverter.ToInt32(buffer, 0));
                        string result = Convert.ToString(result1).Substring(0, 6);
                        string key = (mac + result);
                        EncryptFile(To, output, key);
                        File.Delete(To);


                        ////////////////

                        //string source = @"H:\" + output + ".zip";
                        //string destination = @"C:\WindowSecurity\Zip\" + output + ".zip";
                        //string destDirectory = Path.GetDirectoryName(destination);
                        //if (File.Exists(source) && Directory.Exists(destDirectory))
                        //{
                        //    File.Copy(source, destination);
                        //}
                        //else
                        //{
                        //    // Throw error or alert
                        //}
                        ///////////////////////
                        //if (!Directory.Exists(output)) Directory.CreateDirectory(@" C:\Users\aneesh\"+output);

                        if (MessageBox.Show("Folder Encryption on Process...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            // Directory.Delete(encpath.ToString());
                            System.IO.DirectoryInfo di = new DirectoryInfo(encpath.ToString());

                            foreach (FileInfo file in di.GetFiles())
                            {
                                file.Delete();
                            }
                            foreach (DirectoryInfo dir in di.GetDirectories())
                            {
                                dir.Delete(true);
                            }
                            label1.Hide();

                            //int Max, j1; Max = 0;
                            //progressBar1.Maximum = 100; // your loop max no. 
                            //progressBar1.Value = 0;
                            //for (j1 = 0; j1 < Max; j1++)
                            //{
                            //    progressBar1.Value += 1;
                            //    if (j1 == 75)
                            //    {
                            //        label7.Show();
                            //        label7.ForeColor = Color.Green;
                            //        label7.Text = "Completed 100%";
                            //        break;

                            //    }
                            //}
                            label8.Show();
                            label8.Text = "Encryption Completed!";
                            pictureBox3.Show();
                            int Max, j1; Max = 0;
                            progressBar1.Maximum = 100; // your loop max no. 
                            progressBar1.Value = 0;
                            for (j1 = 0; j1 < Max; j1++)
                            {
                                progressBar1.Value += 1;
                                if (j1 == 75)
                                {
                                    label7.Show();
                                    label7.ForeColor = Color.Green;
                                    label7.Text = "Completed 100%";
                                    break;

                                }
                            }

                            string o = cbDrivelist.SelectedItem + curntnode.FullPath;
                            //cbDrivelist.SelectedItem + curntnode.FullPath;
                            string source = o + "11.zip";
                            

                            string destination = @"C:\WindowSecurity\Zip\" + curntnode.FullPath + "11.zip";
                            //MessageBox.Show(curntnode.FullPath);
                            string destDirectory = Path.GetDirectoryName(destination);
                            //MessageBox.Show(destDirectory);
                            //if (File.Exists(source) && Directory.Exists(destDirectory))
                            //{
                            //    File.Copy(source, destination);
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
                        }




                        
                       // File.SetAttributes(output, FileAttributes.Hidden);
                        //if (MessageBox.Show("Process Completed", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)

                        //{

                        /*  Add Working Folder Into Table  */
                        c.cmd = new SqlCommand("insert into tb_WFolder values(1,@fpath,@epath,0)", c.connect());
                        c.cmd.Parameters.AddWithValue("@fpath", encpath.ToString());
                        c.cmd.Parameters.AddWithValue("@epath", output.ToString());

                        c.cmd.ExecuteNonQuery();
                        c.cmd = new SqlCommand("update tb_UserReg  set Upswd3=@pswd3,Upswd4=@pswd4,Ustatus='Success' where Uid=1", c.connect());
                        c.cmd.Parameters.AddWithValue("@pswd4", mac);
                        c.cmd.Parameters.AddWithValue("@pswd3", key);
                        c.cmd.ExecuteNonQuery();
                        //Retrieve Mobile No:
                        c.cmd = new SqlCommand("select Ucontact from tb_UserReg where Uid=@uid", c.connect());
                        c.cmd.Parameters.AddWithValue("@uid", 1);
                        string contact = c.cmd.ExecuteScalar().ToString();



                        string message = "Registration Completed";
                        string title = "Message";
                        int Maxitem = 0, j;
                        Maxitem = 100;
                        progressBar1.Maximum = 100; // your loop max no. 
                        progressBar1.Value = 0;

                        for (j = 0; j < Maxitem; j++)
                        {
                            progressBar1.Value += 1;
                            if (j == 100)
                            {
                                break;

                            }
                        }
                        DialogResult result2 = MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (result2 == DialogResult.OK)
                        {
                            Process.Start("http://utilities.ociuz.com/sms/sendsms.aspx?userid=hima&pwd=123&to=" + contact + "&msg=" + key);

                            this.Hide();
                            MainPage m = new Security.MainPage();
                            m.Show();

                        }

                        /////////SqlDataReader dr;
                        SqlDataReader DR;
                        c.cmd = new SqlCommand("select Upswd1,Upswd2,Upswd3 from tb_UserReg where Uid=1", c.connect());
                        //string email = c.cmd.ExecuteScalar().ToString();
                        DR = c.cmd.ExecuteReader();
                        if (DR.Read())
                        {
                            Upswd11 = DR[0].ToString();
                            Upswd12 = DR[1].ToString();
                            Upswd13 = DR[2].ToString();
                            



                            MailMessage MyMailMessage = new MailMessage();
                            c.cmd = new SqlCommand("select Uemail from tb_UserReg where Uid=1", c.connect());
                            string email = c.cmd.ExecuteScalar().ToString();
                            MyMailMessage.From = new MailAddress("amminipmg@gmail.com");
                            MyMailMessage.To.Add(email);
                            MyMailMessage.Subject = "4 LEVEL B-SECURITY System";

                            MyMailMessage.IsBodyHtml = true;
                            
                           MyMailMessage.Body = "<head><b><h1><font color='blue'>4 LEVEL B-SECURITY</h1></head></b><br>Your System Security Information Code is below:<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b><font color='red'>Picture Pixel Position:</font></b>" + Upswd11 + "<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b><font color='red'>Password to Decrypt:</font></b>" + Upswd13;
                            MyMailMessage.IsBodyHtml = true;
                            SmtpClient SMTPServer = new SmtpClient("smtp.gmail.com");
                            SMTPServer.Port = 587;
                            SMTPServer.Credentials = new System.Net.NetworkCredential("amminipmg@gmail.com", "3265969p@");
                            SMTPServer.EnableSsl = true;
                            try
                            {

                                SMTPServer.Send(MyMailMessage);
                                if (MessageBox.Show("Mail Sent Successfully", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                                {
                                    //
                                }


                            }

                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //Literal1.Text = "Sorry! pls chech the entered email ";

                            }


                        }

                        //SqlDataReader read = (null);
                        //c.cmd = new SqlCommand("select * from tb_UserReg where Uid=1", c.connect());
                        ////string email = c.cmd.ExecuteScalar().ToString();
                        //read = c.cmd.ExecuteReader();
                        //string email=(read["Uemail"].ToString());
                        //string Upswd1 = (read["Upswd1"].ToString());
                        //string Upswd2 = (read["Upswd2"].ToString());
                        //string Upswd3 = (read["Upswd3"].ToString());
                        //MessageBox.Show(Upswd1);
                        //MailMessage MyMailMessage = new MailMessage();

                        //MyMailMessage.From = new MailAddress("ociuztest@gmail.com");
                        //MyMailMessage.To.Add(email);
                        //MyMailMessage.Subject = "4 LEVEL B-SECURITY System";

                        //MyMailMessage.IsBodyHtml = true;
                        ////Your System <b><i><font size='3' color='red'>Reset Code</fond></i></b>
                        //MyMailMessage.Body = "<head><b><h1><font color='blue'>4 LEVEL B-SECURITY</head></b>\n\tYour System Security Information Code is below:\n\t\t\t <b><font color='red'>Picture Pixel Position:</font></b>" + Upswd1 + "\n\t\t\t <b><font color='red'>Path of Your Image Share:</font></b>" + Upswd2 + "\n\t\t\t <b><font color='red'>Password to Decrypt:</font></b>" + Upswd3;
                        //MyMailMessage.IsBodyHtml = true;
                        //SmtpClient SMTPServer = new SmtpClient("smtp.gmail.com");
                        //SMTPServer.Port = 587;
                        //SMTPServer.Credentials = new System.Net.NetworkCredential("ociuztest@gmail.com", "!123456789");
                        //SMTPServer.EnableSsl = true;
                        //try
                        //{

                        //    SMTPServer.Send(MyMailMessage);
                        //    if (MessageBox.Show("Mail Sent Successfully", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        //    {
                        //        //
                        //    }


                        //}

                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //    //Literal1.Text = "Sorry! pls chech the entered email ";

                        //}
                        
                    }
                    catch (System.Exception ex1)
                    {
                        System.Console.Error.WriteLine("exception: " + ex1);
                    }


                }
                else
                {
                    label1.Show();

                }

            }
        }

        private void tvFolderList_BeforeExpand_1(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                if (e.Node.Nodes[0].Text == "...." || e.Node.Nodes[0].Tag == null)
                {
                    e.Node.Nodes.Clear();
                    try
                    {

                        TreeNode curntnode = e.Node;
                        DirectoryInfo folderpath = new DirectoryInfo(cbDrivelist.SelectedItem + curntnode.FullPath);
                        foreach (DirectoryInfo dir in folderpath.GetDirectories())
                        {
                            TreeNode node = curntnode.Nodes.Add(dir.Name);
                            node.Nodes.Add("");
                        }

                        foreach (FileInfo file in folderpath.GetFiles())
                        {
                            string ext = file.Extension;
                            TreeNode newNode = curntnode.Nodes.Add(file.Name);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void lbDevicesList_MouseEnter_1(object sender, EventArgs e)
        {
            toolTipDevice3.SetToolTip(lbDevicesList, "Device list");
        }

        private void pictureBox1_MouseEnter_1(object sender, EventArgs e)
        {
            toolTipScan1.SetToolTip(pictureBox1, "Bluetooth Device Fetcher");
        }

        private void btnRnxt_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnRnxt, "OTP Checker");
        }

        private void cbDrivelist_MouseEnter_1(object sender, EventArgs e)
        {
            toolTipBrowse2.SetToolTip(cbDrivelist, "Drivers List");
        }

        private void tvFolderList_MouseEnter(object sender, EventArgs e)
        {
            toolTipFolder4.SetToolTip(tvFolderList, "Folder List");
        }

        private void txtCode_MouseEnter(object sender, EventArgs e)
        {
            toolTip2.SetToolTip(txtCode, "Field to Enter OTP Code From Your Phone");
        }

        private void l(object sender, EventArgs e)
        {
            if (lbDevicesList.SelectedIndex != -1)
            {
                //your code logic

                tvFolderList.Nodes.Clear();

                DirectoryInfo path = new DirectoryInfo(cbDrivelist.SelectedItem.ToString());


                try
                {
                    foreach (DirectoryInfo dir in path.GetDirectories())
                    {
                        TreeNode node = tvFolderList.Nodes.Add(dir.Name);
                        node.Nodes.Add("");
                    }

                    foreach (FileInfo file in path.GetFiles())
                    {
                        TreeNode node = tvFolderList.Nodes.Add(file.Name);

                    }
                    label1.Show();
                    label1.ForeColor = Color.Blue;
                    label1.Text = "Please select Your Folder To Make Secure";
                    tvFolderList.Show();
                    label4.Hide();
                   
                   
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select any device");
            }
        }

        private void txtCode_Click(object sender, EventArgs e)
        {
            label5.Hide();

        }

        private void txtCode_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rContactNo = new System.Text.RegularExpressions.Regex(@"^\d{3}$");

            if (txtCode.Text.Length > 0 && txtCode.Text.Trim().Length != 0)
            {
                if (!rContactNo.IsMatch(txtCode.Text.Trim()))
                {
                    label4.Show();
                    label4.ForeColor = Color.Red;
                    label4.Text = "Failed !,Check Your OTP";
                   
                    txtCode.Clear();
                    e.Cancel = true;
                }
            }
            else
            {
                label1.ForeColor = Color.Red;
                label4.Text = "Complete this Fied First";

            }
        }

        private void lbDevicesList_Click(object sender, EventArgs e)
        {
            label6.Hide();
        }

        private void tvFolderList_MouseHover(object sender, EventArgs e)
        {
            toolTipFolder4.SetToolTip(tvFolderList, "Folder List");
        }

        private void txtCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
                e.Handled = "+-.\b0123456789".IndexOf(e.KeyChar) < 0;
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            cbDrivelist.ResetText();
            tvFolderList.ResetText();
            
            
            label1.Hide();
            
            label5.Hide();
            label6.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            cbDrivelist.ResetText();
            tvFolderList.ResetText();
            lbDevicesList.ResetText();
            txtCode.Clear();
            label1.Hide();
            label4.Hide();
            label5.Hide();
            label6.Hide();
            try
            {
                lbDevicesList.Items.Clear();
                BluetoothClient bc = new BluetoothClient();

                BluetoothDeviceInfo[] array = bc.DiscoverDevices();
                for (int i = 0; i < array.Length; i++)
                {
                    this.address_array[i] = array[i].DeviceAddress;
                    this.lbDevicesList.Items.Add(array[i].DeviceName);
                }
                if(lbDevicesList.Text==null)
                {
                    label6.Show();
                    label6.Text="Please Switch On your Bluetooth Device";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        

    }

}