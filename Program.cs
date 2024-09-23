using System;
using SplashKitSDK;

public class Program
{
    public static void Main()
    {
        Window gameWindow = new Window("Procedural Maze Game", 1200, 800);
        MazeGame maze = new MazeGame(25, 35);
        Player player = new Player(maze, 30); // Passing the maze and cell size

        while (!SplashKit.QuitRequested())
        {
            SplashKit.ProcessEvents();
            SplashKit.ClearScreen(Color.Gray);

            // Handle player input and movement
            player.HandleInput();

            // Render the maze and player
            maze.RenderMaze();
            player.Draw();

            // Check if player has reached the exit
            if (player.IsAtExit())
            {
                SplashKit.DrawTextOnWindow(gameWindow, "You've reached the destination!", Color.Red, "Arial", 140, 300, 400);
            }

            SplashKit.RefreshScreen(60); // Refresh at 60 FPS
        }

        gameWindow.Close();
    }
}
