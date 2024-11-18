namespace Security
{
    partial class Login2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login2));
            this.lbl_UploadShare = new System.Windows.Forms.Label();
            this.pbImg2 = new System.Windows.Forms.PictureBox();
            this.PbImg1 = new System.Windows.Forms.PictureBox();
            this.pbCombined = new System.Windows.Forms.PictureBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtUploadFile = new System.Windows.Forms.TextBox();
            this.pbOrginalImage = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbImg2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbImg1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCombined)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOrginalImage)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_UploadShare
            // 
            this.lbl_UploadShare.AutoSize = true;
            this.lbl_UploadShare.Location = new System.Drawing.Point(26, 48);
            this.lbl_UploadShare.Name = "lbl_UploadShare";
            this.lbl_UploadShare.Size = new System.Drawing.Size(133, 18);
            this.lbl_UploadShare.TabIndex = 0;
            this.lbl_UploadShare.Text = "Upload Your Share";
            // 
            // pbImg2
            // 
            this.pbImg2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbImg2.Location = new System.Drawing.Point(210, 123);
            this.pbImg2.Name = "pbImg2";
            this.pbImg2.Size = new System.Drawing.Size(58, 177);
            this.pbImg2.TabIndex = 5;
            this.pbImg2.TabStop = false;
            // 
            // PbImg1
            // 
            this.PbImg1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PbImg1.Location = new System.Drawing.Point(108, 123);
            this.PbImg1.Name = "PbImg1";
            this.PbImg1.Size = new System.Drawing.Size(58, 177);
            this.PbImg1.TabIndex = 4;
            this.PbImg1.TabStop = false;
            // 
            // pbCombined
            // 
            this.pbCombined.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbCombined.Location = new System.Drawing.Point(323, 123);
            this.pbCombined.Name = "pbCombined";
            this.pbCombined.Size = new System.Drawing.Size(116, 177);
            this.pbCombined.TabIndex = 6;
            this.pbCombined.TabStop = false;
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackgroundImage = global::Security.Properties.Resources.download__5_1;
            this.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowse.Location = new System.Drawing.Point(389, 38);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(50, 40);
            this.btnBrowse.TabIndex = 8;
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtUploadFile
            // 
            this.txtUploadFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUploadFile.Location = new System.Drawing.Point(168, 48);
            this.txtUploadFile.Name = "txtUploadFile";
            this.txtUploadFile.Size = new System.Drawing.Size(215, 22);
            this.txtUploadFile.TabIndex = 7;
            // 
            // pbOrginalImage
            // 
            this.pbOrginalImage.Location = new System.Drawing.Point(581, 139);
            this.pbOrginalImage.Name = "pbOrginalImage";
            this.pbOrginalImage.Size = new System.Drawing.Size(189, 195);
            this.pbOrginalImage.TabIndex = 9;
            this.pbOrginalImage.TabStop = false;
            this.pbOrginalImage.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.BackgroundImage = global::Security.Properties.Resources.BACK;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pbImg2);
            this.panel1.Controls.Add(this.PbImg1);
            this.panel1.Controls.Add(this.btnBrowse);
            this.panel1.Controls.Add(this.pbOrginalImage);
            this.panel1.Controls.Add(this.txtUploadFile);
            this.panel1.Controls.Add(this.pbCombined);
            this.panel1.Controls.Add(this.lbl_UploadShare);
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(307, 216);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(476, 331);
            this.panel1.TabIndex = 10;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = global::Security.Properties.Resources.button_loging__3_;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Location = new System.Drawing.Point(307, 22);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(180, 32);
            this.panel3.TabIndex = 11;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Security.Properties.Resources.source;
            this.pictureBox1.Location = new System.Drawing.Point(190, 260);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(73, 135);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // Login2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Security.Properties.Resources.log22;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(795, 514);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login2";
            this.Text = "4 Level B-Secure System";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Login2_FormClosing);
            this.Load += new System.EventHandler(this.Login2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbImg2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbImg1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCombined)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbOrginalImage)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_UploadShare;
        private System.Windows.Forms.PictureBox pbImg2;
        private System.Windows.Forms.PictureBox PbImg1;
        private System.Windows.Forms.PictureBox pbCombined;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtUploadFile;
        private System.Windows.Forms.PictureBox pbOrginalImage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}