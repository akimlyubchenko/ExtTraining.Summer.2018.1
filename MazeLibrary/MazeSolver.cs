using System;

namespace MazeLibrary
{
    /// <summary>
    /// Maze solver finder
    /// </summary>
    public class MazeSolver
    {
        #region Fields
        /// <summary>
        /// Gets the current place x.
        /// </summary>
        public int CurrentPlaceX { get; private set; }

        /// <summary>
        /// Gets the current place y.
        /// </summary>
        public int CurrentPlaceY { get; private set; }

        /// <summary>
        /// Gets the last place x.
        /// </summary>
        public int LastPlaceX { get; private set; }

        /// <summary>
        /// Gets the last place y.
        /// </summary>
        public int LastPlaceY { get; private set; }

        private readonly int borderX;
        private readonly int borderY;

        /// <summary>
        /// Gets the neighbor left x.
        /// </summary>
        public int NeighborLeftX { get { return CurrentPlaceX; } }

        /// <summary>
        /// Gets the neighbor left y.
        /// </summary>

        public int NeighborLeftY { get { return CurrentPlaceY - 1; } }

        /// <summary>
        /// Gets the neighbor right x.
        /// </summary>
        public int NeighborRightX { get { return CurrentPlaceX; } }

        /// <summary>
        /// Gets the neighbor right y.
        /// </summary>
        public int NeighborRightY { get { return CurrentPlaceY + 1; } }

        /// <summary>
        /// Gets the neighbor down x.
        /// </summary>
        public int NeighborDownX { get { return CurrentPlaceX + 1; } }

        /// <summary>
        /// Gets the neighbor down y.
        /// </summary>
        public int NeighborDownY { get { return CurrentPlaceY; } }

        /// <summary>
        /// Gets the neighbor up x.
        /// </summary>
        public int NeighborUpX { get { return CurrentPlaceX - 1; } }

        /// <summary>
        /// Gets the neighbor up y.
        /// </summary>
        public int NeighborUpY { get { return CurrentPlaceY; } }

        private bool susuccess = false;

        public int[,] maze;
        #endregion
        #region Public API
        /// <summary>
        /// Initializes a new instance of the <see cref="MazeSolver"/> class.
        /// </summary>
        /// <param name="mazeModel">The maze array</param>
        /// <param name="startX">The start x.</param>
        /// <param name="startY">The start y.</param>
        public MazeSolver(int[,] mazeModel, int startX, int startY) //dont move
        {
            Validator(mazeModel, startX, startY);
            int[,] tempMaze = new int[mazeModel.GetLength(0), mazeModel.GetLength(1)];
            for (int i = 0; i < mazeModel.GetLength(0); i++)
            {
                for (int j = 0; j < mazeModel.GetLength(1); j++)
                {
                    tempMaze[i, j] = mazeModel[i, j];
                }
            }

            maze = tempMaze;
            maze[startX, startY] = 1;
            CurrentPlaceX = startX;
            CurrentPlaceY = startY;
            borderX = mazeModel.GetLength(0);
            borderY = mazeModel.GetLength(1);
        }

        /// <summary>
        /// Return done array
        /// </summary>
        /// <returns> Done array </returns>
        public int[,] MazeWithPass() //dont move
            => maze;

        /// <summary>
        /// Passes the maze.
        /// </summary>
        public void PassMaze() //dont move
            => PassMaze(CurrentPlaceX, CurrentPlaceY);
        #endregion
        #region Prevate API
        /// <summary>
        /// Passes the maze.
        /// </summary>
        /// <param name="currentPlaceX">The current place x.</param>
        /// <param name="currentPlaceY">The current place y.</param>
        /// <returns> Done array </returns>
        private int[,] PassMaze(int currentPlaceX, int currentPlaceY)
        {
            if (maze[currentPlaceX, currentPlaceY] != 1)
            {
                if (currentPlaceX == 0 || currentPlaceX == borderX - 1 || currentPlaceY == 0 || currentPlaceY == borderY - 1)
                {
                    susuccess = true;
                    return maze;
                }
            }

            if (IfZero(NeighborRightX, NeighborRightY))
            {
                OneStep(NeighborRightX, NeighborRightY, CurrentPlaceX, CurrentPlaceY);
                if (susuccess == true)
                {
                    return maze;
                }
            }

            if (IfZero(NeighborDownX, NeighborDownY))
            {
                OneStep(NeighborDownX, NeighborDownY, CurrentPlaceX, CurrentPlaceY);
                if (susuccess == true)
                {
                    return maze;
                }
            }

            if (IfZero(NeighborLeftX, NeighborLeftY))
            {
                OneStep(NeighborLeftX, NeighborLeftY, CurrentPlaceX, CurrentPlaceY);
                if (susuccess == true)
                {
                    return maze;
                }
            }

            if (IfZero(NeighborUpX, NeighborUpY))
            {
                OneStep(NeighborUpX, NeighborUpY, CurrentPlaceX, CurrentPlaceY);
                if (susuccess == true)
                {
                    return maze;
                }
            }

            maze[currentPlaceX, currentPlaceY] = 0;
            return null;
        }

        /// <summary>
        /// Called when when you need to take a step
        /// </summary>
        /// <param name="neigborX">The neigbor x.</param>
        /// <param name="neigborY">The neigbor y.</param>
        /// <param name="curX">The current x.</param>
        /// <param name="curY">The current y.</param>
        private void OneStep(int neigborX, int neigborY, int curX, int curY)
        {
            LastPlaceX = CurrentPlaceX;
            LastPlaceY = CurrentPlaceY;
            CurrentPlaceX = neigborX;
            CurrentPlaceY = neigborY;
            maze[neigborX, neigborY] = maze[LastPlaceX, LastPlaceY] + 1;
            PassMaze(neigborX, neigborY);
            CurrentPlaceX = curX;
            CurrentPlaceY = curY;
        }

        /// <summary>
        /// Checks if may to take a step
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns> true or false </returns>
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

        /// <summary>
        /// Validators the specified maze model.
        /// </summary>
        /// <param name="mazeModel">The maze model.</param>
        /// <param name="startX">The start x.</param>
        /// <param name="startY">The start y.</param>
        /// <exception cref="ArgumentNullException">MazeLibrary</exception>
        /// <exception cref="ArgumentException">
        /// Array doesn't be empty
        /// or
        /// Start point mustn't have negative value
        /// </exception>
        private void Validator(int[,] mazeModel, int startX, int startY)
        {
            if (mazeModel == null)
            {
                throw new ArgumentNullException(nameof(MazeLibrary));
            }

            if (mazeModel.GetLength(0) == 0)
            {
                throw new ArgumentException("Array doesn't be empty");
            }

            if (startX < 0 || startY < 0)
            {
                throw new ArgumentException("Start point mustn't have negative value");
            }
        }
        #endregion
    }
}
