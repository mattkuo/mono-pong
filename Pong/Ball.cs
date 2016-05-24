using System;
using Microsoft.Xna.Framework;

namespace Pong
{
	public class Ball
	{
		public const int Width = 15;

		private Rectangle ball;

		public Rectangle BallRect { get { return this.ball; } }

		public int X { 
			get { return this.ball.Location.X; }
			set { this.ball.Location = new Point (value, this.Y); }
		}

		public int Y { 
			get { return this.ball.Location.Y; }
			set { this.ball.Location = new Point (this.X, value); }
		}

		public Ball (int startX, int startY)
		{
			this.ball = new Rectangle (startX, startY, Ball.Width, Ball.Width);
		}
	}
}

