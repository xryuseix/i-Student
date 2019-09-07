using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Diagnostics;

namespace i_student
{
    public partial class useDB : UserControl
    {
        public useDB()
        {
            InitializeComponent();
            textBox1.Text = "000";
        }
        string db_file = "StudentDB.db";

        
        // データベース作成
        private void button1_Click(object sender, EventArgs e)
        {
            //ここはもう触らない！！！！！！！！！！
            //一応型とか気になった時参照できるように残しておきます。
            
                // コネクションを開いてテーブル作成して閉じる  
                using (var conn = new SQLiteConnection("Data Source=" + db_file))
                {
                    conn.Open();
                    using (SQLiteCommand command = conn.CreateCommand())
                    {
                        //command.CommandText = "create table Sample(Id INTEGER  PRIMARY KEY AUTOINCREMENT, Name TEXT, Age INTEGER)";
                        command.CommandText = " create table student(Number TEXT PRIMARY KEY, Name TEXT, Class TEXT, Gender TEXT, Correct INTEGER, Answer INTEGER, Note TEXT, Pic1 TEXT, Pic2 TEXT, Pic3 TEXT)";
                        command.ExecuteNonQuery();
                    }

                    conn.Close();
                }
              
        }
        

        // データベース接続
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "222";
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + db_file))
            {
                try
                {
                    conn.Open();
                    MessageBox.Show("Connection Success", "Connection Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Connection Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // データの追加
        //Class,ID設定なし
        private void button3_Click(object sender, EventArgs e)
        {/*
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + db_file))
            {
                conn.Open();
                using (SQLiteTransaction trans = conn.BeginTransaction())
                {
                    SQLiteCommand cmd = conn.CreateCommand();

                    // インサート
                    cmd.CommandText = "INSERT INTO student (Id, Number, Name, Gender, Correct, Answer, Note, Pic1, Pic2, Pic3) VALUES (@Id, @Number, @Name, @Gender, @Correct, @Answer, @Note, @Pic1, @Pic2, @Pic3)";

                    // パラメータセット
                    cmd.Parameters.Add("Id", System.Data.DbType.Int32);
                    cmd.Parameters.Add("Number", System.Data.DbType.String);
                    cmd.Parameters.Add("Name", System.Data.DbType.String);
                    cmd.Parameters.Add("Gender", System.Data.DbType.String);
                    cmd.Parameters.Add("Correct", System.Data.DbType.Int16);
                    cmd.Parameters.Add("Answer", System.Data.DbType.Int16);
                    cmd.Parameters.Add("Note", System.Data.DbType.String);
                    cmd.Parameters.Add("Pic1", System.Data.DbType.Binary);
                    cmd.Parameters.Add("Pic2", System.Data.DbType.Binary);
                    cmd.Parameters.Add("Pic3", System.Data.DbType.Binary);

                    // データ追加
                    cmd.Parameters["Name"].Value = "佐藤";
                    cmd.Parameters["Gender"].Value = "男";
                    cmd.Parameters["Number"].Value = "100000";
                    cmd.ExecuteNonQuery();

                    cmd.Parameters["Name"].Value = "斉藤";
                    cmd.Parameters["Gender"].Value = "女";
                    cmd.Parameters["Answer"].Value =3;
                    cmd.ExecuteNonQuery();

                    // コミット
                    trans.Commit();
                }
            }*/
        }
        
        // データの取得
        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "444";
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + db_file))
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM student";
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    string message = "Number,Name,Gender\n";

                    while (reader.Read())
                    {
                        message += reader["Number"].ToString() + "," + reader["Name"].ToString() + "," + reader["Gender"].ToString() + "\n";
                    }

                    textBox1.Text = message;
                    //MessageBox.Show(message);
                }
                conn.Close();
            }
        }
        
    }
}
