using System;

namespace MazeLibrary
{
    public class MazeSolver
    {
        public int CurrentPlaceX { get; private set; }
        public int CurrentPlaceY { get; private set; }

        public int LastPlaceX { get; private set; }
        public int LastPlaceY { get; private set; }

        private readonly int borderX;
        private readonly int borderY;

        public int NeighborLeftX { get { return CurrentPlaceX; } }
        public int NeighborLeftY { get { return CurrentPlaceY - 1; } }
        public int NeighborRightX { get { return CurrentPlaceX; } }
        public int NeighborRightY { get { return CurrentPlaceY + 1; } }
        public int NeighborDownX { get { return CurrentPlaceX + 1; } }
        public int NeighborDownY { get { return CurrentPlaceY; } }
        public int NeighborUpX { get { return CurrentPlaceX - 1; } }
        public int NeighborUpY { get { return CurrentPlaceY; } }

        private int count = 1;
        private bool susuccess = false;

        private readonly int[,] maze;

        public MazeSolver(int[,] mazeModel, int startX, int startY) //dont move
        {
            int[,] tempMaze = new int[mazeModel.GetLength(0), mazeModel.GetLength(1)];
            for (int i = 0; i < mazeModel.GetLength(0); i++)
            {
                for (int j = 0; j < mazeModel.GetLength(1); j++)
                {
                    tempMaze[i, j] = mazeModel[i, j];
                }
            }

            maze = tempMaze;
            maze[startX,startY] = count;
            CurrentPlaceX = startX;
            CurrentPlaceY = startY;
            borderX = mazeModel.GetLength(0);
            borderY = mazeModel.GetLength(1);
        }



        public int[,] MazeWithPass() //dont move
            => maze;

        public void PassMaze() //dont move
            => PassMaze(CurrentPlaceX, CurrentPlaceY);

        public int[,] PassMaze(int currentPlaceX, int currentPlaceY)
        {
            if (count != 1)
            {
                if (currentPlaceX == 0 || currentPlaceX == borderX || currentPlaceY ==0 || currentPlaceY == borderY)
                {
                    susuccess = true;
                    return maze;
                }
            }

            if (IfZero(NeighborRightX, NeighborRightY))
            {
                OneStep(NeighborRightX, NeighborRightY);
                if (susuccess == true)
                {
                    return maze;
                }
            }

            if (IfZero(NeighborDownX, NeighborDownY))
            {
                OneStep(NeighborDownX, NeighborDownY);
                if (susuccess == true)
                {
                    return maze;
                }
            }

            if (IfZero(NeighborLeftX, NeighborLeftY))
            {
                OneStep(NeighborLeftX, NeighborLeftY);
                if (susuccess == true)
                {
                    return maze;
                }
            }

            if (IfZero(NeighborUpX, NeighborUpY))
            {
                OneStep(NeighborUpX, NeighborUpY);
                 if (susuccess == true)
                {
                    return maze;
                }
            }

            maze[currentPlaceX, currentPlaceY] = 0;
            count--;
            return null;
        }

        private void OneStep(int neigborX, int neigborY)
        {
            LastPlaceX = CurrentPlaceX;
            LastPlaceY = CurrentPlaceY;
            CurrentPlaceX = neigborX;
            CurrentPlaceY = neigborY;
            maze[neigborX, neigborY] = ++count;
            PassMaze(neigborX, neigborY);
            CurrentPlaceX = LastPlaceX;
            CurrentPlaceY = LastPlaceY;
        }

        private bool IfZero(int x, int y)
        {
            if (x > borderX - 1 || x < 0 || y > borderY - 1 || y < 0)
            {
                return false;
            }
            else if (maze[x, y] != 0)
            {
                return false;
            }

            return true;
        }
    }
}
