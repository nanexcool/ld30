using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD30
{
    class ScreenManager
    {
        Screen currentScreen;
        Screen nextScreen;



        public ScreenManager(Screen screen, float fade = 0)
        {
            SetScreen(screen, fade);
        }

        public void SetScreen(Screen screen, float fade = 0)
        {
            currentScreen = screen;
            screen.ScreenManager = this;
        }

        public void Update()
        {
            currentScreen.Update();

            if (nextScreen != null)
            {
                currentScreen = nextScreen;
                nextScreen = null;
            }
        }

        public void Draw()
        {
            currentScreen.Draw();
        }
    }
}
