using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FcAgent
{
    public class MapSquare
    {
        //percepts
        private bool obstacle;
        //facts
        private bool clear, door, visited;
		FcAgent CurrentAgent = null;

        public bool Obstacle
        {
            get
            {
                return obstacle;
            }

            set
            {
                obstacle = value;
            }
        }
        

		public bool Visited
        {
            get
            {
                return visited;
            }

            set
            {
                visited = value;
            }
        }
        public bool Clear
        {
            get
            {
                return clear;
            }

            set
            {
                clear = value;
            }
        }

        public bool Door
        {
            get
            {
                return door;
            }

            set
            {
                door = value;
            }
        }


		internal FcAgent CurrentAgent1 { get => CurrentAgent; set => CurrentAgent = value; }

		public MapSquare()
        {
            obstacle = clear = door = visited = false;
        }

       

        public String ReturnSquare()
        {
            String s = "";
            if (CurrentAgent != null)
                s += "A";
            if (visited)
                s += "V";
            if (obstacle)
                s+="Obs";
            if (clear)
                s += "Cl";
            if (door)
                s += "Dr";
            if (!obstacle && !clear && !door)
                s += "-";
            while (s.Length < 10)
            {
                s += " ";
            }
            
                
            return s;
        }
		public void PutAgent(FcAgent a) {
            CurrentAgent = a;
            visited = true;
        }
        public void RemoveAgent()
        {
            CurrentAgent = null;
            

        }
    }
}
