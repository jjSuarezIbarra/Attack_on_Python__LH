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
    public partial class Notifications : Form
    {
        public Notifications()
        {
            InitializeComponent();
            NotifDataGrid.Rows.Clear();
            foreach (var incident in SharedData.IncidentsByCurp)
            {
                NotifDataGrid.Rows.Add(incident.IncidentID, incident.RegistrationID, incident.Date, incident.Description);
            }
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
            Menu window = new Menu();
            window.Show();
            this.Hide();
        }

        private void NotifBtn_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            InitializeComponent();
        }

        private void SignOutBtn_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            Login window = new Login();
            window.Show();
            this.Hide();
        }

        private void NotifBtn_Click_1(object sender, EventArgs e)
        {

        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }

        private void OptionsButton_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void ClosePanel_Click_1(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
    }
}
