using System;
using System.Collections.Generic;
using System.Text;

namespace Draw
{
    class Tile
    {
        private int _x;
		public int x
		{
			get { return _x; }
			set { _x = value; }
		}

		private int _y;
		public int y
		{
			get { return _y; }
			set { _y = value; }
		}

		private string _symbol;
		public string symbol
		{
			get { return _symbol; }
			set { _symbol = value; }
		}


		public Tile(int x, int y, string symbol)
		{
			this.x = x;
			this.y = y;
			this.symbol = symbol;
		}

		public override string ToString()
		{
			return $"({x},{y},{symbol})";
		}
	}
}
