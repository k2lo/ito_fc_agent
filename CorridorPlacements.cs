using System.Collections.Generic;
using System.Drawing;


namespace FcAgent
{
	public class CorridorPlacements
	{
		private int size = 0;
		private Point door;
		private List<Point> obstacles = new List<Point>();
		public CorridorPlacements(List<Point> o, Point d)
		{
			obstacles = o;
			door = d;
		}

		public List<Point> Obstacles { get => obstacles; set => obstacles = value; }
		public Point Door { get => door; set => door = value; }
	}
}