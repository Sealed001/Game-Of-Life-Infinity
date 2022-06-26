using System;
using System.Collections.Generic;

namespace GameOfLife
{
	internal struct CellUpdateBuffer
	{
		public bool isAlive;
		public int neighboursCount;
		
		public CellUpdateBuffer(bool isAlive, int neighboursCount)
		{
			this.isAlive = isAlive;
			this.neighboursCount = neighboursCount;
		}
	}
	
	public struct Vector2Int
	{
		public int x;
		public int y;

		public Vector2Int(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}

	public class Game2D
	{
		public List<Vector2Int> cells = new ();
		
		public Game2D()
		{
			cells.Add(new Vector2Int(0, 0));
			cells.Add(new Vector2Int(0, 1));
			
			cells.Add(new Vector2Int(1, 0));
			cells.Add(new Vector2Int(1, 1));
			
			cells.Add(new Vector2Int(8, 0));
			
			cells.Add(new Vector2Int(9, -1));
			cells.Add(new Vector2Int(9, 0));
			cells.Add(new Vector2Int(9, 1));
			
			cells.Add(new Vector2Int(10, -2));
			cells.Add(new Vector2Int(10, -1));
			cells.Add(new Vector2Int(10, 0));
			cells.Add(new Vector2Int(10, 1));
			cells.Add(new Vector2Int(10, 2));
			
			cells.Add(new Vector2Int(11, -3));
			cells.Add(new Vector2Int(11, -1));
			cells.Add(new Vector2Int(11, 1));
			cells.Add(new Vector2Int(11, 3));

			cells.Add(new Vector2Int(12, -3));
			cells.Add(new Vector2Int(12, -2));
			cells.Add(new Vector2Int(12, 2));
			cells.Add(new Vector2Int(12, 3));
			
			cells.Add(new Vector2Int(15, 0));
			
			cells.Add(new Vector2Int(16, -1));
			cells.Add(new Vector2Int(16, 1));
			
			cells.Add(new Vector2Int(17, -1));
			cells.Add(new Vector2Int(17, 1));
			cells.Add(new Vector2Int(17, 2));
			
			cells.Add(new Vector2Int(18, 0));
			cells.Add(new Vector2Int(18, 1));
			cells.Add(new Vector2Int(18, 2));
			
			cells.Add(new Vector2Int(19, 0));
			cells.Add(new Vector2Int(19, 3));
			
			cells.Add(new Vector2Int(20, 1));
			cells.Add(new Vector2Int(20, 2));
			cells.Add(new Vector2Int(20, 3));
			
			cells.Add(new Vector2Int(21, 0));
			cells.Add(new Vector2Int(21, 4));
			
			cells.Add(new Vector2Int(22, -1));
			cells.Add(new Vector2Int(22, 5));
			
			cells.Add(new Vector2Int(23, 0));
			cells.Add(new Vector2Int(23, 4));
			
			cells.Add(new Vector2Int(24, 1));
			cells.Add(new Vector2Int(24, 2));
			cells.Add(new Vector2Int(24, 3));
			
			cells.Add(new Vector2Int(34, 2));
			cells.Add(new Vector2Int(34, 3));
			cells.Add(new Vector2Int(35, 2));
			cells.Add(new Vector2Int(35, 3));
		}

		public void Update()
		{
			List<Vector2Int> newCells = new List<Vector2Int>();

			Dictionary<int, Dictionary<int, CellUpdateBuffer>> cellsUpdateBuffer
				= new Dictionary<int, Dictionary<int, CellUpdateBuffer>>();

			foreach (Vector2Int cell in cells)
			{
				for (int xOffset = -1; xOffset <= 1; xOffset++)
				{
					int x = cell.x + xOffset;
					
					if (!cellsUpdateBuffer.ContainsKey(x))
					{
						cellsUpdateBuffer[x] = new Dictionary<int, CellUpdateBuffer>();
					}

					for (int yOffset = -1; yOffset <= 1; yOffset++)
					{
						int y = cell.y + yOffset;

						bool isAlive = xOffset == 0 && yOffset == 0;
						
						if (!cellsUpdateBuffer[x].ContainsKey(y))
						{
							cellsUpdateBuffer[x][y] = new CellUpdateBuffer(isAlive, isAlive ? 0 : 1);
						}
						else
						{
							cellsUpdateBuffer[x][y] = new CellUpdateBuffer(
								cellsUpdateBuffer[x][y].isAlive || isAlive,
								cellsUpdateBuffer[x][y].neighboursCount + (isAlive ? 0 : 1)
							);
						}
					}
				}
			}

			foreach (KeyValuePair<int, Dictionary<int, CellUpdateBuffer>> horizontal in cellsUpdateBuffer)
			{
				foreach (KeyValuePair<int, CellUpdateBuffer> vertical in horizontal.Value)
				{
					Vector2Int coordinates = new Vector2Int(horizontal.Key, vertical.Key);
					
					bool isAlive = vertical.Value.isAlive;
					int neighboursCount = vertical.Value.neighboursCount;

					switch (neighboursCount)
					{
						case 2:
							if (isAlive)
							{
								newCells.Add(coordinates);
							}
							break;
						case 3:
							newCells.Add(coordinates);
							break;
					}
				}
			}

			cells = newCells;
		}
	}
	
	public class Game3D
	{
		
	}
}