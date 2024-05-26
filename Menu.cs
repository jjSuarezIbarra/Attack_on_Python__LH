using Bunifu.UI.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Pejecoins.Program;

namespace Pejecoins
{
    public partial class Menu : Form
    {
        public Menu()
        {
            var incLen = SharedData.IncidentsByCurp.Count;
            InitializeComponent();
            radialGauge.Value = incLen;


        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void ClosePanel_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void IncLbl_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponent();
        }

        private void NotifBtn_Click(object sender, EventArgs e)
        {
            Notifications window = new Notifications();
            window.Show();
            this.Hide();
        }

        private void SignOutBtn_Click(object sender, EventArgs e)
        {
            Login window = new Login();
            window.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
