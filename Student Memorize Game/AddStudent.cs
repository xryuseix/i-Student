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
using System.Drawing.Imaging;
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace i_student
{
    public partial class AddStudent : UserControl
    {
        public static bool BMchange=true;
        public AddStudent()
        {
            InitializeComponent();
        }
        
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var bmp = ImageFileOpen();
            pictureBox1.Image = bmp;
        }
        public static string tmpnumber = "";
        /// <summary>
        /// ファイルを開くダイアログボックスを表示して画像ファイルを開く
        /// </summary>
        /// <returns>生成したBitmapクラスオブジェクト</returns>
        private Bitmap ImageFileOpen()
        {
            //ファイルを開くダイアログボックスの作成  
            var ofd = new OpenFileDialog();
            //ファイルフィルタ  
            ofd.Filter = "Image File(*.bmp,*.jpg,*.png,*.tif)|*.bmp;*.jpg;*.png;*.tif|Bitmap(*.bmp)|*.bmp|Jpeg(*.jpg)|*.jpg|PNG(*.png)|*.png";
            //ダイアログの表示 （Cancelボタンがクリックされた場合は何もしない）
            if (ofd.ShowDialog() == DialogResult.Cancel) return null;

            return ImageFileOpen(ofd.FileName);
        }

        /// <summary>
        /// ファイルパスを指定して画像ファイルを開く
        /// </summary>
        /// <param name="fileName">画像ファイルのファイルパスを指定します。</param>
        /// <returns>生成したBitmapクラスオブジェクト</returns>
        private Bitmap ImageFileOpen(string fileName)
        {
            // 指定したファイルが存在するか？確認
            if (System.IO.File.Exists(fileName) == false) return null;

            // 拡張子の確認
            var ext = System.IO.Path.GetExtension(fileName).ToLower();

            // ファイルの拡張子が対応しているファイルかどうか調べる
            if (
                (ext != ".bmp") &&
                (ext != ".jpg") &&
                (ext != ".png") &&
                (ext != ".tif")
                )
            {
                return null;
            }

            Bitmap bmp;
            // ファイルストリームでファイルを開く
            using (var fs = new System.IO.FileStream(
                fileName,
                System.IO.FileMode.Open,
                System.IO.FileAccess.Read))
            {
                bmp = new Bitmap(fs);
            }
            BMchange = true;
            return bmp;
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            //生徒情報の削除
            string db_file = "StudentDB.db";

            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + db_file))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    using (SQLiteCommand cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = "delete from student where Number = @NUMBER";
                        cmd.Parameters.Add(new SQLiteParameter("@NUMBER", textBox2.Text));
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
            AddSetStudent();
            Form1.PeopleNum--;
            MessageBox.Show($"生徒情報の削除が完了しました", "削除完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //初期化
            textBox1.Text = "";
            textBox2.Text = "";
            richTextBox1.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            listBox2.Items.Clear();
            listBox2.Items.Add("新規登録");
            pictureBox1.Image = null;
            
            BMchange = false;
  //          var reg = new Regex(@",.*$");
            var reg = new Regex(@"[^(0-9)]+");
            string s = reg.Replace(listBox1.Text, "");
            s=reg.Replace(s, "");
            textBox2.Text=s;
            //textBox1.Text = Regex.Matches(, @"").ToString();
            if (listBox1.Text == "新規登録")
            {
                textBox1.Text = "";
                pictureBox1.ImageLocation = "";
            }
            string db_file = "StudentDB.db";
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + db_file))
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT DISTINCT Class  FROM student";
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listBox2.Items.Add(reader["Class"]);
                    }

                }
                cmd.CommandText = "SELECT * FROM student";
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (s == reader["Number"].ToString())
                        {
                            pictureBox1.ImageLocation = reader["Pic1"].ToString();
                            textBox1.Text = reader["Name"].ToString();
                            richTextBox1.Text = reader["Note"].ToString();
                            listBox2.Text = reader["Class"].ToString();
                            if (reader["Gender"].ToString() == "　男性")
                            {
                                radioButton1.Checked = true;
                            }
                            else if(reader["Gender"].ToString() == "　女性")
                            {
                                radioButton2.Checked = true;
                            }
                            tmpnumber = reader["Number"].ToString();


                            break;
                        }
                    }
                        
                }
                    
                    
                conn.Close();
            }
        }
        public static void AddSetStudent()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("新規登録");
            listBox2.Items.Clear();
            listBox2.Items.Add("新規登録");
            string db_file = "StudentDB.db";
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + db_file))
            {

                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();


                cmd.CommandText = "SELECT * FROM student";
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader["Number"].ToString() + "," + reader["Name"].ToString());
                    }
                }
                cmd.CommandText = "SELECT DISTINCT Class  FROM student";
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listBox2.Items.Add(reader["Class"]);
                    }

                }
                
                conn.Close();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string db_file = "StudentDB.db";
            int existcount= 0;
            if (listBox1.Text == "")
            {
                MessageBox.Show($"生徒を選択して下さい", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
                
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + db_file))
            {

                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                
                cmd.CommandText = "SELECT * FROM student";
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Number"].ToString() == textBox2.Text)
                        {
                            existcount++;
                        }
                    }
                }
                conn.Close();

            }
            var reg = new Regex(@"(?<=,)(.*)");
            var reg2 = new Regex(@"[0-9]+");
            //string s = reg.Replace(listBox1.Text, "");
            string s2 = reg2.Replace(textBox2.Text, "");
            if (textBox1.Text=="")
            {
                MessageBox.Show($"学生の名前を入力してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (textBox1.Text=="")
            {
                MessageBox.Show($"学生番号を入力してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (""!=s2)
            {
                richTextBox1.Text = s2;
                MessageBox.Show($"学生番号は数字で入力してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (listBox2.Text=="新規登録"&&textBox3.Text=="")
            {
                MessageBox.Show($"クラスを入力してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (listBox1.Text=="新規登録"&&existcount==0&&BMchange)
            {
                
                //string db_file = "StudentDB.db";
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + db_file))
                {
                    conn.Open();
                    using (SQLiteTransaction trans = conn.BeginTransaction())
                    {
                        SQLiteCommand cmd = conn.CreateCommand();
                        
                        if (!radioButton1.Checked&&!radioButton2.Checked)
                        {
                            radioButton1.Checked = true;
                        }
                        var RadioButtonChecked_InGroup = groupBox1.Controls.OfType<RadioButton>().SingleOrDefault(rb => rb.Checked == true);
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
                        cmd.Parameters["Number"].Value = textBox2.Text;
                        cmd.Parameters["Name"].Value = textBox1.Text;
                        if (listBox2.Text=="新規登録")
                        {
                            cmd.Parameters["Class"].Value = textBox3.Text;

                        }
                        else
                        {
                            cmd.Parameters["Class"].Value = listBox2.Text;

                        }
                        cmd.Parameters["Gender"].Value = RadioButtonChecked_InGroup.Text;
                        cmd.Parameters["Correct"].Value = 0;
                        cmd.Parameters["Answer"].Value = 0;
                        cmd.Parameters["Note"].Value = richTextBox1.Text;
                        cmd.Parameters["Pic1"].Value = "./Pictures/" + textBox2.Text + ".bmp";
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
                    conn.Close();
                }
                
                Image img;
                using (Image imgSrc = pictureBox1.Image)
                {
                    img = new Bitmap(imgSrc);
                }
                using (img)
                {
                    string BMname = "./Pictures/" + textBox2.Text + ".bmp";
                    img.Save(@BMname, ImageFormat.Bmp); //OK
                }
                
                AddSetStudent();
                Form1.PeopleNum++;
                MessageBox.Show($"生徒情報の保存が完了しました！", "保存完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (listBox1.Text == "新規登録" && existcount != 0)
            {
                MessageBox.Show($"学生番号{textBox2.Text}は既に登録されています", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (pictureBox1.ImageLocation == "")
            {
                MessageBox.Show($"写真を登録してください", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (existcount==0||textBox2.Text==tmpnumber)
            {
                
                //pictureBox1.Image.Save("./Pictures/test.bmp", ImageFormat.Bmp);
                Match matche = Regex.Match(listBox1.Text, @"\d+");
                var RadioButtonChecked_InGroup = groupBox1.Controls.OfType<RadioButton>().SingleOrDefault(rb => rb.Checked == true);
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=" + db_file))
                {
                    
                    conn.Open();

                    using (var transaction = conn.BeginTransaction())
                    {
                        using (SQLiteCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "update student set Name = @NAME where Number = @NUMBER";
                            cmd.Parameters.Add(new SQLiteParameter("@NUMBER", matche));
                            cmd.Parameters.Add(new SQLiteParameter("@NAME", textBox1.Text));
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "update student set Number = @NUMBER where Name = @NAME AND Number=@MATCH";
                            cmd.Parameters.Add(new SQLiteParameter("@NUMBER", textBox2.Text));
                            cmd.Parameters.Add(new SQLiteParameter("@NAME", textBox1.Text));
                            cmd.Parameters.Add(new SQLiteParameter("@MATCH", matche));
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "update student set Class = @CLASS where Number = @NUMBER";
                            cmd.Parameters.Add(new SQLiteParameter("@NUMBER", textBox2.Text));
                            if (listBox2.Text == "新規登録")
                            {
                                cmd.Parameters.Add(new SQLiteParameter("@CLASS", textBox3.Text));
                            }
                            else
                            {
                                cmd.Parameters.Add(new SQLiteParameter("@CLASS", listBox2.Text));
                            }

                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "update student set Gender = @GENDER where Number = @NUMBER";
                            cmd.Parameters.Add(new SQLiteParameter("@NUMBER", textBox2.Text));
                            cmd.Parameters.Add(new SQLiteParameter("@GENDER", RadioButtonChecked_InGroup.Text));
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "update student set Note = @NOTE where Number = @NUMBER";
                            cmd.Parameters.Add(new SQLiteParameter("@NUMBER", textBox2.Text));
                            cmd.Parameters.Add(new SQLiteParameter("@NOTE", richTextBox1.Text));
                            cmd.ExecuteNonQuery();

                            if (BMchange)
                            {
                                cmd.CommandText = "update student set Pic1 = @PICTURE1 where Number = @NUMBER";
                                cmd.Parameters.Add(new SQLiteParameter("@NUMBER", textBox2.Text));
                                cmd.Parameters.Add(new SQLiteParameter("@PICTURE1", "./Pictures/"+textBox2.Text+".bmp"));
                                cmd.ExecuteNonQuery();
                            }
                            
                        }

                        transaction.Commit();
                        
                    }
                    if (BMchange)
                    {
                        Image img;
                        using (Image imgSrc = pictureBox1.Image)
                        {
                            img = new Bitmap(imgSrc);
                        }
                        using (img)
                        {
                            string BMname = "./Pictures/" + textBox2.Text + ".bmp";
                            img.Save(@BMname, ImageFormat.Bmp); //OK
                        }
                    }
                    conn.Close();



                }
                AddSetStudent();
                MessageBox.Show($"生徒情報の保存が完了しました！", "保存完了", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            else
            {
                MessageBox.Show($"学生番号{textBox2.Text}は既に登録されています", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}