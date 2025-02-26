using System;
using System.Drawing;
using System.Windows.Forms;

namespace ScreenshotApp
{
    public partial class AreaSelectionForm : Form
    {
        private Point startPoint;
        private Rectangle selectedRectangle;
        private bool isDragging = false;
        private BufferedGraphicsContext bgc;
        private BufferedGraphics buffer;

        public Rectangle SelectedRectangle => selectedRectangle;

        public AreaSelectionForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;
            this.BackColor = Color.Black;
            this.Opacity = 0.3;
            this.FormBorderStyle = FormBorderStyle.None;

            bgc = BufferedGraphicsManager.Current;
            buffer = bgc.Allocate(this.CreateGraphics(), this.ClientRectangle);
        }

        //private void AreaSelectionForm_Load(object sender, EventArgs e)
        //{
        //    // You can add any setup code here if needed
        //}

        protected override void OnMouseDown(MouseEventArgs e)
        {
            isDragging = true;
            startPoint = e.Location;
            selectedRectangle = new Rectangle(startPoint.X, startPoint.Y, 0, 0);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (isDragging)
            {
                int x = Math.Min(startPoint.X, e.X);
                int y = Math.Min(startPoint.Y, e.Y);
                int width = Math.Abs(e.X - startPoint.X);
                int height = Math.Abs(e.Y - startPoint.Y);

                selectedRectangle = new Rectangle(x, y, width, height);
                this.Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isDragging = false;
            if (selectedRectangle.Width > 5 && selectedRectangle.Height > 5)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
            this.Close();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            buffer.Graphics.Clear(Color.FromArgb(80, 0, 0, 0)); // Semi-transparent dark overlay
            using (Pen pen = new Pen(Color.Red, 3))
            {
                buffer.Graphics.DrawRectangle(pen, selectedRectangle);
            }
            buffer.Render(e.Graphics);
        }
    }
}
