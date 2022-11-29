using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StadiumReservation
{
    public partial class PlayerHome : Form
    {
        public string user { get; set; }
        public PlayerHome()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reservation screen = new Reservation();
            screen.user = user;
            screen.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PlayerProfile screen = new PlayerProfile();
            screen.user = user;
            screen.Show();
        }
    }
}
