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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace StadiumReservation
{
    public partial class MyStadiums : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=STR;Integrated Security=True");
        public string user { get;set; }
        private string currentname;
        public MyStadiums()
        {
            InitializeComponent();
        }
        public void getStadiums()
        {
            con.Open();
            string Query = "SELECT name,description from Stadiums where IDStadiumOwner='"+user+"'";
            SqlDataAdapter sd = new SqlDataAdapter(Query, con);
            SqlCommandBuilder cmd = new SqlCommandBuilder(sd);
            var ds = new DataSet();
            sd.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        private void MyStadiums_Load(object sender, EventArgs e)
        {
            getStadiums();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                currentname = textBox1.Text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Can't be empty");
                return;
            }
            SqlDataAdapter cmd2 = new SqlDataAdapter("select * from [Stadiums] where name='" + textBox1.Text + "'", con);
            DataTable dtable2 = new DataTable();
            cmd2.Fill(dtable2);
            if (dtable2.Rows.Count > 0)
            {
                MessageBox.Show("This stadium already exists!");
                return;
            }
            try
            {
                SqlCommand cmd = new SqlCommand("insert into [Stadiums] (name,description,IDStadiumOwner) values (@name,@description,@IDStadiumOwner)", con);
                cmd.Parameters.AddWithValue("@name", textBox1.Text);
                cmd.Parameters.AddWithValue("@description", textBox2.Text);
                cmd.Parameters.AddWithValue("@IDStadiumOwner", user);
                con.Open();
                cmd.ExecuteNonQuery();
                
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                con.Close();
                getStadiums();
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                con.Open();
                string Query = "DELETE FROM [Stadiums] where ID=(select ID from [Stadiums] where name='" + textBox1.Text + "')";
                string Query1 = "DELETE FROM [Reservation] where IDStadium=(select ID from [Stadiums] where name='" + textBox1.Text + "')";
                SqlCommand cmd = new SqlCommand(Query, con);
                SqlCommand cmd1 = new SqlCommand(Query1,con);
                cmd1.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Stadium Deleted successfully");

                }
                catch
                {
                    MessageBox.Show("error");
                }
                finally
                {
                con.Close();
                textBox1.Text = "";
                textBox2.Text = "";
                getStadiums();

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != currentname)
            {
            SqlDataAdapter cmd2 = new SqlDataAdapter("select * from [Stadiums] where name='" + textBox1.Text + "'", con);
            DataTable dtable2 = new DataTable();
            cmd2.Fill(dtable2);
            if (dtable2.Rows.Count > 0)
            {
                MessageBox.Show("This stadium already exists!");
                return;
            }

            }
            try
            {
                string Query = "UPDATE Stadiums set name = '" + textBox1.Text + "',description='" + textBox2.Text + "' WHERE ID =(select ID from Stadiums where name='" + currentname + "')";
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
            textBox1.Text = "";
            textBox2.Text = "";
            getStadiums();
            }
        }
    }
}
