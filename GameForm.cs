using System;
using System.Drawing;
using System.Windows.Forms;

namespace BushidoBurrito.Planarity
{
    public class GameForm : Form
    {
        private readonly BufferedGraphicsContext _context;
        private BufferedGraphics _bufferedGraphics;

        public GameForm() : base()
        {
            this.Text = "Planarity";
            this.MouseDown += this.MouseDownHandler;
            this.Resize += this.OnResize;
            this.SetStyle( ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true );

            _context = BufferedGraphicsManager.Current;

            ResizeBuffer();
        }

        private void MouseDownHandler(object sender, MouseEventArgs e)
        {
            if( e.Button == MouseButtons.Left )
            {
                Console.WriteLine($"clicked ({e.X}, {e.Y})");
            }
        }

        private void OnResize(object sender, EventArgs e)
        {
            ResizeBuffer();
            Clear();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _bufferedGraphics.Render(e.Graphics);
        }

        private void ResizeBuffer()
        {
            _context.MaximumBuffer = new Size(this.Width+1, this.Height+1);

            if( _bufferedGraphics != null )
            {
                _bufferedGraphics.Dispose();
            }

            _bufferedGraphics = _context.Allocate(
                this.CreateGraphics(), 
                new Rectangle( 0, 0, this.Width, this.Height ));
        }

        private void Clear()
        {
            ClearBuffer(_bufferedGraphics, _context, Brushes.Black);
            this.Refresh();
        }

        private void ClearBuffer(BufferedGraphics graphics, BufferedGraphicsContext context, Brush clearColor)
        {
            graphics.Graphics.FillRectangle(
                clearColor,
                new Rectangle(new Point(0, 0), context.MaximumBuffer));
        }
    }
}
