using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using aima.core.logic.fol.kb;
using aima.core.logic.fol.domain;
using aima.core.logic.fol.inference;
using aima.core.logic.fol.inference.proof;
using aima.core.logic.fol.parsing.ast;
using aima.core.logic.propositional.agent;
using aima.core.agent;
using aima.core.environment.wumpusworld;
using aima.core.logic.propositional.parsing.ast;

enum Direction { East, South, North, West };
namespace FcAgent
{
    


	public class FcAgent : KBAgent
	{
		private string Textb = "";
		private List<Point> visited = new List<Point>();
		private int actions = 0;
		private Corridor corridor;
		private MapSquare[,] map;
		private int CurrentX = 0;
		private int CurrentY = 0;
		private Random r = new Random();
        private FOLKnowledgeBase kb = FOLKnowledgeBaseFactory.CreateProjectKnowledgeBase(new FOLFCAsk());

		private Point startpoint = new Point(0, 0);
		private List<Point> traversedPoints = new List<Point>();
		Stack<Point> availablepoints = new Stack<Point>();
		int performance = 0;


        
		public FcAgent(Corridor c) : base(FOLKnowledgeBaseFactory.CreateProjectKnowledgeBase(new FOLFCAsk()))
		{
			corridor = c;
			map = c.map;
			map[0, 0].PutAgent(this);
			visited.Add(new Point(0, 0));
			AgentPercept percept = new AgentPercept(true,true,false,true,false);
			execute(percept);
            InferenceResult resul = kb.ask("Clear(x,y)");
            //TODO tworzenie zapytania do bazy
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
            return "Ahead(1,3,Door)";
            //return "Ahead(" + x + ","  + y + ",Door";
            //
            //if (door)
            //{
            //	Console.WriteLine("Ahead is Door");
            //	return "Ahead(Door)";
            //}
            //if (obstacle)
            //{
            //	Console.WriteLine("point ( " + x + "," + y + ") is obstacle");
            //	return "Ahead(Obstacle)";
            //}
            //else
            //{
            //	//Console.WriteLine("Ahead is Clear ");
            //	return "Ahead(Clear)";
            //}

        }
		//This function makes the agent take a step in the corridor
		public void Step()
		{
			Point point = new Point(CurrentX, CurrentY);
			bool obs = map[CurrentX, CurrentY].Obstacle;
			bool dr = map[CurrentX, CurrentY].Door;

			//kb.tell(CreateSentence(obs, dr, CurrentX, CurrentY)); nie tutaj
            //wstawiamy tutaj zbudowane zdanie na podstawie zmiennych 

			if (obs)
            {
                //tu powinno być odnotowywanie zderzenie z przeszkodą
                throw new Exception("The agent hitted the obstacle");
            }
     
			if (dr)
			{
				throw new Exception("The agent found the door and won");
			}

			List<Point> clear = new List<Point>();
			List<Point> notclear = new List<Point>();

			List<Point> obstaclenodes = new List<Point>();

			//Asking FOLKnowledgeBase about safety of neighbouring cell's safety
			//foreach (Point p in visited)
			//{
				//making neighbours for each visited node
			List<Point> neighbours = Getpointneighbours(point);
				foreach (Point n in neighbours)
				{   

					bool obsn = map[n.X, n.Y].Obstacle;
				    bool drn = map[n.X, n.Y].Door;
				    kb.tell(CreateSentence(obsn, drn, CurrentX, CurrentY));
				    
					if (obsn)
					{   
					    //kb.tell()
						//Console.WriteLine("point ( " + n.X + "," + n.Y + ") is obstacle");
						obstaclenodes.Add(n);
					}
					else
					{
						Console.WriteLine("point ( "+n.X+","+n.Y+") is clear");
						if (!clear.Contains(n))
							clear.Add(n);
					}
				}
			//}


			Console.WriteLine();
			//if there are safe nodes go to safe one
			List<Point> newclear = new List<Point>();
			foreach (Point s in clear)
			{
				if (!visited.Contains(s))
				{
					newclear.Add(s);

				}
			}
			Console.WriteLine("Not Visited Safe Nodes:");
			foreach (Point l in newclear)
			{
				Console.WriteLine(l);
			}
			Console.WriteLine("These nodes are obstacles:");
			foreach (Point l in obstaclenodes)
			{
				Console.WriteLine(l);
			}
           
			//if there are safe new nodes to visit go to that node
			if (newclear.Count != 0)
			{

				Point nextpoint = new Point(CurrentX, CurrentY + 1);
				if (!newclear.Contains(nextpoint))
				{
					nextpoint = newclear.First();
				}
					
				Console.WriteLine("Moving to location " + nextpoint + "\n");
				startpoint = new Point(CurrentX, CurrentY);
				MovetoLocation(nextpoint.X, nextpoint.Y);


			}
			//if there are no new safe nodes to visit pick a random new node to go to
			else
			{
				Console.WriteLine("No more safe nodes\n");


				bool nomoremoves = true;
				foreach (Point m in notclear)
				{
					if (!visited.Contains(m))
					{
						Console.WriteLine("Moving to random node " + m + "\n");
						startpoint = new Point(CurrentX, CurrentY);
						MovetoLocation(m.X, m.Y);
						nomoremoves = false;
						break;
					}
				}
				if (nomoremoves)
				{
					Console.WriteLine("Can't make any more moves\n");
				}
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

		public override string makePerceptSentence(Percept percept, int t)
		{
			throw new NotImplementedException();
		}

		public override aima.core.logic.propositional.parsing.ast.Sentence makeActionQuery(int t)
		{
			throw new NotImplementedException();
		}

		public override string makeActionSentence(aima.core.agent.Action action, int t)
		{
			throw new NotImplementedException();
		}

		public override aima.core.agent.Action ask(FOLKnowledgeBase KB, aima.core.logic.propositional.parsing.ast.Sentence actionQuery)
		{
			throw new NotImplementedException();
		}
	}
}
