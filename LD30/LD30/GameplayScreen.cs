using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace LD30
{
    enum Character
    {
        One = 1,
        Two = 2
    }

    class GameplayScreen : Screen
    {
        int player;

        public GameplayScreen(int selected)
        {
            player = selected;
            background = Util.Game.Content.Load<Texture2D>("gameplay");
        }

        public override void Update()
        {
            base.Update();

            if (input.IsKeyPressed(Keys.Escape))
            {
                TitleScreen screen = new TitleScreen();
                ScreenManager.SetScreen(screen);
            }
        }

        public override void Draw()
        {
            base.Draw();

            Util.Render(background);

            Util.DrawShadowedText("You selected character " + player.ToString(), Vector2.Zero);
            Util.DrawShadowedText("Press ESCAPE to return to Main Menu", new Vector2(0, 20));
        }
    }
}
