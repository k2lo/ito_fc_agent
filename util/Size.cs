using System;
namespace ito_fc_agent.util
{
	public class Size
	{

		private int x;
		private int y;

		public Size(int x, int y)
		{
			this.x = x;
			this.y = y;

		}

		public int X
		{
			get
			{
				return x;
			}

		}

		public int Y
		{
			get
			{
				return y;
			}
		}

	}
}
