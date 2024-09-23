using System;
using SplashKitSDK;

public class PowerUp
{
    private int row, col; // Power-up's position in the maze
    private int cellSize;
    private MazeGame maze;
    private Random random;

    public PowerUp(MazeGame maze, int cellSize)
    {
        this.maze = maze;
        this.cellSize = cellSize;
        this.random = new Random();

        // Place the power-up randomly in the maze
        do
        {
            row = random.Next(0, maze.Rows);
            col = random.Next(0, maze.Cols);
        }
        while (maze.MazeGrid[row, col] != 1); // Ensure the power-up is placed on a valid path

        Console.WriteLine($"Power-up initialized at Row: {row}, Col: {col}");
    }

    // Method to render the power-up
    public void Draw()
    {
        SplashKit.FillRectangle(Color.Yellow, col * cellSize, row * cellSize, cellSize, cellSize);
    }

    // Check if power-up is collected by the player
    public bool IsCollectedByPlayer(Player player)
    {
        return player.Row == row && player.Col == col;
    }
}
