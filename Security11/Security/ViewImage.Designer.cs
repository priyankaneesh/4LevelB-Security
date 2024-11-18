namespace Security
{
    partial class ViewImage
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
            this.previous = new System.Windows.Forms.Button();
            this.lblTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pbViewImage = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbViewImage)).BeginInit();
            this.SuspendLayout();
            // 
            // previous
            // 
            this.previous.BackColor = System.Drawing.Color.Transparent;
            this.previous.BackgroundImage = global::Security.Properties.Resources.download__1_;
            this.previous.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.previous.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.previous.ForeColor = System.Drawing.Color.White;
            this.previous.Location = new System.Drawing.Point(683, 533);
            this.previous.Name = "previous";
            this.previous.Size = new System.Drawing.Size(91, 45);
            this.previous.TabIndex = 7;
            this.previous.Text = "Previous";
            this.previous.UseVisualStyleBackColor = false;
            this.previous.Click += new System.EventHandler(this.previous_Click);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.Red;
            this.lblTime.Location = new System.Drawing.Point(177, 548);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(85, 16);
            this.lblTime.TabIndex = 6;
            this.lblTime.Text = "Login Time";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(295, 548);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 18);
            this.label1.TabIndex = 5;
            this.label1.Text = "Time";
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Security.Properties.Resources.button_previous_loggers_list__6_;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(308, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(255, 34);
            this.panel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::Security.Properties.Resources.images__7_;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.pbViewImage);
            this.panel2.Location = new System.Drawing.Point(422, 74);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(352, 393);
            this.panel2.TabIndex = 9;
            // 
            // pbViewImage
            // 
            this.pbViewImage.Image = global::Security.Properties.Resources.images__8_;
            this.pbViewImage.Location = new System.Drawing.Point(26, 18);
            this.pbViewImage.Name = "pbViewImage";
            this.pbViewImage.Size = new System.Drawing.Size(299, 354);
            this.pbViewImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbViewImage.TabIndex = 2;
            this.pbViewImage.TabStop = false;
            // 
            // ViewImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Security.Properties.Resources.Capture12;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(795, 624);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.previous);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Name = "ViewImage";
            this.Text = "4 Level B-Secure System";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewImage_FormClosing);
            this.Load += new System.EventHandler(this.ViewImage_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbViewImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button previous;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pbViewImage;
    }
}