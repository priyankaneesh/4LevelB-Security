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

    public partial class Register : Form
    {
        Class1 c = new Class1();
        public Register()
        {
            InitializeComponent();
        }

        private void btnRnxt_Click(object sender, EventArgs e)
        {

            if (txtContactNo.Text != "" && txtEmail.Text != "" && txtName.Text != "")
            {

                c.cmd = new SqlCommand("INSERT INTO tb_UserReg (Uname, Uemail, Ucontact, Upswd1, Upswd2, Upswd3, Upswd4, Uvcode, Ustatus, Ubmcode) " +
                        "VALUES (@uname, @uemail, @ucontact, 'Nill', 'Nill', 'Nill', 'Nill', 'Nill', 'Nill', 'Nill')", c.connect());
                c.cmd.Parameters.AddWithValue("@uname", txtName.Text);
                c.cmd.Parameters.AddWithValue("@uemail", txtEmail.Text);
                c.cmd.Parameters.AddWithValue("@ucontact", txtContactNo.Text);

                int i = c.cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    Pswd1 p = new Pswd1();
                    p.Show();
                    this.Hide();

                }
            }
            else
            {
                MessageBox.Show("Please Fill The Form Completely Before Submission");
            }
        }



        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rName = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z ]+$");

            if (txtName.Text.Length > 0 && txtName.Text.Trim().Length != 0)
            {
                if (!rName.IsMatch(txtName.Text.Trim()))
                {
                    label2.Show();
                    label2.Text = "Alphabets && Space Only";
                    txtName.Clear();
                    e.Cancel = true;
                }
            }
            else if (button1.Enabled)
            {
               

            }
            else
            {
                label2.Text = "Must Fill This Fields";
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEmail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");

            if (txtEmail.Text.Length > 0 && txtEmail.Text.Trim().Length != 0)
            {
                if (!rEmail.IsMatch(txtEmail.Text.Trim()))
                {

                    label3.Show();
                    label3.Text = "Check Email Id";
                    txtEmail.Clear();
                    
                    e.Cancel = true;
                }
            }
            else
            {
                label3.Text = "Must Fill This Fields";

            }
        }


        private void txtContactNo_Validating(object sender, CancelEventArgs e)
        {

            System.Text.RegularExpressions.Regex rContactNo = new System.Text.RegularExpressions.Regex(@"^\d{10}$");

            if (txtContactNo.Text.Length > 0 && txtContactNo.Text.Trim().Length != 0)
            {
                if (!rContactNo.IsMatch(txtContactNo.Text.Trim()))
                {
                    label4.Show();
                    label4.Text = "Check Mobile No";
                    txtContactNo.Clear();
                    
                    e.Cancel = true;
                }
            }
            else
            {
                label4.Text ="Must Fill This Fields";

            }

        }

        private void Register_Load(object sender, EventArgs e)
        {
            MainPage m = new Security.MainPage();
            m.Hide();
            label2.Hide();
            label3.Hide();
            label4.Hide();
        }

        private void Register_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Do you really want to Close Registration?", "Dialog Title", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtContactNo.Text = "";

                    MainPage p1 = new MainPage();
                    p1.Show();




                }
                else
                {
                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtContactNo.Text = "";

                    Register r = new Register();
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

        private void button1_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtEmail.Clear();
            txtContactNo.Clear();

            MainPage m = new MainPage();
            m.Show();
            Register n = new Register();
            this.Hide();
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void lbl_Name_Click(object sender, EventArgs e)
        {

        }

        private void lbl_Email_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
                e.Handled = "+-.\b0123456789".IndexOf(e.KeyChar) < 0;
           
        }

        private void txtName_Click(object sender, EventArgs e)
        {
            label2.Hide();
            label3.Hide();
            label4.Hide();
        }

        private void txtEmail_Click(object sender, EventArgs e)
        {
            label2.Hide();
            label3.Hide();
            label4.Hide();
        }

        private void txtContactNo_Click(object sender, EventArgs e)
        {
            label2.Hide();
            label3.Hide();
            label4.Hide();
        }
    }
}