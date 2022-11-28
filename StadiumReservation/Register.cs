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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StadiumReservation
{
    public partial class Register : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=STR;Integrated Security=True");
        public Register()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void Signup_Click(object sender, EventArgs e)
        {
            if (emailbox.Text == "" || username.Text == "" || firstname.Text == "" || lastname.Text == "" || address.Text == "" || tel.Text == "")
            {
                MessageBox.Show("Can't be empty");
                return;
            }
            if (passwordbox.Text.Length < 6)
            {
                MessageBox.Show("Password > 6 chars");
                return;
            }
            try
            {
                Int32.Parse(tel.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("telephone has to be a number");
                return;
            }
            try
            {
                SqlCommand cmd = new SqlCommand("insert into [User] (email,password,lastname,firstname,username,address,tel,owner,IDTeam) values (@email,@password,@lastname,@firstname,@username,@address,@tel,@owner,@IDTeam)", con);
                cmd.Parameters.AddWithValue("@email", emailbox.Text);
                cmd.Parameters.AddWithValue("@password", passwordbox.Text);
                cmd.Parameters.AddWithValue("@firstname", firstname.Text);
                cmd.Parameters.AddWithValue("@lastname", lastname.Text);
                cmd.Parameters.AddWithValue("@username", username.Text);
                cmd.Parameters.AddWithValue("@address", address.Text);
                cmd.Parameters.AddWithValue("@tel", tel.Text);
                SqlDataAdapter cmd1 = new SqlDataAdapter("select ID from [Team] where name='" + comboBox1.SelectedItem + "'", con);
                DataTable dtable1 = new DataTable();
                cmd1.Fill(dtable1);
                cmd.Parameters.AddWithValue("@IDTeam", dtable1.Rows[0][0].ToString());
                if (checkBox1.Checked)
                {
                    cmd.Parameters.AddWithValue("@owner", 1);
                }
                else if (!checkBox1.Checked)
                {
                    cmd.Parameters.AddWithValue("@owner", 0);
                }
                con.Open();
                cmd.ExecuteNonQuery();
                Login signIn = new Login();
                signIn.Show();
                this.Hide();
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                con.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Register_Load(object sender, EventArgs e)
        {
            SqlDataAdapter cmd1 = new SqlDataAdapter("select name from [Team]", con);
            DataTable dtable1 = new DataTable();
            cmd1.Fill(dtable1);
            for (int i = 0; i < dtable1.Rows.Count; i++)
            {
                comboBox1.Items.Add(dtable1.Rows[i][0].ToString());
            }
        }
    }
}
