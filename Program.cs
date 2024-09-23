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
        List<PowerUp> powerUps = new List<PowerUp>();
        Random random = new Random();

        bool creepersVisible = true;
        int powerUpDuration = 5000; // 5 seconds duration
        DateTime powerUpCollectedTime = DateTime.MinValue;

        // Create 5 random creepers
        for (int i = 0; i < 10; i++)
        {
            creepers.Add(new Creeper(maze, 30));
        }

        // Create frequent power-ups
        for (int i = 0; i < 30; i++)
        {
            powerUps.Add(new PowerUp(maze, 30));
        }

        while (!SplashKit.QuitRequested())
        {
            SplashKit.ProcessEvents();
            SplashKit.ClearScreen(Color.Gray);

            // Render the maze first
            maze.RenderMaze();

            // Handle player input and movement
            player.HandleInput();

            // Render the power-ups
            foreach (PowerUp powerUp in powerUps)
            {
                powerUp.Draw();

                // Check if power-up is collected
                if (powerUp.IsCollectedByPlayer(player))
                {
                    // If collected, hide all creepers and start the timer
                    creepersVisible = false;
                    powerUpCollectedTime = DateTime.Now;

                    // Respawn the power-up in a new location
                    powerUp.Respawn();
                }
            }

            // Check if the creepers should reappear after the power-up duration
            if (!creepersVisible && (DateTime.Now - powerUpCollectedTime).TotalMilliseconds > powerUpDuration)
            {
                creepersVisible = true; // Make creepers visible again after timer
            }

            // Move and render creepers if they are visible
            if (creepersVisible)
            {
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
            }

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
