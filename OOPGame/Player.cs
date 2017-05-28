using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace tetris
{
    class Player : IGameObject
    {
        private IGameObject figure;
        private int[,] grid;
        private int numberHeightCells = 20;
        private int numberWightCells = 10;
        private int xMin;
        private int yMin;
        private int xMax;
        private int yMax;
        private int width;
        private int height;
        private int xStartPoint;
        private List<int> figures;
        private int speed;
        Random rnd = new Random();
        private ConsoleGraphics graphics;
        private int score;

        public bool IsRunning { get; }
      
        public Player(ConsoleGraphics graphics)
        {
            xMin = 5;
            yMin = 5;
            xMax = 205;
            yMax = 405;
            width = (xMax - xMin) / numberWightCells;
            height = (yMax - yMin) / numberHeightCells;
            xStartPoint = numberWightCells * width / 2 - width + xMin;
            grid = new int[numberWightCells, numberHeightCells];
            this.graphics = graphics;
            for (int i = 0; i < numberWightCells; i++)
            {
                for (int j = 0; j < numberHeightCells; j++)
                {
                    grid[i, j] = 0;
                }
            }
            figures = new List<int>();
            foreach (int item in Enum.GetValues(typeof(Figure)))
            {
                figures.Add(item);
            }
            score = 0;
            speed = 30;
        } 

        public void Render(ConsoleGraphics graphics)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i,j] == 1)
                    {
                        graphics.FillRectangle(0xFF0FF0F0, width*i + xMin, height*j + yMin, width, height);
                    }
                }
            }

            figure?.Render(graphics);
            for (int i = xMin; i <= xMax; i += (xMax - xMin) / numberWightCells)
            {
                graphics.DrawLine(0xFF000000, i, yMin, i, yMax);
            }
            for (int i = yMin; i <= yMax; i += (yMax - yMin) / numberHeightCells)
            {
                graphics.DrawLine(0xFF000000, xMin, i, xMax, i);
            }
        }

        public void Update(GameEngine engine)
        {
            if(figure == null)
            {
                figure = ChooseGameObject();
            }            

            if (figure.IsRunning)
            {
                figure.Update(engine);
            }
            else
            {
                
                for (int i = 0; i < grid.GetLength(1); i++)
                {
                    for (int j = 0; j < grid.GetLength(0); j++)
                    {
                        if (grid[j, i] == 0)
                        {
                            break;
                        }
                        if(grid[j,i] == 1 && j == grid.GetLength(0)-1)
                        {
                            for (int k = i; k >= 0; k--)
                            {
                                for (int q = 0; q < grid.GetLength(0); q++)
                                {
                                    if (k != 0)
                                    {
                                        grid[q, k] = grid[q, k - 1];
                                    }
                                    else
                                    {
                                        grid[q, k] = 0;
                                    }                                    
                                }
                            }
                        }
                    }
                }            
                figure = ChooseGameObject();
            }            
        }

        private IGameObject ChooseGameObject()
        {
            IGameObject obj;
            int index = rnd.Next(0, 1);

            switch (index)
            {
                case 0:
                    obj = new ObjectI(xMin, yMin, width, height, speed, ref grid);
                    break;
                case 1:
                    obj = new ObjectO(xMin, yMin, width, height, speed, ref grid);                    
                    break;
                //case 2:
                //    obj = new ObjectO(beginPointX, minPosY, width, height);
                //    break;
                //case 3:
                //    obj = new ObjectO(beginPointX, minPosY, width, height);
                //    break;
                //case 4:
                //    obj = new ObjectO(beginPointX, minPosY, width, height);
                //    break;
                //case 5:
                //    obj = new ObjectO(beginPointX, minPosY, width, height);
                //    break;
                //case 6:
                //    obj = new ObjectO(beginPointX, minPosY, width, height);
                //    break;
                default:
                    obj = new ObjectO(xMin, yMin, width, height, speed, ref grid);
                    break;
            }
            return obj;
        }

        private void ChangeFigureCoordinates()
        {

        }
    }
}
