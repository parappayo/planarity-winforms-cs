using System;
using System.Windows.Forms;

namespace BushidoBurrito.Planarity
{
    public class GameInput
    {
        private Pip _mouseDragTarget;

        internal void HandleKeyPress(GameState gameState, Keys keyCode)
        {
            if (keyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }

        internal void HandleMouseDown(GameState gameState, int x, int y)
        {
            _mouseDragTarget = gameState.FindPip(new Point<float>(x, y), 25F);
        }

        internal void HandleMouseUp(GameState gameState, int x, int y)
        {
            _mouseDragTarget = null;
            gameState.CheckWinCondition();
        }

        internal void HandleMouseMove(GameState gameState, int x, int y)
        {
            if (_mouseDragTarget == null) { return; }
            _mouseDragTarget.X = x;
            _mouseDragTarget.Y = y;
        }
    }
}
