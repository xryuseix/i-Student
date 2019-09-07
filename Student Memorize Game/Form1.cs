using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using i_student;
using System.Data.SQLite;
using System.Security.AccessControl;
using System.IO;
using System.Security.Principal;



namespace i_student
{

    public partial class Form1 : Form
    {
        public static int mode = 0;
        //0:写真から名前を当てる（ランダム）
        //1:名前から写真を当てる（ランダム）
        //2:写真から名前を当てる（男）
        //3:写真から名前を当てる（女）
        //4:名前から写真を当てる（男）
        //5:名前から写真を当てる（女）
        //6:写真から名前を当てる（学生番号）
        //7:名前から写真を当てる（学生番号）
        public static long first = 0;//mode=6,7の場合
        public static long last = 100;//mode=6,7の場合
        public static int ans = 0;//ユーザの解答番号
        public static int AnsPerson = 0;//問題の正解となる人
        public static int[] QuizPerson = new int[10];//問題の選択肢となる人
        public static int StudentNum = 0;
        public static int PeopleNum=0;


        //staticで宣言することでインスタンスを固定
        public static UserControl1 ctr1;
        public static UserControl2 ctr2;
        public static Mainmenu menu;
        public static Conditions con;
        public static PicToName ptn;
        public static NameToPic ntp;
        public static Grade grd;
        public static AddStudent add;
        public static Correct cct;
        public static Wrong wng;
        public static useDB db;

        public Form1()
        {
            InitializeComponent();
            ctr1 = new UserControl1();
            ctr2 = new UserControl2();
            menu = new Mainmenu();
            con = new Conditions();
            ptn = new PicToName();
            ntp = new NameToPic();
            grd = new Grade();
            add = new AddStudent();
            cct = new Correct();
            wng = new Wrong();
            db = new useDB();

            //パネルにコントロールを追加
            panel1.Controls.Add(ctr1);
            panel1.Controls.Add(ctr2);
            panel1.Controls.Add(menu);
            panel1.Controls.Add(con);
            panel1.Controls.Add(ptn);
            panel1.Controls.Add(ntp);
            panel1.Controls.Add(grd);
            panel1.Controls.Add(add);
            panel1.Controls.Add(cct);
            panel1.Controls.Add(wng);
            panel1.Controls.Add(db);

            //メインメニューのみを見えるようにする
            ctr1.Visible = false;
            ctr2.Visible = false;
            menu.Visible = true;
            con.Visible = false;
            ptn.Visible = false;
            ntp.Visible = false;
            grd.Visible = false;
            add.Visible = false;
            cct.Visible = false;
            wng.Visible = false;
            db.Visible = false;

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
                        PeopleNum++;
                    }
                }
                conn.Close();
            }

            string file = "./StudentDB.db";
            string user = Environment.UserDomainName + "\\" + Environment.UserName;
            FileSystemAccessRule fsaRule = new FileSystemAccessRule(
            user, FileSystemRights.FullControl, AccessControlType.Allow);
            bool hasMyAccessRight = false;
            
            //既存のアクセス権を調べる
            FileSecurity security = File.GetAccessControl(file);
            foreach (FileSystemAccessRule rule in
                    security.GetAccessRules(true, true, typeof(NTAccount)))
            {
                if (((NTAccount)rule.IdentityReference).Value == user)
                {
                    //すでにユーザーに関する権限があればループを抜ける。
                    hasMyAccessRight = true;
                    break;
                }
            }
            //アクセス権の設定
            if (hasMyAccessRight)
            {
                security.SetAccessRule(fsaRule);
            }
            else
            {
                security.AddAccessRule(fsaRule);
            }
            //変更したアクセス権をファイルに設定
            File.SetAccessControl(file, security);
        }
        
        
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void Button_toMainmenu_Click(object sender, EventArgs e)
        {
            Form1.ctr1.Visible = false;
            Form1.ctr2.Visible = false;
            Form1.menu.Visible = true;
            Form1.con.Visible = false;
            Form1.ptn.Visible = false;
            Form1.ntp.Visible = false;
            Form1.grd.Visible = false;
            Form1.add.Visible = false;
            Form1.cct.Visible = false;
            Form1.wng.Visible = false;
        }
        static void Swap(ref int m, ref int n)
        {
            int work = m;
            m = n;
            n = work;
        }
        static IEnumerable<int> GetUniqRandomNumbers(int rangeBegin, int rangeEnd, int count)
        {
            // 指定された範囲の整数を格納できる配列を用意する
            int[] work = new int[rangeEnd - rangeBegin + 1];

            // 配列を初期化する
            for (int n = rangeBegin, i = 0; n <= rangeEnd; n++, i++)
                work[i] = n;

            // ランダムに取り出しては先頭から順に置いていく（count回繰り返す）
            var rnd = new Random();
            for (int resultPos = 0; resultPos < count; resultPos++)
            {
                // （resultPosを含めて）resultPosの後ろからランダムに1つ選ぶ
                int nextResultPos = rnd.Next(resultPos, work.Length);

                // nextResultPosの値をresultPosと入れ替える
                Swap(ref work[resultPos], ref work[nextResultPos]);
            }

            return work.Take(count); // workの先頭からcount個を返す
        }
        public static void SetQuiz(int num)
        {
            int StudentNum = num;
            int[] menber = new int[StudentNum];
            int seed = Environment.TickCount;
            menber=GetUniqRandomNumbers(1, Form1.PeopleNum, StudentNum).ToArray();

            var array2 = menber.OrderBy(i => Guid.NewGuid()).ToArray();
            
            for (int i = 0; i < StudentNum; i++)
            {
                Form1.QuizPerson[i] = menber[i];
            }

        }
        
    }
    
}