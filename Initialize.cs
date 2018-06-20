using System;

namespace FcAgent
{
    public class Initialize
    {
		Corridor corridor;
		Agent agent;
        bool agentdead = false;
        bool agentwon = false;
		aima.core.logic.fol.kb.FOLKnowledgeBase kb = null;
        
       
		public void Run(){

			corridor = new Corridor(10, 15, 3);
			corridor.InitializeRandomWorld();

			agent = new Agent(corridor);
		}

        public Initialize()
        {
        }
    }
}
