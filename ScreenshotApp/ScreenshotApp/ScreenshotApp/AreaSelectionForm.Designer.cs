namespace ScreenshotApp
{
    partial class AreaSelectionForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AreaSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AreaSelectionForm";
            this.Opacity = 0.3D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Select Area";
            this.TopMost = true;
            //this.Load += new System.EventHandler(this.AreaSelectionForm_Load);
            this.ResumeLayout(false);

        }
    }
}
