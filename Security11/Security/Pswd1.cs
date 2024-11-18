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

namespace Security
{
    public partial class Pswd1 : Form
    {
        Class1 c = new Class1();
        public Pswd1()
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

            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;
            int xcoordinates = coordinates.X;
            int ycoordinates = coordinates.Y;
            string selcoordinates = xcoordinates + "," + ycoordinates;
            c.cmd = new SqlCommand("update tb_UserReg set Upswd1=@upswd1 where Uid=1", c.connect());
            c.cmd.Parameters.AddWithValue("@upswd1", selcoordinates);
            int i = c.cmd.ExecuteNonQuery();
            if (i > 0)
            {
                Pswd2 p = new Pswd2();
                p.Show();
                this.Hide();
                //main page hide
               // MessageBox.Show("Please Remember This Point  : " + selcoordinates);
                MainPage m = new Security.MainPage();
                m.Hide();
            }
        }

        private void Pswd1_Load(object sender, EventArgs e)
        {
            
            MainPage m = new Security.MainPage();
            m.Hide();
            int Maxitem = 0, j;
            Maxitem = 100;
            progressBar1.Maximum = 100; // your loop max no. 
            progressBar1.Value = 0;

            for (j = 0; j < Maxitem; j++)
            {
                progressBar1.Value += 1;
                //progressBar1.ForeColor = Color.Red;
                if (j == 25)
                {
                    label1.Show();
                    label1.Text = "Completed 25 %";
                    break;

                }
            }
        }

        private void Pswd1_FormClosing(object sender, FormClosingEventArgs e)
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
                    

                    Pswd1 r = new Pswd1();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //private void pbPswd1_MouseEnter(object sender, EventArgs e)
        //{
        //    ToolTip tt = new ToolTip();
        //    tt.SetToolTip(this.pbPswd1, "Please Select Any Point");
        //}

        private void pbPswd1_MouseHover(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.pbPswd1, "Please Select Any Point");
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}