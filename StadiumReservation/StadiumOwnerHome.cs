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
    public partial class StadiumOwnerHome : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=STR;Integrated Security=True");
        public string user { get; set; }
        public StadiumOwnerHome()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void StadiumOwnerHome_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyStadiums myStadiums = new MyStadiums();
            myStadiums.user = user;
            myStadiums.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StadiumOwnerProfile myStadiumProfile = new StadiumOwnerProfile();
            myStadiumProfile.user = user;
            myStadiumProfile.Show();
        }
    }
}
