using System;
using System.Collections.Generic;
using SplashKitSDK;

public class Program
{
    public static void Main()
    {
        Window gameWindow = new Window("Procedural Maze Game", 1200, 800);
        MazeGame maze = new MazeGame(25, 35);
        Player player = new Player(maze, 30);
        List<Creeper> creepers = new List<Creeper>();

        // Create 5 random creepers
        for (int i = 0; i < 5; i++)
        {
            creepers.Add(new Creeper(maze, 30));
        }

        while (!SplashKit.QuitRequested())
        {
            SplashKit.ProcessEvents();
            SplashKit.ClearScreen(Color.Gray);

            // Render the maze first
            maze.RenderMaze();

            // Move and render creepers
            foreach (Creeper creeper in creepers)
            {
                creeper.MoveRandomly();
                creeper.Draw();

                // Check if any creeper collides with the player
                if (creeper.IsCollidingWithPlayer(player))
                {
                    SplashKit.DrawTextOnWindow(gameWindow, "Game Over! You've been caught!", Color.Red, "Arial", 48, 300, 400);
                    SplashKit.RefreshScreen(60);
                    SplashKit.Delay(3000); // Pause to display the message
                    return; // Exit the game loop
                }
            }

            // Handle player input and movement
            player.HandleInput();

            // Render the player
            player.Draw();

            // Check if player has reached the exit
            if (player.IsAtExit())
            {
                SplashKit.DrawTextOnWindow(gameWindow, "You've reached the destination!", Color.White, "Arial", 48, 300, 400);
            }

            SplashKit.RefreshScreen(60); // Refresh at 60 FPS
        }

        gameWindow.Close();
    }
}
