using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
	public class GameMenuButton
	{
		
		public const int ButtonPadding = 10;

		public Rectangle buttonRectangle;
		public GameMenu.Options optionType;
		string buttonText;


		public GameMenuButton (GameMenu.Options option, string text, int x, int y, int width, int height)
		{
			this.buttonRectangle = new Rectangle (x, y, width, height);
			this.buttonText = text;
			this.optionType = option;
		}

		public void draw(SpriteBatch spriteBatch, SpriteFont font, Texture2D texture)
		{
			spriteBatch.Draw (texture, this.buttonRectangle, Color.White);

			int textWidth = (int) font.MeasureString (buttonText).X;
			int alignX = this.buttonRectangle.Center.X - textWidth / 2;
			int alignY = this.buttonRectangle.Y + ButtonPadding;
			spriteBatch.DrawString (font, this.buttonText, new Vector2 (alignX, alignY), Color.White);
		}
	}
}

