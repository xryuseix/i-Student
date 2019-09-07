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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;

namespace i_student
{
    
   public partial class Grade : UserControl
    {
        public Grade()
        {
            InitializeComponent();
            dataGridView1.Columns["Column1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["Column3"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["Column6"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void Button_AddStudent_Click(object sender, EventArgs e)
        {
            Form1.grd.Visible = false;
            Form1.add.Visible = true;
            AddStudent.AddSetStudent();
        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //textBox1.Text = csv.lists[0][0];
        }
        /*
        private void button2_Click(object sender, EventArgs e)
        {
            Form1.grd.Visible = false;
            Form1.Cv.Visible = true;
        }*/

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
        public static void SetStudent()
        {
            dataGridView1.Rows.Clear();
            string db_file = "StudentDB.db";
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + db_file))
            {
                
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                
                
                cmd.CommandText = "SELECT * FROM student";
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    //string message = "Number,Name,Gender\n";
                    int i=0;
                    while (reader.Read())
                    {
                        //message += reader["Number"].ToString() + "," + reader["Name"].ToString() + "," + reader["Gender"].ToString() + "\n";
                        i++;
                        if (reader["Name"].ToString() != "")
                        {
                            dataGridView1.Rows.Add(reader["Number"].ToString(), reader["Class"].ToString(), reader["Name"].ToString(), reader["Gender"].ToString(), reader["Correct"].ToString() + "/" + reader["Answer"].ToString(), reader["Note"].ToString());
                        }
                           
                    }
                    Form1.StudentNum = i;
                }
                conn.Close();
                
            }
        }
        public void DaleteStudent()
        {
            string db_file = "StudentDB.db";
            
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + db_file))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    using (SQLiteCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "delete from student where Number > @NUM";
                        cmd.Parameters.Add(new SQLiteParameter("@NUM", 1));
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }

        }
        public static void AddStudentInfo()
        {
            string db_file = "StudentDB.db";
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + db_file))
            {
                conn.Open();
                using (SQLiteTransaction trans = conn.BeginTransaction())
                {
                    SQLiteCommand cmd = conn.CreateCommand();

                    // インサート
                    cmd.CommandText = "INSERT INTO student (Number, Name, Class, Gender, Correct, Answer, Note, Pic1, Pic2, Pic3) VALUES (@Number, @Name, @Class, @Gender, @Correct, @Answer, @Note, @Pic1, @Pic2, @Pic3)";

                    // パラメータセット
                    cmd.Parameters.Add("Number", System.Data.DbType.String);
                    cmd.Parameters.Add("Name", System.Data.DbType.String);
                    cmd.Parameters.Add("Class", System.Data.DbType.String);
                    cmd.Parameters.Add("Gender", System.Data.DbType.String);
                    cmd.Parameters.Add("Correct", System.Data.DbType.Int16);
                    cmd.Parameters.Add("Answer", System.Data.DbType.Int16);
                    cmd.Parameters.Add("Note", System.Data.DbType.String);
                    cmd.Parameters.Add("Pic1", System.Data.DbType.String);
                    cmd.Parameters.Add("Pic2", System.Data.DbType.String);
                    cmd.Parameters.Add("Pic3", System.Data.DbType.String);

                    // データ追加
                    cmd.Parameters["Number"].Value = "20000825";
                    cmd.Parameters["Name"].Value = "石川バージョンⅡ";
                    cmd.Parameters["Class"].Value = "1";
                    cmd.Parameters["Gender"].Value = "男性";
                    cmd.Parameters["Correct"].Value = 3;
                    cmd.Parameters["Answer"].Value = 5;
                    cmd.Parameters["Note"].Value = "RiPProの人";
                    cmd.Parameters["Pic1"].Value = "./Pictures/"+ "abe" + ".jpg";
                    cmd.Parameters["Pic2"].Value = "";
                    cmd.Parameters["Pic3"].Value = "";
                    cmd.ExecuteNonQuery();
                    /*
                    cmd.Parameters["Name"].Value = "斉藤";
                    cmd.Parameters["Gender"].Value = "女";
                    cmd.Parameters["Answer"].Value = 3;
                    cmd.ExecuteNonQuery();
                    */
                    // コミット
                    trans.Commit();
                }
            }
        }/*
        private void button2_Click_1(object sender, EventArgs e)
        {
            //DaleteStudent();
            //AddStudentInfo();
            
            //SetStudent();
            //AddStudent.AddSetStudent();
            Form1.grd.Visible = false;
            Form1.db.Visible = true;
        }
        */
    }
}