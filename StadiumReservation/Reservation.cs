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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ScrollBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace StadiumReservation
{
    public partial class Reservation : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=STR;Integrated Security=True");
        public string user { get; set; }
        public Reservation()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void stadiums_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Reservation_Load(object sender, EventArgs e)
        {
            SqlDataAdapter cmd1 = new SqlDataAdapter("select name from [Stadiums]", con);
            DataTable dtable1 = new DataTable();
            cmd1.Fill(dtable1);
            for (int i = 0; i < dtable1.Rows.Count; i++)
            {
                stadiums.Items.Add(dtable1.Rows[i][0].ToString());
            }
            SqlDataAdapter cmd2 = new SqlDataAdapter("select starttime,endtime,name from [Reservation],[Stadiums] where IDPlayer='" + user + "' and [Reservation].IDStadium=[Stadiums].ID", con);
            DataTable dtable2 = new DataTable();
            cmd2.Fill(dtable2);
            if (dtable2.Rows.Count > 0)
            {
                label3.Text = "You have reservation from " + dtable2.Rows[0][0].ToString() + " to " + dtable2.Rows[0][1].ToString() + " in Stadium " + dtable2.Rows[0][2].ToString();
                button1.Enabled = false;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter cmd1 = new SqlDataAdapter("select * from [Reservation] where starttime between "+textBox1.Text+" and "+textBox2.Text+" or endtime between "+textBox1.Text+" and "+textBox2.Text, con);
            DataTable dtable1 = new DataTable();
            cmd1.Fill(dtable1);
            if (dtable1.Rows.Count > 0)
            {
                MessageBox.Show("This time periode is booked");
                return;
            }
            if (textBox1.Text=="" || textBox2.Text=="" || stadiums.SelectedItem==null)
            {
                MessageBox.Show("Can't be empty");
                return;
            }
            try
            {
                Int32.Parse(textBox1.Text);
                Int32.Parse(textBox2.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Start time and end time have to be numbers");
                return;
            }
            if (Int32.Parse(textBox1.Text) >= Int32.Parse(textBox2.Text))
            {
                MessageBox.Show("End time has to be after start time");
                return;
            }
            try
            {
                SqlCommand cmd = new SqlCommand("insert into [Reservation] (starttime,endtime,IDStadium,IDPlayer) values (@starttime,@endtime,@IDStadium,@IDPlayer)", con);
                cmd.Parameters.AddWithValue("@starttime", textBox1.Text);
                cmd.Parameters.AddWithValue("@endtime", textBox2.Text);
                cmd.Parameters.AddWithValue("@IDPlayer", user);
                SqlDataAdapter cmd2 = new SqlDataAdapter("select ID from [Stadiums] where name='" + stadiums.SelectedItem + "'", con);
                DataTable dtable2 = new DataTable();
                cmd2.Fill(dtable2);
                cmd.Parameters.AddWithValue("@IDStadium", dtable2.Rows[0][0].ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("BOOKED!");
                Reservation reservation = new Reservation();
                reservation.user = user;
                reservation.Show();
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("Delete from [Reservation] where IDPlayer='" + user + "'", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Canceled!");
                    Reservation reservation = new Reservation();
                    reservation.user = user;
                    reservation.Show();
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
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
            
        }
    }
}
