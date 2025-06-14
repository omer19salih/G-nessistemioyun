namespace Günessistemioyun
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.sifreayarlaPanel = new System.Windows.Forms.Panel();
            this.chkShowPassword = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.dogrulamaPanel = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.txtVerificationCode = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pbAvatar = new System.Windows.Forms.PictureBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.button6 = new System.Windows.Forms.Button();
            this.sifreayarlaPanel.SuspendLayout();
            this.dogrulamaPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtUsername.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtUsername.Location = new System.Drawing.Point(295, 105);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(144, 27);
            this.txtUsername.TabIndex = 0;
            this.txtUsername.Text = "Kullanıcı_İsmi";
            this.txtUsername.Enter += new System.EventHandler(this.textBox1_Enter);
            this.txtUsername.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtNewPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtNewPassword.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtNewPassword.Location = new System.Drawing.Point(14, 21);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(144, 27);
            this.txtNewPassword.TabIndex = 1;
            this.txtNewPassword.Text = "Şifre";
            this.txtNewPassword.Enter += new System.EventHandler(this.textBox2_Enter);
            this.txtNewPassword.Leave += new System.EventHandler(this.textBox2_Leave);
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtConfirmPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConfirmPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtConfirmPassword.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtConfirmPassword.Location = new System.Drawing.Point(14, 66);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new System.Drawing.Size(144, 27);
            this.txtConfirmPassword.TabIndex = 2;
            this.txtConfirmPassword.Text = "Şifre Tekrar";
            this.txtConfirmPassword.Enter += new System.EventHandler(this.textBox3_Enter);
            this.txtConfirmPassword.Leave += new System.EventHandler(this.textBox3_Leave);
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtEmail.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtEmail.Location = new System.Drawing.Point(295, 161);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(144, 27);
            this.txtEmail.TabIndex = 3;
            this.txtEmail.Text = "E -mail";
            this.txtEmail.Enter += new System.EventHandler(this.textBox4_Enter);
            this.txtEmail.Leave += new System.EventHandler(this.textBox4_Leave);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button1.Location = new System.Drawing.Point(295, 306);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(98, 51);
            this.button1.TabIndex = 4;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnRegister_Click);
            // 
            // txtPhone
            // 
            this.txtPhone.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtPhone.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtPhone.Location = new System.Drawing.Point(295, 189);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(144, 27);
            this.txtPhone.TabIndex = 5;
            this.txtPhone.Text = "Numara";
            this.txtPhone.Enter += new System.EventHandler(this.textBox5_Enter);
            this.txtPhone.Leave += new System.EventHandler(this.textBox5_Leave);
            // 
            // sifreayarlaPanel
            // 
            this.sifreayarlaPanel.BackColor = System.Drawing.SystemColors.HotTrack;
            this.sifreayarlaPanel.Controls.Add(this.chkShowPassword);
            this.sifreayarlaPanel.Controls.Add(this.button3);
            this.sifreayarlaPanel.Controls.Add(this.txtNewPassword);
            this.sifreayarlaPanel.Controls.Add(this.txtConfirmPassword);
            this.sifreayarlaPanel.Location = new System.Drawing.Point(34, 306);
            this.sifreayarlaPanel.Name = "sifreayarlaPanel";
            this.sifreayarlaPanel.Size = new System.Drawing.Size(302, 182);
            this.sifreayarlaPanel.TabIndex = 7;
            this.sifreayarlaPanel.Visible = false;
            this.sifreayarlaPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.sifreayarlaPanel_Paint);
            // 
            // chkShowPassword
            // 
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.chkShowPassword.Location = new System.Drawing.Point(195, 47);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.Size = new System.Drawing.Size(18, 17);
            this.chkShowPassword.TabIndex = 4;
            this.chkShowPassword.UseVisualStyleBackColor = true;
            this.chkShowPassword.CheckedChanged += new System.EventHandler(this.chkShowPassword_CheckedChanged);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button3.Location = new System.Drawing.Point(85, 103);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(118, 66);
            this.button3.TabIndex = 3;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.btnSetPassword_Click);
            // 
            // txtFullName
            // 
            this.txtFullName.BackColor = System.Drawing.SystemColors.InfoText;
            this.txtFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtFullName.ForeColor = System.Drawing.SystemColors.MenuBar;
            this.txtFullName.Location = new System.Drawing.Point(295, 133);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(144, 27);
            this.txtFullName.TabIndex = 8;
            this.txtFullName.Text = "Ad_Soyad";
            this.txtFullName.Enter += new System.EventHandler(this.textBox6_Enter);
            this.txtFullName.Leave += new System.EventHandler(this.textBox6_Leave);
            // 
            // dogrulamaPanel
            // 
            this.dogrulamaPanel.BackColor = System.Drawing.Color.Brown;
            this.dogrulamaPanel.Controls.Add(this.button2);
            this.dogrulamaPanel.Controls.Add(this.txtVerificationCode);
            this.dogrulamaPanel.Location = new System.Drawing.Point(373, 385);
            this.dogrulamaPanel.Name = "dogrulamaPanel";
            this.dogrulamaPanel.Size = new System.Drawing.Size(576, 250);
            this.dogrulamaPanel.TabIndex = 9;
            this.dogrulamaPanel.Visible = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Location = new System.Drawing.Point(143, 131);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(211, 81);
            this.button2.TabIndex = 1;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.btnVerifyCode_Click);
            // 
            // txtVerificationCode
            // 
            this.txtVerificationCode.BackColor = System.Drawing.SystemColors.InfoText;
            this.txtVerificationCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtVerificationCode.ForeColor = System.Drawing.SystemColors.MenuBar;
            this.txtVerificationCode.Location = new System.Drawing.Point(66, 62);
            this.txtVerificationCode.Name = "txtVerificationCode";
            this.txtVerificationCode.Size = new System.Drawing.Size(419, 49);
            this.txtVerificationCode.TabIndex = 0;
            this.txtVerificationCode.Text = "Doğrulama_kodunu_gir ";
            this.txtVerificationCode.Enter += new System.EventHandler(this.textBox7_Enter);
            this.txtVerificationCode.Leave += new System.EventHandler(this.textBox7_Leave);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pbAvatar
            // 
            this.pbAvatar.Location = new System.Drawing.Point(229, 217);
            this.pbAvatar.Name = "pbAvatar";
            this.pbAvatar.Size = new System.Drawing.Size(75, 73);
            this.pbAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbAvatar.TabIndex = 10;
            this.pbAvatar.TabStop = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button4.BackgroundImage")));
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button4.Location = new System.Drawing.Point(310, 217);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(121, 73);
            this.button4.TabIndex = 11;
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.btnSelectAvatar_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Transparent;
            this.button5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button5.BackgroundImage")));
            this.button5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button5.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.button5.Location = new System.Drawing.Point(453, 239);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 51);
            this.button5.TabIndex = 12;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(621, 25);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // button6
            // 
            this.button6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button6.BackgroundImage")));
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button6.Location = new System.Drawing.Point(563, 0);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(58, 32);
            this.button6.TabIndex = 14;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(621, 438);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.dogrulamaPanel);
            this.Controls.Add(this.sifreayarlaPanel);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.pbAvatar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.sifreayarlaPanel.ResumeLayout(false);
            this.sifreayarlaPanel.PerformLayout();
            this.dogrulamaPanel.ResumeLayout(false);
            this.dogrulamaPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Panel sifreayarlaPanel;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Panel dogrulamaPanel;
        private System.Windows.Forms.TextBox txtVerificationCode;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pbAvatar;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button button6;
    }
}