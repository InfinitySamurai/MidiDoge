using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midiGame
{
    public static class InputManager
    {
        public static KeyboardState currentKeyboardState { get; private set; }
        public static KeyboardState previousKeyboardState { get; private set; }

        public enum Action
        {
            MOVEUP,
            MOVEDOWN,
            MOVELEFT,
            MOVERIGHT
        }

        public static bool IsKeyPressed(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public static void update()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }
    }
}
