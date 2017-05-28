using NConsoleGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
  public class SampleGameEngine : GameEngine
    {
        private Player player;
        public SampleGameEngine(ConsoleGraphics graphics)
           : base(graphics)
        {
            //AddObject(new SamplePlayer(graphics));
            player = new Player(graphics);
            AddObject(player);
        }
    }
}
