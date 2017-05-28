using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NConsoleGraphics;

namespace tetris
{
    class ObjectO : IGameObject
    {
        private int x;
        private int y;
        private int width;
        private int height;
        private int speed;
        private bool isRunning;
        private Cell[] obj;
        private int[,] grid;
        private const int size = 4;
        private int tic;

        public ObjectO(int x, int y, int width, int height, int speed, ref int[,] grid)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.speed = speed;
            tic = 1;
            isRunning = true;
            obj = new Cell[size];
            FillValue();
            this.grid = grid;
        }        

        public bool IsRunning
        {
            get { return isRunning; }
        }

        private void FillValue()
        {
            obj[0] = new Cell { X = x, Y = y, Color = 0xFFFF0000 };
            obj[1] = new Cell { X = x + width, Y = y, Color = 0xFFFF0000 };
            obj[2] = new Cell { X = x, Y = y + height, Color = 0xFFFF0000 };
            obj[3] = new Cell { X = x + width, Y = y + height, Color = 0xFFFF0000 };
        }

        public void Render(ConsoleGraphics graphics)
        {
            //for (int i = 0; i < 2; i++)
            //{
            //    for (int j = 0; j < 2; j++)
            //    {
            //        graphics.FillRectangle(0xFFFF0000, x + i * width, y + j * height, width, height);
            //    }                
            //}     
            for (int i = 0; i < obj.Length; i++)
            {
                graphics.FillRectangle(obj[i].Color, obj[i].X, obj[i].Y, width, height);
            }       
        }

        public void Update(GameEngine engine)
        {
            if (Input.IsKeyDown(Keys.LEFT))
            {
                if (IsMovingOn(Keys.LEFT))
                {
                    for (int i = 0; i < obj.Length; i++)
                    {
                        obj[i].X -= width;
                    }
                }                
            }
            else if (Input.IsKeyDown(Keys.RIGHT))
            {
                if (IsMovingOn(Keys.RIGHT))
                {
                    for (int i = 0; i < obj.Length; i++)
                    {
                        obj[i].X += width;
                    }
                }                
            }
            else if (Input.IsKeyDown(Keys.DOWN))
            {
                if (IsMovingOn(Keys.DOWN))
                {                    
                    for (int i = 0; i < obj.Length; i++)
                    {
                        obj[i].Y += height;
                    }
                }                
            }

            if (tic % speed == 0)
            {
                if (IsMovingOn(Keys.DOWN))
                {                    
                    for (int i = 0; i < obj.Length; i++)
                    {
                        obj[i].Y += height;
                    }
                }
                else
                {
                    isRunning = false;
                    
                    for (int i = 0; i < 4; i++)
                    {
                        int gridX = (obj[i].X - x) / width;
                        int gridY = (obj[i].Y - y) / height; 
                        grid[gridX, gridY] = 1;
                    }
                }
            }
            tic++;
        }

        private bool IsMovingOn(Keys key)
        {
            bool isMovingOn = true;
            for (int i = 0; i < 4; i++)
            {
                int gridX = (obj[i].X - 5) / width;
                int gridY = (obj[i].Y - 5) / height;
                if (key == Keys.LEFT)
                {
                    if (gridX <= 0 || grid[gridX - 1, gridY] == 1)
                    {
                        isMovingOn = false;
                    }
                }

                if (key == Keys.RIGHT)
                {
                    if (gridX + 1 >= grid.GetLength(0) || grid[gridX+1, gridY] == 1)
                    {
                        isMovingOn = false;
                    }
                }

                if (key == Keys.DOWN)
                {
                    if (gridY+1 >= grid.GetLength(1) || grid[gridX, gridY + 1] == 1)
                    {
                        isMovingOn = false;
                    }
                }
            }
            return isMovingOn;
        }        
    }
}
