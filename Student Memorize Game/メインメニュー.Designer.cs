namespace i_student
{
    partial class Mainmenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(75, 84);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(187, 254);
            this.button1.TabIndex = 0;
            this.button1.Text = "写真から名前を当てる\r\n(全生徒からランダム)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button_Pic_Name_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(303, 84);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(187, 254);
            this.button2.TabIndex = 1;
            this.button2.Text = "写真から名前を当てる\r\n(全生徒からランダム)";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button_Name_Pic_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(530, 84);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(187, 118);
            this.button3.TabIndex = 2;
            this.button3.Text = "条件を付けて遊ぶ";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Button_Conditions_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(530, 220);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(187, 118);
            this.button4.TabIndex = 3;
            this.button4.Text = "成績紹介";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.Button_Grade_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(355, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "メインメニュー";
            // 
            // Mainmenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Mainmenu";
            this.Size = new System.Drawing.Size(800, 450);
            this.Load += new System.EventHandler(this.Mainmenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
    }
}