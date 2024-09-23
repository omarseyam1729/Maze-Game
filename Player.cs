using SplashKitSDK;

public class Player
{
    private int row, col; // Player's position in the maze
    private int cellSize; // Size of each cell in the maze
    private MazeGame maze;
    public int Row => row; // Expose the player's row
    public int Col => col; // Expose the player's column

    public Player(MazeGame maze, int cellSize)
    {
        this.maze = maze;
        this.cellSize = cellSize;
        this.row = maze.EntryPoint.Item1; // Start at maze entry
        this.col = maze.EntryPoint.Item2;
    }

    // Method to move the player
    public void HandleInput()
    {
        if (SplashKit.KeyTyped(KeyCode.UpKey) && maze.CanMoveTo(row - 1, col))
        {
            row -= 1;
        }
        else if (SplashKit.KeyTyped(KeyCode.DownKey) && maze.CanMoveTo(row + 1, col))
        {
            row += 1;
        }
        else if (SplashKit.KeyTyped(KeyCode.LeftKey) && maze.CanMoveTo(row, col - 1))
        {
            col -= 1;
        }
        else if (SplashKit.KeyTyped(KeyCode.RightKey) && maze.CanMoveTo(row, col + 1))
        {
            col += 1;
        }
    }

    // Method to render the player
    public void Draw()
    {
        SplashKit.FillRectangle(Color.Blue, col * cellSize, row * cellSize, cellSize, cellSize);
    }

    // Check if player reached the exit
    public bool IsAtExit()
    {
        return (row == maze.ExitPoint.Item1 && col == maze.ExitPoint.Item2);
    }
}
