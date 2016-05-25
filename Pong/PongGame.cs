﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class PongGame : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		Texture2D whiteRectangle;
		Texture2D ballSprite;

		Player player, player2;
		Ball ball;

		SpriteFont scoreFont;

		public PongGame ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
			graphics.PreferredBackBufferWidth = 500;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			IsMouseVisible = true;

			int playerCenter = GraphicsDevice.Viewport.Height / 2 - Player.Length / 2;
			player = new Player (0, playerCenter);
			player2 = new Player (GraphicsDevice.Viewport.Width - Player.Width, playerCenter);

			this.resetBall ();
            
			base.Initialize ();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);

			whiteRectangle = new Texture2D (GraphicsDevice, 1, 1);
			whiteRectangle.SetData (new[] { Color.White });

			ballSprite = Content.Load<Texture2D> ("ball");

			scoreFont = Content.Load<SpriteFont> ("digital");
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			// Exit() is obsolete on iOS
			#if !__IOS__ &&  !__TVOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState ().IsKeyDown (Keys.Escape))
				Exit ();
			#endif
            
			updatePlayerMovement (Keys.W, Keys.S, player);
			updatePlayerMovement (Keys.Up, Keys.Down, player2);

			ball.X += (int) (ball.BallSpeed.X * gameTime.ElapsedGameTime.TotalSeconds);
			ball.Y += (int) (ball.BallSpeed.Y * gameTime.ElapsedGameTime.TotalSeconds);

			if (ball.X < 0) {
				player2.Score += 1;
				this.resetBall ();
				this.resetPaddles ();
			} else if (ball.X + Ball.Width > GraphicsDevice.Viewport.Width) {
				player.Score += 1;
				this.resetPaddles ();
				this.resetBall ();
			}

			if (ball.Y <= 0 || ball.Y + Ball.Width >= GraphicsDevice.Viewport.Height)
				ball.BallSpeed = new Vector2(ball.BallSpeed.X, ball.BallSpeed.Y * -1);

			bool isPaddleHit = false;
			Rectangle paddleHit = player.Paddle;
			if (ball.BallRect.Intersects (player.Paddle)) {
				isPaddleHit = true;
			} else if (ball.BallRect.Intersects (player2.Paddle)) {
				isPaddleHit = true;
				paddleHit = player2.Paddle;
			}
				
			if (isPaddleHit) {
				int paddleCenter = paddleHit.Center.Y;
				int collisionCenter = ball.BallRect.Center.Y;

				float multiplier = (float)(collisionCenter - paddleCenter) / (float)(Player.Length / 2);
				ball.BallSpeed = new Vector2 (ball.BallSpeed.X * -1, Ball.Speed * multiplier);
			}
            
			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.SlateGray);
            
			spriteBatch.Begin ();

			spriteBatch.Draw (whiteRectangle, player.Paddle, Color.White);
			spriteBatch.Draw (whiteRectangle, player2.Paddle, Color.White);
			spriteBatch.Draw(ballSprite, ball.BallRect, Color.White);

			int playerScoreX = GraphicsDevice.Viewport.Width / 4;
			int playerScoreX2 = GraphicsDevice.Viewport.Width - playerScoreX;

			this.drawScore (playerScoreX, 20, player);
			this.drawScore (playerScoreX2, 20, player2);

			spriteBatch.End ();
            
			base.Draw (gameTime);
		}

		private void drawScore(int x, int y, Player p) 
		{
			String playerScore = p.Score.ToString ();
			int scoreWidth = (int) (scoreFont.MeasureString (playerScore).X / 2);
			spriteBatch.DrawString (scoreFont, playerScore, new Vector2 (x - scoreWidth, y), Color.White);
		}

		private void resetPaddles()
		{
			int playerCenter = GraphicsDevice.Viewport.Height / 2 - Player.Length / 2;
			player.Y = playerCenter;
			player2.Y = playerCenter;
		}

		private void resetBall ()
		{
			int centerY = GraphicsDevice.Viewport.Height / 2 - Ball.Width / 2;
			int centerX = GraphicsDevice.Viewport.Width / 2 - Ball.Width / 2;
			ball = new Ball (centerX, centerY);
		}

		private void updatePlayerMovement (Keys up, Keys down, Player p)
		{
			KeyboardState state = Keyboard.GetState ();
			if (state.IsKeyDown (up) && p.Y > 0)
				p.Y -= Player.MoveSpeed;
			if (state.IsKeyDown (down) && GraphicsDevice.Viewport.Height > p.Y + Player.Length)
				p.Y += Player.MoveSpeed;

		}
	}
}
