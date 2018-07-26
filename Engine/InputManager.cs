using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace MGJamSummer2018.Engine
{
    class InputManager
    {
        public static InputManager Instance { get { return instance ?? (instance = new InputManager()); } }
        private static InputManager instance;

        protected MouseState cMouseState, pMouseState;
        protected KeyboardState cKeyboardState, pKeyboardstate;
        public InputManager()
        { }

        public void Update()
        {
            pKeyboardstate = cKeyboardState;
            pMouseState = cMouseState;
            cMouseState = Mouse.GetState();
            cKeyboardState = Keyboard.GetState();
        }

        // Mouse input checks
        public Vector2 MousePos        { get => new Vector2(cMouseState.Position.X, cMouseState.Position.Y); }
        public bool MouseLeftPressed   { get => (cMouseState.LeftButton == ButtonState.Pressed && pMouseState.LeftButton == ButtonState.Released); }
        public bool MouseLeftDown      { get => cMouseState.LeftButton == ButtonState.Pressed; }
        public bool MouseRightPressed  { get => (cMouseState.RightButton == ButtonState.Pressed && pMouseState.RightButton == ButtonState.Released); }
        public bool MouseRightDown     { get => cMouseState.RightButton == ButtonState.Pressed; }

        // Keyboard input checks
        public bool KeyPressed(Keys k) { return cKeyboardState.IsKeyDown(k) && pKeyboardstate.IsKeyUp(k); }
        public bool KeyDown(Keys k)    { return cKeyboardState.IsKeyDown(k); }
        public bool AnyKeyPressed      { get => (cKeyboardState.GetPressedKeys().Length > 0 && pKeyboardstate.GetPressedKeys().Length == 0); }

    }
}
