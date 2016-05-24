using System;
using Microsoft.Xna.Framework;

namespace Pong
{
	public class Player
	{
		public const int Length = 100;
		public const int Width = 10;
		public const int MoveSpeed = 5;

		private Rectangle paddle;

		public Rectangle Paddle { get { return this.paddle; } }

		public int X 
		{
			get { return this.paddle.X; }
			set { this.paddle.Location = new Point (value, this.Y); }
		}

		public int Y 
		{
			get { return this.paddle.Y; }
			set { this.paddle.Location = new Point (this.X, value); }
		}

		public Player (int startX, int startY)
		{
			this.paddle = new Rectangle (startX, startY, Player.Width, Player.Length);
		}

	}
}
