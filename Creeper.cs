using System;
using SplashKitSDK;

public class Creeper
{
    private int row, col; // Creeper's position in the maze
    private int cellSize;
    private MazeGame maze;
    private Random random;
    private int moveDelay; // Frame delay for creeper movement
    private int frameCounter; // Counts frames to control movement speed

    public Creeper(MazeGame maze, int cellSize)
    {
        this.maze = maze;
        this.cellSize = cellSize;
        this.random = new Random();
        this.moveDelay = 10; // Number of frames to wait before moving
        this.frameCounter = 0;

        // Place the creeper randomly in the maze
        do
        {
            row = random.Next(0, maze.Rows);
            col = random.Next(0, maze.Cols);
        }
        while (maze.MazeGrid[row, col] != 1); // Ensure the creeper is placed on a valid path

        // Debugging to confirm placement
        Console.WriteLine($"Creeper initialized at Row: {row}, Col: {col}");
    }

    // Method to randomly move the creeper, slowed down by moveDelay
    public void MoveRandomly()
    {
        frameCounter++;

        // Only move the creeper if enough frames have passed
        if (frameCounter >= moveDelay)
        {
            int[] directions = { 0, 1, 2, 3 };
            ShuffleArray(directions);

            foreach (int direction in directions)
            {
                int newRow = row, newCol = col;

                switch (direction)
                {
                    case 0: newRow -= 1; break; // Move up
                    case 1: newCol += 1; break; // Move right
                    case 2: newRow += 1; break; // Move down
                    case 3: newCol -= 1; break; // Move left
                }

                if (maze.CanMoveTo(newRow, newCol))
                {
                    row = newRow;
                    col = newCol;
                    break; // Only move in one valid direction per frame
                }
            }

            frameCounter = 0; // Reset the frame counter after a move
        }
    }

    // Shuffle an array of directions for random movement
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

    // Method to render the creeper
    public void Draw()
    {
        SplashKit.FillRectangle(Color.Red, col * cellSize, row * cellSize, cellSize, cellSize);
    }

    // Check if creeper collides with the player
    public bool IsCollidingWithPlayer(Player player)
    {
        return player.Row == row && player.Col == col;
    }
}
