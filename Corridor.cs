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
			numofpits = pits;
			numofwumpus = obstacles;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    map[i, j] = new MapSquare();
                }
               
            }
            
        }
		
		public int Size { get => size; set => size = value; }

		public void InitializeSpecificWorld(CorridorPlacements p)
		{
			foreach (Point i in p.Obstacles)
			{
				Placeobstacles(i.X, i.Y);
			}
			map[(int)p.Gold.X, (int)p.Gold.Y].Glitter = true;

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
            while (map[x, y].Pit || map[x, y].Wumpus || (x == 0 && y == 0))
            {
                x = r.Next(0, size);
                y = r.Next(0, size);
            }
            map[x, y].Glitter = true;
           
            

        }
		private void Placeobstacles(int x,int y)
		{
			map[x, y].Obstacle = true;
			//map[x, y].Stench = true;
			if (x + 1 < size && x + 1 >= 0)
			{
				map[x + 1, y].Stench = true;
			}
			if (x - 1 < size && x - 1 >= 0)
			{
				map[x - 1, y].Stench = true;
			}
			if (y + 1 < size && y + 1 >= 0)
			{
				map[x, y + 1].Stench = true;
			}
			if (y - 1 < size && y - 1 >= 0)
			{
				map[x, y - 1].Stench = true;
			}
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
            Console.WriteLine("(W = Wumpus, St=Stench, Br=Breeze, Gl=Glitter, Sc=Scream, V=Visited, Sa=Safe, P=Pit, A=Agent)");
        }

        
    }
}
