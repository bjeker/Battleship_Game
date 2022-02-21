using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
	public class Ship
	{
		//variables for ship
		string name;
		int length;
		int health;
		Direction direction;
		int x;
		int y;
		int[,] shipLocation;
		int tileCount;

		//ship object with name and length
		public Ship(string name, int length)
        {
			this.name = name;
			this.length = length;
			health = length;
			shipLocation = new int[length, 2];
        }

		//name of ship
		public string Name
        {
            get => name;
			set => name = value;
        }

        //length of ship
        public int Length
        {
			get => length;
			set => length = value;
        }

		//health of ship
		public int Health
		{
			get => health;
			set => health = value;
		}

		//get and set direction of ship
		public Direction Direction
        {
			get => direction;
			set => direction = value;
        }

		//x location of ship
		public int X
        {
			get => x;
			set => x = value;
        }

		//y location of ship
		public int Y
        {
			get => y;
			set => y = value;
        }

		//set position of ship and counter
		public void SetPosition(int xLocation, int yLocation)
        {
			shipLocation[tileCount, 0] = xLocation;
			shipLocation[tileCount, 1] = yLocation;
			tileCount++;
        }

	}

}