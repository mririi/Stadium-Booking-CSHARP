using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StadiumReservation
{
    public partial class StadiumOwnerProfile : Form
    {
        public string user { get; set; }
        SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=STR;Integrated Security=True");

        public StadiumOwnerProfile()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void StadiumOwnerProfile_Load(object sender, EventArgs e)
        {
            
            SqlDataAdapter cmd = new SqlDataAdapter("select * from [User] where ID='" + user + "'", con);
            DataTable dtable = new DataTable();
            cmd.Fill(dtable);
            if (dtable.Rows.Count > 0)
            {
                emailbox.Text = dtable.Rows[0][1].ToString();
                passwordbox.Text = dtable.Rows[0][2].ToString();
                lastname.Text = dtable.Rows[0][3].ToString();
                firstname.Text = dtable.Rows[0][4].ToString();
                username.Text = dtable.Rows[0][5].ToString();
                address.Text = dtable.Rows[0][6].ToString();
                tel.Text = dtable.Rows[0][7].ToString();
                
            }
        }

        private void Modify_Click(object sender, EventArgs e)
        {
            if (emailbox.Text == "" || username.Text == "" || firstname.Text == "" || lastname.Text == "" || address.Text == "" || tel.Text == "" )
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
                string Query = "UPDATE [USER] set email = '" + emailbox.Text + "', password='" + passwordbox.Text + "', lastname='" + lastname.Text + "', firstname='" + firstname.Text + "', username='" + username.Text + "', address='" + address.Text + "', tel='" + tel.Text + "' WHERE ID='" + user + "'";
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
