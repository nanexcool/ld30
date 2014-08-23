using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD30
{
    class CharacterSelectScreen : Screen
    {
        float elapsed = 0;

        int selected = 0;

        public CharacterSelectScreen()
        {
            background = Util.Game.Content.Load<Texture2D>("choose");
        }

        public override void Update()
        {
            base.Update();

            if (input.IsKeyPressed(Keys.Left))
            {
                // highlight left
                selected = 1;
            }
            else if (input.IsKeyPressed(Keys.Right))
            {
                // highlight right
                selected = 2;
            }

            if (input.IsKeyPressed(Keys.Enter))
            {
                if (selected != 0)
                {
                    GameplayScreen screen = new GameplayScreen(selected);
                    ScreenManager.SetScreen(screen);
                }
            }

            elapsed += Time.Delta;
        }

        public override void Draw()
        {
            //base.Draw();
            Util.Render(background);

            Util.DrawShadowedText("Use the ARROW KEYS to select a character.", Vector2.Zero);
            Util.DrawShadowedText("Press ENTER when ready.", new Vector2(0, 20));

            switch (selected)
            {
                case 1:
                    Util.DrawShadowedText("Player 1", new Vector2(60, 60));
                    break;
                case 2:
                    Util.DrawShadowedText("Player 2", new Vector2(340, 60));
                    break;
                default:
                    break;
            }
        }
    }
}
