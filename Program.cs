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
			initialize.agent.Step();
			initialize.corridor.PrintMap();
			initialize.agent.Step();
			initialize.corridor.PrintMap();
        }
    }
}
