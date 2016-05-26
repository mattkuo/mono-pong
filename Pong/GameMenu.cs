using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Pong
{
	public class GameMenu
	{
		public enum Options { Start, Quit };
		private string[] buttonText = { "Start", "Quit" };

		private const int MenuWidth = 300;
		private const int MenuPadding = 20;
		private Color bgColor = new Color (145, 163, 176);

		private Color buttonBgColor = new Color (188, 212, 230);
		Texture2D buttonBgTexture;
		List<GameMenuButton> gameButtons = new List<GameMenuButton> ();

		Rectangle menuRectangle;
		Texture2D menuBgTexture;

		SpriteFont menuFont;

		public GameMenu (GraphicsDevice graphicsDevice, SpriteFont font)
		{
			this.menuFont = font;
			menuBgTexture = new Texture2D (graphicsDevice, 1, 1);
			menuBgTexture.SetData (new[] { this.bgColor });

			buttonBgTexture = new Texture2D (graphicsDevice, 1, 1);
			buttonBgTexture.SetData (new[] { this.buttonBgColor });

			int buttonWidth = MenuWidth - (MenuPadding * 2);
			int numButtons = Enum.GetNames (typeof(Options)).Length;
			int textHeight = (int) this.menuFont.MeasureString ("A").Y;
			int buttonHeight = GameMenuButton.ButtonPadding * 2 + textHeight;
			int menuHeight = (1 + numButtons) * MenuPadding + (numButtons * buttonHeight);

			int alignX = graphicsDevice.Viewport.Width / 2 - MenuWidth / 2;
			int alignY = graphicsDevice.Viewport.Height / 2 - menuHeight / 2;

			menuRectangle = new Rectangle (alignX, alignY, MenuWidth, menuHeight);
			createButtons (alignX, alignY, buttonWidth, buttonHeight);
		}

		public Options? handleMouseClick(int x, int y)
		{
			Point clickedPoint = new Point (x, y);

			foreach (GameMenuButton button in gameButtons) {
				if (button.buttonRectangle.Contains (clickedPoint)) {
					return button.optionType;
				}
			}

			return null;
		}

		public void draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin ();
			spriteBatch.Draw (menuBgTexture, menuRectangle, Color.White);

			foreach (GameMenuButton gameButton in gameButtons) {
				gameButton.draw(spriteBatch, this.menuFont, buttonBgTexture);
			}

			spriteBatch.End ();
		}

		private void createButtons(int menuStartX, int menuStartY, int buttonWidth, int buttonHeight)
		{
			int offsetY = menuStartY + MenuPadding;
			int offsetX = menuStartX + MenuPadding;

			int counter = 0;
			foreach (Options option in Enum.GetValues (typeof(Options))) {
				GameMenuButton button = new GameMenuButton (option, buttonText [counter], offsetX, offsetY, buttonWidth, buttonHeight);
				gameButtons.Add (button);
				offsetY += buttonHeight + MenuPadding;
				counter++;
			}
		}
			
	}
}

