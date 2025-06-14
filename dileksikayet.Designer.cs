namespace Günessistemioyun
{
    partial class dileksikayet
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
            this.labelAd = new System.Windows.Forms.Label();
            this.textBoxAd = new System.Windows.Forms.TextBox();
            this.labelMesaj = new System.Windows.Forms.Label();
            this.textBoxMesaj = new System.Windows.Forms.TextBox();
            this.buttonGonder = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelAd
            // 
            this.labelAd.AutoSize = true;
            this.labelAd.Location = new System.Drawing.Point(3, 0);
            this.labelAd.Name = "labelAd";
            this.labelAd.Size = new System.Drawing.Size(27, 16);
            this.labelAd.TabIndex = 0;
            this.labelAd.Text = "Ad:";
            // 
            // textBoxAd
            // 
            this.textBoxAd.Location = new System.Drawing.Point(3, 22);
            this.textBoxAd.Name = "textBoxAd";
            this.textBoxAd.ReadOnly = true;
            this.textBoxAd.Size = new System.Drawing.Size(794, 22);
            this.textBoxAd.TabIndex = 1;
            // 
            // labelMesaj
            // 
            this.labelMesaj.AutoSize = true;
            this.labelMesaj.Location = new System.Drawing.Point(3, 44);
            this.labelMesaj.Name = "labelMesaj";
            this.labelMesaj.Size = new System.Drawing.Size(47, 16);
            this.labelMesaj.TabIndex = 2;
            this.labelMesaj.Text = "Mesaj:";
            // 
            // textBoxMesaj
            // 
            this.textBoxMesaj.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMesaj.Location = new System.Drawing.Point(3, 66);
            this.textBoxMesaj.Multiline = true;
            this.textBoxMesaj.Name = "textBoxMesaj";
            this.textBoxMesaj.Size = new System.Drawing.Size(794, 345);
            this.textBoxMesaj.TabIndex = 3;
            // 
            // buttonGonder
            // 
            this.buttonGonder.Location = new System.Drawing.Point(3, 417);
            this.buttonGonder.Name = "buttonGonder";
            this.buttonGonder.Size = new System.Drawing.Size(794, 30);
            this.buttonGonder.TabIndex = 4;
            this.buttonGonder.Text = "Gönder";
            this.buttonGonder.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labelAd, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBoxAd, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelMesaj, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.textBoxMesaj, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.buttonGonder, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dileksikayet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "dileksikayet";
            this.Text = "Dilek ve Şikayet";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelAd;
        private System.Windows.Forms.TextBox textBoxAd;
        private System.Windows.Forms.Label labelMesaj;
        private System.Windows.Forms.TextBox textBoxMesaj;
        private System.Windows.Forms.Button buttonGonder;
    }
}