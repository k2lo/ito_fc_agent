using System;

namespace FcAgent
{
    static class Program
    {
        static void Main()
        {
			Initialize initialize = new Initialize();
			initialize.Init();
			initialize.corridor.PrintMap();
            for (int i = 0; i < 200; i++)
			{
				initialize.agent.Step();
                initialize.corridor.PrintMap();
			}

        }
    }
}
