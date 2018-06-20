using System;
using System.Drawing;

namespace FcAgent
{
	class Corridor
	{

		public MapSquare[,] map;
		private ito_fc_agent.util.Size size;
		private int numofobstacles;
		Random r = new Random();

		public Corridor(int size_x, int size_y, int obstacles)
		{
			size = new ito_fc_agent.util.Size(size_x, size_y);
			map = new MapSquare[size_x, size_y];
			numofobstacles = obstacles;
			for (int i = 0; i < size.X; i++)
			{
				for (int j = 0; j < size.Y; j++)
				{
					map[i, j] = new MapSquare();
				}
			}
		}

		public Corridor(ito_fc_agent.util.Size size, int obstacles) : this(size.X, size.Y, obstacles)
		{

		}


		public ito_fc_agent.util.Size Size
		{
			get
			{
				return size;
			}
			set
			{
				size = value;
			}
		}

		public void InitializeSpecificWorld(CorridorPlacements p)
		{
			foreach (Point i in p.Obstacles)
			{
				Placeobstacles(i.X, i.Y);
			}
			map[(int)p.Door.X, (int)p.Door.Y].Door = true;

		}
		public void InitializeRandomWorld()
		{


			int x = 0, y = 0;

			for (int m = 0; m < numofobstacles; m++)
			{
				x = r.Next(1, size.X);
				y = r.Next(1, size.Y);
				while (x == 0 && y == 0)
				{
					x = r.Next(1, size.X);
					y = r.Next(1, size.Y);
				}
				Placeobstacles(x, y);
			}

			//Adding Gold
			x = r.Next(0, size.X);


			while (map[x, size.Y-1].Obstacle)
			{
				x = r.Next(0, size.X);
			}
			map[x, size.Y].Door = true;



		}
		private void Placeobstacles(int x, int y)
		{
			map[x, y].Obstacle = true;
		}

		public void PrintMap()
		{
			Console.WriteLine();
			Console.WriteLine("______________________________________________");
			Console.WriteLine("   0         1         2         3");
			for (int i = 0; i < size.X; i++)
			{
				Console.Write(i + "  ");
				for (int j = 0; j < size.Y; j++)
				{
					Console.Write(map[j, i].ReturnSquare());

				}
				Console.WriteLine();
			}
			Console.WriteLine("(Obs=Obstacle, Dr=Door, V=Visited, A=Agent)");
		}


	}
}
