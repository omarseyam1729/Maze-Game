using System;
using SplashKitSDK;

public class MazeGame
{
    private int[,] maze;
    private int rows, cols;
    private int cellSize = 30; // Size of each cell (smaller size for larger maze)
    private Random random;
    private (int, int) entry, exit;
    public (int, int) EntryPoint => entry; // Getter for entry point
    public (int, int) ExitPoint => exit;   // Getter for exit point
    public bool CanMoveTo(int row, int col)
    {
    return IsInBounds(row, col) && maze[row, col] == 1;
    }
    public MazeGame(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
        this.maze = new int[rows, cols];
        this.random = new Random();

        // Set the entry point at the top left and exit at the bottom right
        entry = (0, 1);  // Entry in second cell of the top row
        exit = (rows - 1, cols - 2); // Exit in the second last cell of the last row

        GenerateMaze(0, 0); // Start generating from top-left corner
        maze[entry.Item1, entry.Item2] = 1; // Mark entry point
        maze[exit.Item1, exit.Item2] = 1; // Mark exit point
    }

    // Maze Generation using Depth-First Search (DFS) and backtracking
    private void GenerateMaze(int row, int col)
    {
        // Directions for N, E, S, W
        int[] directions = { 0, 1, 2, 3 };
        ShuffleArray(directions);

        foreach (int direction in directions)
        {
            int newRow = row, newCol = col;

            switch (direction)
            {
                case 0: newRow -= 2; break; // North
                case 1: newCol += 2; break; // East
                case 2: newRow += 2; break; // South
                case 3: newCol -= 2; break; // West
            }

            if (IsInBounds(newRow, newCol) && maze[newRow, newCol] == 0)
            {
                maze[(newRow + row) / 2, (newCol + col) / 2] = 1; // Break wall between cells
                maze[newRow, newCol] = 1; // Mark new cell as part of the maze
                GenerateMaze(newRow, newCol); // Recursively generate next part of maze
            }
        }
    }

    private bool IsInBounds(int row, int col)
    {
        return row >= 0 && row < rows && col >= 0 && col < cols;
    }

    private void ShuffleArray(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int randomIndex = random.Next(i, array.Length);
            int temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    // Render the maze using SplashKit
    public void RenderMaze()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Color color;

                // Color the maze cells with different colors
                if (row == entry.Item1 && col == entry.Item2)
                {
                    color = Color.Green; // Entry point
                }
                else if (row == exit.Item1 && col == exit.Item2)
                {
                    color = Color.Red; // Exit point
                }
                else if (maze[row, col] == 1)
                {
                    color = Color.White; // Open path
                }
                else
                {
                    color = Color.Black; // Wall
                }

                // Draw each cell
                SplashKit.FillRectangle(color, col * cellSize, row * cellSize, cellSize, cellSize);
            }
        }
    }
}
