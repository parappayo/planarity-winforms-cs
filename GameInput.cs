using System;
using System.Windows.Forms;

namespace BushidoBurrito.Planarity
{
    public class GameInput
    {
        internal void HandleKeyPress(GameState gameState, Keys keyCode)
        {
            if (keyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        internal void HandleMouseDown(GameState gameState, int x, int y)
        {
        }

        internal void HandleMouseUp(GameState gameState, int x, int y)
        {
        }

        internal void HandleMouseMove(GameState gameState, int x, int y)
        {
        }
    }
}
