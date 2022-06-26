using System.Numerics;
using Raylib_cs;

namespace GameOfLife
{
	class Program
	{
		static void Main()
		{
			Camera2D camera = new Camera2D(
				new Vector2(400, 300),
				new Vector2(0, 0),
				0.0f,
				1.0f
			);
			
			Game2D game = new Game2D();

			float updateTimer = -2f;
			
			Raylib.SetConfigFlags(
				ConfigFlags.FLAG_WINDOW_RESIZABLE
				| ConfigFlags.FLAG_MSAA_4X_HINT
			);
			
			Raylib.InitWindow(800, 600, "Game of Life");
			Raylib.SetTargetFPS(60);

			while (!Raylib.WindowShouldClose())
			{
				camera.offset = new Vector2(Raylib.GetScreenWidth() / 2f, Raylib.GetScreenHeight() / 2f);
				
				updateTimer += Raylib.GetFrameTime();
				
				while (updateTimer >= 0.1f)
				{
					game.Update();
					updateTimer -= 0.1f;
				}

				Raylib.BeginDrawing();
					Raylib.ClearBackground(Color.BLACK);
					Raylib.BeginMode2D(camera);
						foreach (Vector2Int cellPosition in game.cells)
						{
							Raylib.DrawRectangle(cellPosition.x * 20 - 10, -cellPosition.y * 20 - 10, 20, 20, Color.GREEN);
						}
					Raylib.EndMode2D();
				Raylib.EndDrawing();
			}
			
			Raylib.CloseWindow();
		}
	}
}