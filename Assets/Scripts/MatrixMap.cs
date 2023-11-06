using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LevelMapGenerator
{
    public enum UnitType { Hero, Civilian, Policeman, Maniac }
    public class MatrixMap
    {
        private static Random random = new Random();
        public const char GROUND = 'G';
        public const char WALL = 'W';
        public const char RIVER = 'R';
        public const char EMPTY = '_';
        private int _groundPotential;
        private int _exStepDirection = -1;
        public char[,] Matrix { get; private set; }

        private bool CheckIfEmptiesLeft()
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    if (Matrix[i, j] == EMPTY)
                        return true;
                }
            }

            return false;
        }

        private void FillWithGroundOnly()
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    Matrix[i, j] = GROUND;
                }
            }
        }
        public MatrixMap(int hight, int width, int groundAmount, int wallsPotential, int wallMaxLength)
        {
            Matrix = new char[hight, width];
            // Filling Matrix with EMPTY cahrs.
            for (int i = 0; i < hight; i++)
            {
                for (int j = 0; j < width; j++)
                    Matrix[i, j] = EMPTY;
            }

            // Checking argument for tiles amount is not being bigger than level's area or less than 0.
            if (groundAmount >= hight * width)
            {
                groundAmount = hight * width;
                FillWithGroundOnly();
                groundAmount = 0;
            }
            else if (groundAmount < 0)
                groundAmount = 0;
            else
                _groundPotential = groundAmount;

            GenerateGroundTiles();
            if (CheckIfEmptiesLeft())
            {
                GenerateRiverTiles();
            }

            if(wallMaxLength < 1)
                wallMaxLength = 1;

            GenerateWallTiles(wallsPotential, wallMaxLength);
            
        }

        private void GenerateGroundTiles()
        {
            int exDirection = -1;
            int direction = exDirection;
            int Y = random.Next(0, Matrix.GetLength(0));
            int X = random.Next(0, Matrix.GetLength(1));
            Matrix[Y, X] = GROUND;
            _groundPotential -= 1;

            while (_groundPotential > 0)
            {
                while (_exStepDirection == direction)
                {
                    direction = random.Next(0, 4);
                }
                _exStepDirection = direction;


                if (direction == 0 && Y - 1 >= 0)
                {
                    if (Matrix[Y - 1, X] == EMPTY)
                    {
                        Matrix[Y - 1, X] = GROUND;
                        _groundPotential--;
                    }

                    Y -= 1;
                }
                if (direction == 1 && X - 1 >= 0)
                {
                    if (Matrix[Y, X - 1] == EMPTY)
                    {
                        Matrix[Y, X - 1] = GROUND;
                        _groundPotential--;
                    }

                    X -= 1;
                }
                if (direction == 2 && Y + 1 < Matrix.GetLength(0))
                {
                    if (Matrix[Y + 1, X] == EMPTY)
                    {
                        Matrix[Y + 1, X] = GROUND;
                        _groundPotential--;
                    }

                    Y += 1;
                }
                if (direction == 3 && X + 1 < Matrix.GetLength(1))
                {
                    if (Matrix[Y, X + 1] == EMPTY)
                    {
                        Matrix[Y, X + 1] = GROUND;
                        _groundPotential--;
                    }

                    X += 1;
                }
            }
        }

        private void GenerateRiverTiles()
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    if (Matrix[i, j] == EMPTY)
                        Matrix[i, j] = RIVER;
                }
            }
        }
        public const int TO_TOP = 2;
        public const int TO_BOTTOM = 0;
        public const int TO_LEFT = 3;
        public const int TO_RIGHT = 1;
        private bool CheckIfWallIsAvailible(int y, int x, int wallSpawnDirection)
        {
            if (y - 1 >= 0 && (Matrix[y - 1, x] == GROUND || wallSpawnDirection == TO_TOP) &&
                y + 1 < Matrix.GetLength(0) && (Matrix[y + 1, x] == GROUND || wallSpawnDirection == TO_BOTTOM) &&
                x - 1 >= 0 && (Matrix[y, x - 1] == GROUND || wallSpawnDirection == TO_LEFT) &&
                x + 1 < Matrix.GetLength(1) && (Matrix[y, x + 1] == GROUND || wallSpawnDirection == TO_RIGHT))
            {
                if (y - 1 > 0 && x - 1 > 0 && Matrix[y - 1, x - 1] == GROUND &&
                    y + 1 < Matrix.GetLength(0) && x - 1 > 0 && Matrix[y + 1, x - 1] == GROUND &&
                    y + 1 < Matrix.GetLength(0) && x + 1 < Matrix.GetLength(1) && Matrix[y + 1, x + 1] == GROUND &&
                    y - 1 > 0 && x + 1 < Matrix.GetLength(1) && Matrix[y - 1, x + 1] == GROUND)
                {
                    return true;
                }
            }
            return false;
        }

        private void GenerateWallTiles(int mazePotential, int wallMaxLength)
        {
            int currentWallLength;
            int exDirection;
            int direction;
            int Y = random.Next(0, Matrix.GetLength(0));
            int X = random.Next(0, Matrix.GetLength(1));
            bool isAproved = false;

            while (mazePotential > 0)
            {
                currentWallLength = wallMaxLength;
                exDirection = -1;
                direction = exDirection;

                while (!isAproved && mazePotential > 0)
                {
                    Y = random.Next(0, Matrix.GetLength(0));
                    X = random.Next(0, Matrix.GetLength(1));
                    if (CheckIfWallIsAvailible(Y, X, exDirection))
                    {
                        Matrix[Y, X] = WALL;
                        currentWallLength--;
                        isAproved = !isAproved;
                    }
                    mazePotential--;
                }
                isAproved = !isAproved;
                

                while (exDirection == direction)
                {
                    direction = random.Next(0, 4);
                }
                exDirection = direction;

                while (true)
                {
                    if (direction == 0)
                    {
                        if (CheckIfWallIsAvailible(Y - 1, X, exDirection) && currentWallLength > 0)
                        {
                            Matrix[Y - 1, X] = WALL;
                            mazePotential--;
                            currentWallLength --;
                        }
                        else
                            break;

                        Y -= 1;
                    }
                    if (direction == 1)
                    {
                        if (CheckIfWallIsAvailible(Y, X - 1, exDirection) && currentWallLength > 0)
                        {
                            Matrix[Y, X - 1] = WALL;
                            mazePotential--;
                            currentWallLength --;
                        }
                        else
                            break;

                        X -= 1;
                    }
                    if (direction == 2)
                    {
                        if (CheckIfWallIsAvailible(Y + 1, X, exDirection) && currentWallLength > 0)
                        {
                            Matrix[Y + 1, X] = WALL;
                            mazePotential--;
                            currentWallLength--;
                        }
                        else
                            break;

                        Y += 1;
                    }
                    if (direction == 3)
                    {
                        if (CheckIfWallIsAvailible(Y, X + 1, exDirection) && currentWallLength > 0)
                        {
                            Matrix[Y, X + 1] = WALL;
                            mazePotential--;
                            currentWallLength--;
                        }
                        else
                            break;

                        Y -= 1;
                    }
                }
            }
        }
    }
}
