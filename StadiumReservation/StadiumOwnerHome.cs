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
    }
}
