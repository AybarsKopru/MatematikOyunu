namespace MatematikOyunu
{
    partial class AnaMenu
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnYeniOyun = new System.Windows.Forms.Button();
            this.btnDevam = new System.Windows.Forms.Button();
            this.lblSkorTablosu = new System.Windows.Forms.Label();
            this.lblSkorBirinci = new System.Windows.Forms.Label();
            this.lblSkorIkinci = new System.Windows.Forms.Label();
            this.lblSkorUcuncu = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnYeniOyun
            // 
            this.btnYeniOyun.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnYeniOyun.Location = new System.Drawing.Point(51, 66);
            this.btnYeniOyun.Name = "btnYeniOyun";
            this.btnYeniOyun.Size = new System.Drawing.Size(233, 112);
            this.btnYeniOyun.TabIndex = 0;
            this.btnYeniOyun.Text = "YENİ OYUNA BAŞLA";
            this.btnYeniOyun.UseVisualStyleBackColor = true;
            this.btnYeniOyun.Click += new System.EventHandler(this.btnYeniOyun_Click);
            // 
            // btnDevam
            // 
            this.btnDevam.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnDevam.Location = new System.Drawing.Point(51, 243);
            this.btnDevam.Name = "btnDevam";
            this.btnDevam.Size = new System.Drawing.Size(233, 112);
            this.btnDevam.TabIndex = 1;
            this.btnDevam.Text = "DEVAM ET";
            this.btnDevam.UseVisualStyleBackColor = true;
            this.btnDevam.Click += new System.EventHandler(this.btnDevam_Click);
            // 
            // lblSkorTablosu
            // 
            this.lblSkorTablosu.AutoSize = true;
            this.lblSkorTablosu.Location = new System.Drawing.Point(573, 66);
            this.lblSkorTablosu.Name = "lblSkorTablosu";
            this.lblSkorTablosu.Size = new System.Drawing.Size(110, 16);
            this.lblSkorTablosu.TabIndex = 2;
            this.lblSkorTablosu.Text = "SKOR TABLOSU";
            this.lblSkorTablosu.Click += new System.EventHandler(this.SkorDetaylariGoster);
            // 
            // lblSkorBirinci
            // 
            this.lblSkorBirinci.AutoSize = true;
            this.lblSkorBirinci.Location = new System.Drawing.Point(543, 95);
            this.lblSkorBirinci.Name = "lblSkorBirinci";
            this.lblSkorBirinci.Size = new System.Drawing.Size(17, 16);
            this.lblSkorBirinci.TabIndex = 3;
            this.lblSkorBirinci.Text = "1.";
            // 
            // lblSkorIkinci
            // 
            this.lblSkorIkinci.AutoSize = true;
            this.lblSkorIkinci.Location = new System.Drawing.Point(543, 162);
            this.lblSkorIkinci.Name = "lblSkorIkinci";
            this.lblSkorIkinci.Size = new System.Drawing.Size(17, 16);
            this.lblSkorIkinci.TabIndex = 4;
            this.lblSkorIkinci.Text = "2.";
            // 
            // lblSkorUcuncu
            // 
            this.lblSkorUcuncu.AutoSize = true;
            this.lblSkorUcuncu.Location = new System.Drawing.Point(543, 233);
            this.lblSkorUcuncu.Name = "lblSkorUcuncu";
            this.lblSkorUcuncu.Size = new System.Drawing.Size(17, 16);
            this.lblSkorUcuncu.TabIndex = 5;
            this.lblSkorUcuncu.Text = "3.";
            // 
            // AnaMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblSkorUcuncu);
            this.Controls.Add(this.lblSkorIkinci);
            this.Controls.Add(this.lblSkorBirinci);
            this.Controls.Add(this.lblSkorTablosu);
            this.Controls.Add(this.btnDevam);
            this.Controls.Add(this.btnYeniOyun);
            this.Name = "AnaMenu";
            this.Text = "Matematik Oyunu - Ana Menü";
            this.Load += new System.EventHandler(this.AnaMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnYeniOyun;
        private System.Windows.Forms.Button btnDevam;
        private System.Windows.Forms.Label lblSkorTablosu;
        private System.Windows.Forms.Label lblSkorBirinci;
        private System.Windows.Forms.Label lblSkorIkinci;
        private System.Windows.Forms.Label lblSkorUcuncu;
    }
}

