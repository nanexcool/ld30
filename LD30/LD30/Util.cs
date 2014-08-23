using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD30
{
    static class Util
    {
        public static SpriteFont DebugFont { get; private set; }
        public static SpriteFont TitleFont { get; private set; }
        public static Texture2D Texture { get; private set; }
        public static SpriteBatch SB { get; set; }

        public static Game Game { get; private set; }

        public static void Initialize(Game game)
        {
            Game = game;

            DebugFont = game.Content.Load<SpriteFont>("Fonts/DebugFont");
            TitleFont = game.Content.Load<SpriteFont>("Fonts/TitleFont");

            Texture = new Texture2D(game.GraphicsDevice, 1, 1);
            Texture.SetData<Color>(new Color[1] { Color.White });
        }

        public static void DrawShadowedText(String text, Vector2 position)
        {
            SB.DrawString(Util.DebugFont, text, position + new Vector2(0, 2), Color.Black);
            SB.DrawString(Util.DebugFont, text, position, Color.White);
        }

        public static void DrawShadowedTitleText(String text, Vector2 position)
        {
            SB.DrawString(Util.TitleFont, text, position + new Vector2(0, 4), Color.Black);
            SB.DrawString(Util.TitleFont, text, position, Color.White);
        }

        public static void Render(Texture2D texture)
        {
            SB.Draw(texture, Vector2.Zero, Color.White);
        }

        public static void Render(Texture2D texture, Vector2 position)
        {
            SB.Draw(texture, position, Color.White);
        }
    }
}
