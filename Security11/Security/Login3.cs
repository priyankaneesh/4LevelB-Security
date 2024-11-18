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

namespace Security
{
    public partial class Login3 : Form
    {
        Class1 c = new Class1();
        public static int sec=0;
        InTheHand.Net.BluetoothAddress[] address_array = new BluetoothAddress[1000];
        public Login3()
        {
            InitializeComponent();
        }

        private void Login3_Load(object sender, EventArgs e)
        {
            Login2 n = new Login2();
            n.Hide();
            //try
            //{
            //    BluetoothClient bc = new BluetoothClient();

            //    BluetoothDeviceInfo[] array = bc.DiscoverDevices();
            //    for (int i = 0; i < array.Length; i++)
            //    {
            //        this.address_array[i] = array[i].DeviceAddress;
            //        this.lbDevicesList.Items.Add(array[i].DeviceName);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            try
            {

                BluetoothClient bc = new BluetoothClient();

                BluetoothDeviceInfo[] array = bc.DiscoverDevices();
                if (array.Length > 0)
                {

                    for (int i = 0; i < array.Length; i++)
                    {
                        this.address_array[i] = array[i].DeviceAddress;
                        this.lbDevices.Items.Add(array[i].DeviceName);
                        this.lbAddress.Items.Add(array[i].DeviceAddress);

                    }
                    timer1.Start();
                }
                else
                {
                    MessageBox.Show("Try Later,No Bluetooth Connection There");
                    this.Close();
                    for (int i = 0; i < array.Length; i++)
                    {
                        this.address_array[i] = array[i].DeviceAddress;
                        this.lbDevices.Items.Add(array[i].DeviceName);
                        this.lbAddress.Items.Add(array[i].DeviceAddress);

                    }
                    timer1.Start();
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }





        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                lbDevices.Items.Clear();
                BluetoothClient bc = new BluetoothClient();

                BluetoothDeviceInfo[] array = bc.DiscoverDevices();
                for (int i = 0; i < array.Length; i++)
                {
                    this.address_array[i] = array[i].DeviceAddress;
                    this.lbDevices.Items.Add(array[i].DeviceName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

    

        private void timer1_Tick(object sender, EventArgs e)
        {
                this.Hide();
                sec = sec + 1;

                string getmac;
                c.cmd = new SqlCommand("select Upswd4 from tb_UserReg where Ustatus='Success'", c.connect());
                string mac = c.cmd.ExecuteScalar().ToString();
                int count = 0, j;

                for (j = 0; j < lbAddress.Items.Count; j++)
                {
                    getmac = lbAddress.Items[j].ToString();

                    if (getmac == mac)
                    {
                        timer1.Stop();
                        count = 1;
                        Login4 p = new Login4();
                        p.Show();
                        p.TopMost = true;
                        this.Hide();
                        Login3 p1 = new Login3();
                        p1.Close();
                        break;

                    }
                    /////////////
                    //if (sec == lbAddress.Items.Count)
                    //{
                    //    timer1.Stop();
                    //    //MessageBox.Show("Please Connect Bluetooth", "Message", MessageBoxButtons.OK);
                    //    this.Close();
                    //}
                }
            
            



        }
    }
}