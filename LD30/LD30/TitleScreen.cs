using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD30
{
    class TitleScreen : Screen
    {
        Vector2 position;

        bool finished = false;

        float delay = 3;

        public TitleScreen()
        {
            position = new Vector2(20, 0);
            background = Util.Texture;
        }

        public override void Update()
        {
            base.Update();

            if (input.IsKeyDown(Keys.Escape) && delay < 1)
            {
                Util.Game.Exit();
            }

            if (input.AnyKeyPressed() && delay < 1)
            {
                ScreenManager.SetScreen(new WarningScreen());
            }

            if (!finished)
            {
                position += new Vector2(0, 100) * Time.Delta;
            }

            delay -= Time.Delta;

            if (position.Y > 100)
            {
                position = new Vector2(20, 100);
                finished = true;
            }
        }

        public override void Draw()
        {
            Util.SB.Draw(background, new Rectangle(0, 0, 480, 270), Color.Black * 0.9f);

            Util.DrawShadowedTitleText("Connected Worlds", position);
            if (finished & delay < 0)
            {
                Util.DrawShadowedText("Press any key to start", new Vector2(130, 200));
                Util.DrawShadowedText("ESCAPE to exit", new Vector2(170, 240));
            }
        }
    }
}
