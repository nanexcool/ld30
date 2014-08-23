using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LD30
{
    class Input
    {
        KeyboardState state, oldState;

        public Input()
        {
            oldState = state = Keyboard.GetState();
        }

        public void Update()
        {
            oldState = state;
            state = Keyboard.GetState();
        }

        public bool IsKeyDown(Keys k)
        {
            return state.IsKeyDown(k);
        }

        public bool IsKeyPressed(Keys k)
        {
            return state.IsKeyDown(k) && oldState.IsKeyUp(k);
        }

        public bool IsKeyReleased(Keys k)
        {
            return state.IsKeyUp(k) && oldState.IsKeyDown(k);
        }

        public bool AnyKeyPressed()
        {
            return state.GetPressedKeys().Length > 0;
        }
    }
}
