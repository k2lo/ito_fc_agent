using System;

namespace FcAgent
{
    public class Initialize
    {
		public Corridor corridor;
		public FcAgent agent;
        bool agentdead = false;
        bool agentwon = false;
		aima.core.logic.fol.kb.FOLKnowledgeBase kb = null;
        
       
		public void Init(){

			corridor = new Corridor(10, 15, 50);
			corridor.InitializeRandomWorld();

			agent = new FcAgent(corridor);

		}

		public void AgenStart(){
			
		}

        public Initialize()
        {
        }
    }
}
