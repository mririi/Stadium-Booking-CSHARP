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
    public partial class PlayerProfile : Form
    {
        public string user { get; set; }
        SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=STR;Integrated Security=True");
        public PlayerProfile()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            SqlDataAdapter cmd1 = new SqlDataAdapter("select name from [Team]", con);
            DataTable dtable1 = new DataTable();
            cmd1.Fill(dtable1);
            for (int i = 0; i < dtable1.Rows.Count; i++)
            {
                comboBox1.Items.Add(dtable1.Rows[i][0].ToString());
            }
            SqlDataAdapter cmd = new SqlDataAdapter("select * from [User] where ID='"+user+"'", con);
            DataTable dtable = new DataTable();
            cmd.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                emailbox.Text = dtable.Rows[0][1].ToString();
                passwordbox.Text = dtable.Rows[0][2].ToString();
                lastname.Text = dtable.Rows[0][3].ToString();
                firstname.Text = dtable.Rows[0][4].ToString();
                username.Text=dtable.Rows[0][5].ToString();
                address.Text = dtable.Rows[0][6].ToString();
                tel.Text = dtable.Rows[0][7].ToString();
                SqlDataAdapter cmd2 = new SqlDataAdapter("select name from [team] where ID='" + dtable.Rows[0][9].ToString() + "'", con);
                DataTable dtable2 = new DataTable();
                cmd2.Fill(dtable2);
                if (dtable2.Rows.Count > 0)
                {
                comboBox1.SelectedItem = dtable2.Rows[0][0].ToString();
                }
            }

        }
        private void Signup_Click(object sender, EventArgs e)
        {
            if (emailbox.Text == "" || username.Text == "" || firstname.Text == "" || lastname.Text == "" || address.Text == "" || tel.Text == "" || comboBox1.SelectedIndex<0)
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
            SqlDataAdapter cmd1 = new SqlDataAdapter("select ID from [team] where name='" + comboBox1.SelectedItem + "'", con);
            DataTable dtable1 = new DataTable();
            cmd1.Fill(dtable1);
            try
            {
                string Query = "UPDATE [USER] set email = '" + emailbox.Text + "', password='" + passwordbox.Text + "', lastname='"+lastname.Text+"', firstname='"+firstname.Text + "', username='"+username.Text + "', address='"+address.Text + "', tel='"+tel.Text + "', IDTeam='" + dtable1.Rows[0][0].ToString()+"' WHERE ID='" + user + "'";
                SqlCommand cmd = new SqlCommand(Query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Updated");
            }
            catch
            {
                MessageBox.Show("error");
            }
            finally
            {
                con.Close();
            }
        }


        }
}
