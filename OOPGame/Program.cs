using NConsoleGraphics;
using System;

namespace tetris
{

  public class Program {

    static void Main(string[] args) {

      Console.WindowWidth = 40;
      Console.WindowHeight = 25;
      Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
      Console.BackgroundColor = ConsoleColor.White;
      Console.CursorVisible = false;
      Console.Clear();

      ConsoleGraphics graphics = new ConsoleGraphics();

      GameEngine engine = new SampleGameEngine(graphics);
      engine.Start();
    }
  }
}
