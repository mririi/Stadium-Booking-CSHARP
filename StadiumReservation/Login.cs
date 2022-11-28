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

namespace StadiumReservation
{
    public partial class Login : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=STR;Integrated Security=True");

        public Login()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (emailbox.Text != "" && passwordbox.Text != "")
            {
                try
                {
                    SqlDataAdapter cmd = new SqlDataAdapter("select * from [User] where email = '" + emailbox.Text + "' and password= '" + passwordbox.Text + "'", con);
                    DataTable dtable = new DataTable();
                    cmd.Fill(dtable);
                    if (dtable.Rows.Count > 0)
                    {
                        if (dtable.Rows[0][8].ToString() == "0")
                        {
                        PlayerHome screen = new PlayerHome();
                        screen.user = dtable.Rows[0][0].ToString();
                        screen.Show();
                        this.Hide();
                        }
                        else if (dtable.Rows[0][8].ToString() == "1")
                        {
                            StadiumOwnerHome screen = new StadiumOwnerHome();
                            screen.user = dtable.Rows[0][0].ToString();
                            screen.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
            else
            {
                MessageBox.Show("Please fill the email and password fields");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();
        }
    }
}
