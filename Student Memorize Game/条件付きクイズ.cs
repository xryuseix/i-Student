using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using i_student;

namespace i_student
{
    public partial class Conditions : UserControl
    {
        public Conditions()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1.con.Visible = false;
            Form1.ptn.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1.con.Visible = false;
            Form1.ptn.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1.con.Visible = false;
            Form1.ntp.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1.con.Visible = false;
            Form1.ntp.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1.con.Visible = false;
            Form1.ptn.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form1.con.Visible = false;
            Form1.ntp.Visible = true;
        }
    }
}