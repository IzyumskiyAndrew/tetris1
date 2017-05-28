using NConsoleGraphics;

namespace tetris
{

    public interface IGameObject
    {
        bool IsRunning { get; }
        void Render(ConsoleGraphics graphics);

        void Update(GameEngine engine);

    }
}