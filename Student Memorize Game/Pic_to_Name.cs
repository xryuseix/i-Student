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
using System.Data.SQLite;

namespace i_student
{
    public partial class PicToName: UserControl
    {
        

        public PicToName()
        {
            InitializeComponent();
            
            
        }
        public static string AnsPersonName;
        public static  void SetText()
        {
            int seed = Environment.TickCount;
            Random rnd = new System.Random();
            /*
            radioButton1.Text = @Csv.lists[Form1.QuizPerson[0],1];
            radioButton2.Text = @Csv.lists[Form1.QuizPerson[1],1];
            radioButton3.Text = @Csv.lists[Form1.QuizPerson[2],1];
            radioButton4.Text = @Csv.lists[Form1.QuizPerson[3],1];*/
            string db_file = "StudentDB.db";
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + db_file))
            {

                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                Form1.AnsPerson=Form1.QuizPerson[rnd.Next(4)];
                int PersonCount = 0;
                cmd.CommandText = "SELECT * FROM student";
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PersonCount++;
                        if (PersonCount== Form1.AnsPerson)
                        {
                            AnsPersonName = reader["Name"].ToString();
                            pictureBox1.ImageLocation = reader["Pic1"].ToString();
                        }
                        if(PersonCount== Form1.QuizPerson[0])
                        {
                            radioButton1.Text = reader["Name"].ToString();
                        }
                        else if (PersonCount == Form1.QuizPerson[1])
                        {
                            radioButton2.Text = reader["Name"].ToString();
                        }
                        else if (PersonCount == Form1.QuizPerson[2])
                        {
                            radioButton3.Text = reader["Name"].ToString();
                        }
                        else if (PersonCount == Form1.QuizPerson[3])
                        {
                            radioButton4.Text = reader["Name"].ToString();
                        }
                    }
                }
                conn.Close();
            }
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            var RadioButtonChecked_InGroup = groupBox1.Controls.OfType<RadioButton>().SingleOrDefault(rb => rb.Checked == true);

            // 結果
            if (RadioButtonChecked_InGroup == null)
            {
                MessageBox.Show("どのボタンもチェックされていません", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (RadioButtonChecked_InGroup.Text == AnsPersonName)
                {
                    Correct.SetInfo();
                    Form1.ptn.Visible = false;
                    Form1.cct.Visible = true;
                }
                else
                {
                    Wrong.SetInfo();
                    Form1.ptn.Visible = false;
                    Form1.wng.Visible = true;
                }
            }



            
            
        }

        
    }
}