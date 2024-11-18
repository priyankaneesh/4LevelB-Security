namespace Security
{
    partial class Login1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login1));
            this.pbPswd1 = new System.Windows.Forms.PictureBox();
            this.ttPosition = new System.Windows.Forms.ToolTip(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbPswd1)).BeginInit();
            this.SuspendLayout();
            // 
            // pbPswd1
            // 
            this.pbPswd1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbPswd1.Image = global::Security.Properties.Resources.aaa;
            this.pbPswd1.Location = new System.Drawing.Point(278, 151);
            this.pbPswd1.Name = "pbPswd1";
            this.pbPswd1.Size = new System.Drawing.Size(505, 276);
            this.pbPswd1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPswd1.TabIndex = 2;
            this.pbPswd1.TabStop = false;
            this.pbPswd1.Click += new System.EventHandler(this.pbPswd1_Click);
            this.pbPswd1.DoubleClick += new System.EventHandler(this.pbPswd1_DoubleClick);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = global::Security.Properties.Resources.button_loging__3_;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Location = new System.Drawing.Point(436, 68);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(245, 32);
            this.panel3.TabIndex = 6;
            // 
            // Login1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Security.Properties.Resources.laptop_notebook_coffee_mobile_phone_top_view_old_wood_plank_background_have_space_798032231;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(795, 514);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pbPswd1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "4 Level B-Secure System";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Login1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pbPswd1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbPswd1;
        private System.Windows.Forms.ToolTip ttPosition;
        private System.Windows.Forms.Panel panel3;
    }
}