using System;

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
		Player player;

		public PongGame ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			int playerCenter = GraphicsDevice.Viewport.Height / 2 - Player.Length / 2;
			player = new Player (0, playerCenter);
            
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
            
			KeyboardState state = Keyboard.GetState ();

			if (state.IsKeyDown(Keys.Escape))
				Exit ();

			if (state.IsKeyDown (Keys.Down) && GraphicsDevice.Viewport.Height > player.Y + Player.Length)
				player.Y += Player.MoveSpeed;
			if (state.IsKeyDown (Keys.Up) && player.Y > 0)
				player.Y -= Player.MoveSpeed;
			
            
			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.Black);
            
			spriteBatch.Begin ();
			spriteBatch.Draw (whiteRectangle, new Rectangle (player.X, player.Y, Player.Width, Player.Length), Color.White);
			spriteBatch.End ();
            
			base.Draw (gameTime);
		}
	}
}

