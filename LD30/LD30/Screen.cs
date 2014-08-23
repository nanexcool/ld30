using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD30
{
    class Screen
    {
        public ScreenManager ScreenManager { get; set; }
        protected Input input;
        protected Texture2D background;

        public Screen()
        {
            input = new Input();
        }

        public virtual void Update()
        {
            input.Update();
        }

        public virtual void Draw()
        {

        }
    }
}
