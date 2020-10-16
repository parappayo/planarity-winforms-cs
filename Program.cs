using System;
using System.Windows.Forms;

namespace BushidoBurrito.Planarity
{
    public class PlanarityMain
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Run(new GameForm
            {
                Width = 1024,
                Height = 768,
            });
        }
    }
}
