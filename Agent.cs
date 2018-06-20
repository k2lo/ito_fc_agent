using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using aima.core.logic.fol.kb;
using aima.core.logic.fol.domain;

enum Direction { East, South, North, West };
namespace FcAgent
{
	public class Agent
	{
		private string Textb = "";
		private List<Point> visited = new List<Point>();
		private int actions = 0;
		private Corridor corridor;
		private MapSquare[,] map;
		private int CurrentX = 0;
		private int CurrentY = 0;
		private Random r = new Random();
		private FOLKnowledgeBase kb;
		private Point startpoint = new Point(0, 0);
		private List<Point> traversedPoints = new List<Point>();
		Stack<Point> availablepoints = new Stack<Point>();
		int performance = 0;
		public Agent(Corridor c)
		{

			corridor = c;
			map = c.map;
			map[0, 0].PutAgent(this);
			visited.Add(new Point(0, 0));


			FOLDomain fOLDomain = new FOLDomain();
			kb = new FOLKnowledgeBase(fOLDomain);

		}
		public int Actions
		{
			get
			{
				return actions;
			}

			set
			{
				actions = value;
			}
		}

		public int CurrentX1
		{
			get
			{
				return CurrentX;
			}

			set
			{
				CurrentX = value;
			}
		}

		public int CurrentY1
		{
			get
			{
				return CurrentY;
			}

			set
			{
				CurrentY = value;
			}
		}

		public int Performance { get => performance; set => performance = value; }
		public FOLKnowledgeBase Kb { get => kb; set => kb = value; }
		public string Textb1 { get => Textb; set => Textb = value; }

		private static double GetpointDistance(double x1, double y1, double x2, double y2)
		{
			return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
		}
		private void MovetoLocation(int x, int y)
		{



			corridor.map[CurrentX1, CurrentY1].RemoveAgent();

			CurrentX = x;
			CurrentY = y;
			Point visitedp = new Point(CurrentX, CurrentY);
			if (!visited.Contains(visitedp))
				visited.Add(new Point(CurrentX, CurrentY));
			corridor.map[x, y].PutAgent(this);




		}

		private bool MoveAgentNorth()
		{
			if (CurrentY1 - 1 >= 0)
			{

				corridor.map[CurrentX1, CurrentY1].RemoveAgent();
				CurrentY1--;
				corridor.map[CurrentX1, CurrentY1].PutAgent(this);
				performance--;
				return true;
			}
			else
			{
				return false;
			}
		}
		private bool MoveAgentSouth()
		{
			if (CurrentY1 + 1 < corridor.Size.Y)
			{

				corridor.map[CurrentX1, CurrentY1].RemoveAgent();
				CurrentY1++;
				corridor.map[CurrentX1, CurrentY1].PutAgent(this);
				performance--;
				return true;
			}
			else
			{
				return false;
			}
		}
		private bool MoveAgentEast()
		{
			if (CurrentX1 + 1 < corridor.Size.X)
			{

				corridor.map[CurrentX1, CurrentY1].RemoveAgent();
				CurrentX1++;
				corridor.map[CurrentX1, CurrentY1].PutAgent(this);
				performance--;
				return true;
			}
			else
			{
				return false;
			}
		}
		private bool MoveAgentWest()
		{
			if (CurrentX1 - 1 >= 0)
			{

				corridor.map[CurrentX1, CurrentY1].RemoveAgent();
				CurrentX1--;
				corridor.map[CurrentX1, CurrentY1].PutAgent(this);
				performance--;
				return true;
			}
			else
			{
				return false;
			}
		}

		private string CreateSentence(bool obstacle, bool door, int x, int y)
		{
			//TODO
			return "Ahead is " + (y + 1) + " , (nopit([A1, " + y + "]); assert(ispit([A1," + y + "])))";
		}

		//This function makes the agent take a step in the corridor
		public void Step()
		{
			Textb = "";
			//Telling FOLKnowledgeBase agent's percept
			bool obs = map[CurrentX, CurrentY].Obstacle;
			bool dr = map[CurrentX, CurrentY].Door;
			//kb.tell(obs, dr, false, CurrentX, CurrentY);

			kb.tell(CreateSentence(obs, dr, CurrentX, CurrentY));
			if (dr)
			{
				throw new Exception("The agent found the door and won");
			}
			List<Point> clear = new List<Point>();
			List<Point> notclear = new List<Point>();

			List<Point> obstaclenodes = new List<Point>();

			////Asking FOLKnowledgeBase about safety of neighbouring cell's safety
			//foreach (Point p in visited)
			//{
			//	//making neighbours for each visited node
			//	List<Point> neighbours = Getpointneighbours(p);
			//	foreach (Point n in neighbours)
			//	{
			//		if (kb.isobstacle(n.X, n.Y) && !visited.Contains(n))
			//		{
			//			obstaclenodes.Add(n);
			//		}
			//		if (!kb.isobstacle(n.X, n.Y))
			//		{
			//			//Console.WriteLine("point ( "+n.X+","+n.Y+") is "+kb.ispit(n.X, n.Y));
			//			if (!clear.Contains(n))
			//				clear.Add(n);
			//		}
			//		else
			//		{
			//			if (!notclear.Contains(n))
			//				notclear.Add(n);
			//		}
			//	}
			//}


			Console.WriteLine();
			//if there are safe nodes go to safe one
			//Console.WriteLine(safe.Count);
			List<Point> newclear = new List<Point>();
			foreach (Point s in clear)
			{
				if (!visited.Contains(s))
				{
					newclear.Add(s);

				}
			}
			Textb += "Not Visited Safe Nodes:\n";
			foreach (Point l in newclear)
			{
				Textb += l + ", \n";
			}
			Textb += "\n";
			Textb += "These nodes are obstacles: \n";
			foreach (Point l in obstaclenodes)
			{
				Textb += l + ", \n";
			}

			Textb += "\n";
			//if there are safe new nodes to visit go to that node
			if (newclear.Count != 0)
			{
				Point nextpoint = newclear.First();
				Textb += "Moving to location " + nextpoint + "\n";
				startpoint = new Point(CurrentX, CurrentY);
				MovetoLocation(nextpoint.X, nextpoint.Y);


			}
			//if there are no new safe nodes to visit pick a random new node to go to
			else
			{
				Textb += "No more safe nodes\n";


				bool nomoremoves = true;
				foreach (Point m in notclear)
				{
					if (!visited.Contains(m))
					{
						Textb += "Moving to random node " + m + "\n";
						startpoint = new Point(CurrentX, CurrentY);
						MovetoLocation(m.X, m.Y);
						nomoremoves = false;
						break;
					}
				}
				if (nomoremoves)
				{
					Textb += "Can't make any more moves\n";
				}
			}

			if (corridor.map[CurrentX, CurrentY].Obstacle)
			{
				//tu powinno być odnotowywanie zderzenie z przeszkodą
				throw new Exception("The agent hitted the obstacle");
			}

		}

		private List<Point> Getpointneighbours(Point p)
		{
			List<Point> neighbours = new List<Point>();
			new List<Point>();
			if (p.X + 1 < corridor.Size.X)
				neighbours.Add(new Point(p.X + 1, p.Y));
			if (p.Y + 1 < corridor.Size.Y)
				neighbours.Add(new Point(p.X, p.Y + 1));
			if (p.X - 1 >= 0)
				neighbours.Add(new Point(p.X - 1, p.Y));
			if (p.Y - 1 >= 0)
				neighbours.Add(new Point(p.X, p.Y - 1));
			return neighbours;
		}


	}
}
