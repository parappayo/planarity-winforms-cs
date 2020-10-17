using System;
using System.Drawing;
using System.Windows.Forms;

namespace BushidoBurrito.Planarity
{
    public class GameForm : Form
    {
        private readonly BufferedGraphicsContext _context;
        private BufferedGraphics _bufferedGraphics;

        private GameState _gameState = new GameState();
        private GameInput _gameInput = new GameInput();

        public GameForm() : base()
        {
            this.Text = "Planarity";
            this.KeyDown += this.KeyDownHandler;
            this.MouseDown += this.MouseDownHandler;
            this.MouseUp += this.MouseUpHandler;
            this.MouseMove += this.MouseMoveHandler;
            this.Resize += this.OnResize;
            this.SetStyle( ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true );

            _context = BufferedGraphicsManager.Current;

            ResizeBuffer();
            DrawFrame();
        }

        private void KeyDownHandler(object sender, KeyEventArgs e)
        {
            _gameInput.HandleKeyPress(_gameState, e.KeyCode);
            DrawFrame();
        }

        private void MouseDownHandler(object sender, MouseEventArgs e)
        {
            if( e.Button == MouseButtons.Left )
            {
                _gameInput.HandleMouseDown(_gameState, e.X, e.Y);
            }
        }

        private void MouseUpHandler(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _gameInput.HandleMouseUp(_gameState, e.X, e.Y);
            }
            DrawFrame();
        }

        private void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            _gameInput.HandleMouseMove(_gameState, e.X, e.Y);
            DrawFrame();
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

        private PointF ToPointF(Pip pip)
        {
            return new PointF(pip.X, pip.Y);
        }

        private RectangleF ToRectangleF(Pip pip)
        {
            float size = 50F;
            return new RectangleF(pip.X - size/2F, pip.Y - size/2F, size, size);
        }

        private void DrawConnection(Edge<Pip> connection, Graphics graphics)
        {
            graphics.DrawLine(
                new Pen(Color.White),
                ToPointF(connection.From),
                ToPointF(connection.To));
        }

        private void DrawPip(Pip pip, Graphics graphics)
        {
            graphics.DrawEllipse(
                new Pen(Color.Blue),
                ToRectangleF(pip));
        }

        private void DrawFrame()
        {
            var graphics = _bufferedGraphics.Graphics;

            foreach (var connection in _gameState.Connections)
            {
                DrawConnection(connection, graphics);
            }

            foreach (var pip in _gameState.Pips)
            {
                DrawPip(pip, graphics);
            }

            this.Refresh();
        }
    }
}
