using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Security
{
    public partial class Login1 : Form
    {
        Class1 c = new Class1();
        private int clickNo = 0;


        public Login1()
        {
            InitializeComponent();
        }

        private void pbPswd1_Click(object sender, EventArgs e)
        {
            
            
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;
            int xcoordinates = coordinates.X;
            int ycoordinates = coordinates.Y;
            ttPosition.SetToolTip(pbPswd1, coordinates.ToString());


        }

        

        private void pbPswd1_DoubleClick(object sender, EventArgs e)
        {
            try {

                MouseEventArgs me = (MouseEventArgs)e;
                if (me.Button == MouseButtons.Left)
                {
                    clickNo++;

                    if (clickNo <= 2)
                    {
                        Point coordinates = me.Location;
                        int xcoordinates = coordinates.X;
                        int ycoordinates = coordinates.Y;
                        int xprevcoordinates = 0, yprevcoordinates = 0;


                        c.cmd = new SqlCommand("select Upswd1 from tb_UserReg where Uid=1", c.connect());
                        string coord = c.cmd.ExecuteScalar().ToString();
                        string[] splitcoord = coord.Split(',');
                        int k = splitcoord.Length;
                        xprevcoordinates = Convert.ToInt32(splitcoord[0]);
                        yprevcoordinates = Convert.ToInt32(splitcoord[1]);

                        //Distance Formula Implementation
                        double distance = GetDistance(xcoordinates, ycoordinates, xprevcoordinates, yprevcoordinates);
                        if (distance <= 5)
                        {
                            Login2 l = new Login2();
                            l.Show();
                            this.Hide();
                            MessageBox.Show("Success");
                        }
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Sorry Timeout,Try Later..", "Message", MessageBoxButtons.OK);
                        if (result == DialogResult.OK)
                        {
                            this.Hide();
                            Application.Exit();
                        }

                    }
                }
            }
            catch(Exception)
            {

            }
        }
        private static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        private void Login1_FormClosing(object sender, FormClosingEventArgs e)
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


                    Login1 r = new Login1();
                    r.Show();
                    this.Hide();




                }
            }
        }
    }
}
