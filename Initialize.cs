using System;

namespace FcAgent
{
    public class Initialize
    {
		public Corridor corridor;
		public Agent agent;
        bool agentdead = false;
        bool agentwon = false;
		aima.core.logic.fol.kb.FOLKnowledgeBase kb = null;
        
       
		public void Init(){

			corridor = new Corridor(10, 15, 3);
			corridor.InitializeRandomWorld();

			agent = new Agent(corridor);


		}

		public void AgenStart(){
			
		}

        public Initialize()
        {
        }
    }
}
