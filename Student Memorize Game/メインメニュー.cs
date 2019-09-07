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
    public partial class Mainmenu : UserControl
    {
        public Mainmenu()
        {
            InitializeComponent();
        }

        private void Button_Pic_Name_Click(object sender, EventArgs e)
        {
            if (Form1.PeopleNum < 4)
            {
                MessageBox.Show($"学生は４人以上登録してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Form1.menu.Visible = false;
                Form1.ptn.Visible = true;
                Form1.mode = 0;
                Form1.SetQuiz(4);
                PicToName.SetText();
            }
            
        }
        private void Button_Name_Pic_Click(object sender, EventArgs e)
        {/*
            Form1.menu.Visible = false;
            Form1.ntp.Visible = true;
            Form1.mode = 1;*/
        }
        private void Button_Conditions_Click(object sender, EventArgs e)
        {
            Form1.menu.Visible = false;
            Form1.con.Visible = true;
        }
        private void Button_Grade_Click(object sender, EventArgs e)
        {
            Grade.SetStudent();
            Form1.menu.Visible = false;
            Form1.grd.Visible = true;
        }
        private void Mainmenu_Load(object sender, EventArgs e)
        {
        }
    }
}