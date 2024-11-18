namespace Security
{
    partial class CaptureImage
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
            this.pbCaptureImage = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbCaptureImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pbCaptureImage
            // 
            this.pbCaptureImage.Location = new System.Drawing.Point(0, 0);
            this.pbCaptureImage.Name = "pbCaptureImage";
            this.pbCaptureImage.Size = new System.Drawing.Size(292, 246);
            this.pbCaptureImage.TabIndex = 0;
            this.pbCaptureImage.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CaptureImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 245);
            this.Controls.Add(this.pbCaptureImage);
            this.Name = "CaptureImage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "4 Level B-Secure System";
            this.Load += new System.EventHandler(this.CaptureImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbCaptureImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCaptureImage;
        private System.Windows.Forms.Timer timer1;
    }
}