using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD30
{
    class WarningScreen : Screen
    {
        float elapsed;

        public override void Update()
        {
            base.Update();

            elapsed += Time.Delta;

            if (input.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.Enter) && elapsed > 5)
            {
                ScreenManager.SetScreen(new CharacterSelectScreen());
            }
        }

        public override void Draw()
        {
            base.Draw();

            Util.DrawShadowedText("Warning", new Microsoft.Xna.Framework.Vector2(100, 0));
        }
    }
}
