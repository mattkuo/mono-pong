using System;

namespace Pong
{
	public class Player
	{
		public const int Length = 100;
		public const int Width = 10;
		public const int MoveSpeed = 5;

		public int X { get; set; }
		public int Y { get; set; }

		public Player (int startX, int startY)
		{
			this.X = startX;
			this.Y = startY;
		}
	}
}
