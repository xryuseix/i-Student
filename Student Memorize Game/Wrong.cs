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
    public partial class Wrong : UserControl
    {
        public Wrong()
        {
            InitializeComponent();
            string db_file = "StudentDB.db";
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + db_file))
            {

                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                int PersonCount = 0;
                cmd.CommandText = "SELECT * FROM student";
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PersonCount++;
                        if (PersonCount == Form1.AnsPerson)
                        {
                            label2.Text = reader["Name"].ToString() + "さん";
                            pictureBox1.ImageLocation = reader["Pic1"].ToString();
                        }

                    }
                }
                conn.Close();
            }
        }
        public static int CorrectTmp;
        public static int AnswerTmp;
        public static string NoteTmp;
        public static int NumberTmp;
        private void Wrong_Load(object sender, EventArgs e)
        {
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }
        public static void SetInfo()
        {
            string db_file = "StudentDB.db";
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + db_file))
            {

                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                int PersonCount = 0;
                cmd.CommandText = "SELECT * FROM student";
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PersonCount++;
                        if (PersonCount == Form1.AnsPerson)
                        {
                            label2.Text = reader["Name"].ToString() + "さん";
                            pictureBox1.ImageLocation = reader["Pic1"].ToString();
                            NumberTmp = int.Parse(reader["Number"].ToString());
                            AnswerTmp = int.Parse(reader["Answer"].ToString());
                            CorrectTmp = int.Parse(reader["Correct"].ToString());
                            CorrectTmp = int.Parse(reader["Correct"].ToString());
                            NoteTmp = reader["Note"].ToString();
                            richTextBox1.Text =reader["Note"].ToString();
                            
                        }

                    }
                }
                conn.Close();
                
            }
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + db_file))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "update student set Correct = @CORRECT where Number = @NUMBER";
                        cmd.Parameters.Add(new SQLiteParameter("@CORRECT", CorrectTmp));
                        cmd.Parameters.Add(new SQLiteParameter("@NUMBER", NumberTmp));
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "update student set Answer = @ANSWER where Number = @NUMBER";
                        cmd.Parameters.Add(new SQLiteParameter("@NUMBER", NumberTmp));
                        cmd.Parameters.Add(new SQLiteParameter("@ANSWER", AnswerTmp+1));
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                conn.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form1.SetQuiz(4);
            PicToName.SetText();
            SaveText();
            Form1.ptn.Visible = true;
            Form1.wng.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveText();
            Form1.menu.Visible = true;
            Form1.wng.Visible = false;
        }
        private void SaveText()
        {
            string db_file = "StudentDB.db";
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + db_file))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "update student set Note = @NOTE where Number = @NUMBER";
                        cmd.Parameters.Add(new SQLiteParameter("@NOTE", richTextBox1.Text));
                        cmd.Parameters.Add(new SQLiteParameter("@NUMBER", NumberTmp));
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                conn.Close();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}