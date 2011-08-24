using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace shooter02.Managers
{
    public class KeyBindings
    {
        Keys Shoot;
        Keys MoveLeft;
        Keys MoveRight;
        Keys MoveUp;
        Keys MoveDown;

        // constructor
        public KeyBindings()
        {
           Shoot = Keys.J;
           MoveLeft = Keys.A;
           MoveRight = Keys.D;
           MoveUp = Keys.W;
           MoveDown = Keys.S;
        }
    }

    public sealed class InputManager
    {
        // static class setup
        private static readonly InputManager instance = new InputManager();
        public static InputManager Instance
        {
            get
            {
                return instance;
            }
        }

        // members
        // TODO: Gamepad Support
        //private GamePadState PrevPadState;
        //private GamePadState CurrPadState;

        private KeyboardState PrevKeyState;
        private KeyboardState CurrKeyState;

        // constructors
        private InputManager()
        {
            PrevKeyState = Keyboard.GetState();
        }

        // methods
        public void Update()
        {
            CurrKeyState = Keyboard.GetState();
        }

        public void OnEndFrame()
        {
           // update the previous state
           PrevKeyState = CurrKeyState;
        }

        public bool KeyPressed( Keys eKey )
        {
            // is eKey being pressed now?
            if (CurrKeyState.IsKeyDown(eKey))
            {
                // if eKey was not down last frame,
                if (!PrevKeyState.IsKeyDown(eKey))
                {
                    // eKey has been pressed
                    return true;
                }
            }

            // eKey was not pressed
            return false;
        }

        public bool KeyReleased(Keys eKey)
        {
            // is eKey down now?
            if (KeyPressed(eKey))
            {
                // it hasn't been released yet
                return false;
            }
            // was eKey down last frame?
            else if (PrevKeyState.IsKeyDown(eKey))
            {
                // eKey was released this frame
                return true;
            }

            // it hasn't been released yet
            return false;
        }

        public bool KeyDown(Keys eKey)
        {
            // is eKey down now?
            if (CurrKeyState.IsKeyDown(eKey))
            {
                // was eKey down last frame?
                if (PrevKeyState.IsKeyDown(eKey))
                {
                    // eKey is being held down
                    return true;
                }
            }

            // eKey is not being held down
            return false;
        }
    }
}
