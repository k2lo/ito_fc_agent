using System;
using System.Drawing;

namespace FcAgent
{
	class Corridor
    {

        public MapSquare[,] map ;
		private int size_x;
		private int size_y;
		private int numofobstacles;		
		Random r = new Random();

		public Corridor(int size_x, int size_y, int door, int obstacles) {
			size = siZe;
            map = new MapSquare[size_x, size_y];
			numofobstacles = obstacles;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    map[i, j] = new MapSquare();
                }
               
            }
            
        }
		
		public int Size { get => size_x; get => size_y; set => size_x, set => size_y = value; }

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

            
            int x=0,y=0;

			for (int m = 0; m < numofobstacles; m++)
			{
				x = r.Next(1, size);
				y = r.Next(1, size);
				while (x == 0 && y == 0)
				{
					x = r.Next(1, size);
					y = r.Next(1, size);
				}
				Placeobstacles(x, y);
			}
            
            //Adding Gold
            x = r.Next(0, size);
            y = r.Next(0, size);
            while (map[x, y].Obstacle || (x == 0 && y == 0))
            {
                x = r.Next(0, size);
                y = r.Next(0, size);
            }
            map[x, y].Door = true;
           
            

        }
		private void Placeobstacles(int x,int y)
		{
			map[x, y].Obstacle = true;
		}
		
		public void PrintMap() {
			Console.WriteLine();
			Console.WriteLine("______________________________________________");
			Console.WriteLine("   0         1         2         3");
            for (int i = 0; i < size; i++) {
                Console.Write(i+"  ");
                for (int j = 0; j < size; j++)
                {
                    Console.Write(map[j,i].ReturnSquare());
                    
                }
                Console.WriteLine();
            }
            Console.WriteLine("(Obs=Obstacle, Dr=Door, V=Visited, A=Agent)");
        }

        
    }
}
