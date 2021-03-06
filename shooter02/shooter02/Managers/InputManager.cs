﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace shooter02.Managers
{
    public class KeyBindings
    {
        public Keys Shoot;
        public Keys Combine;
        public Keys MoveLeft;
        public Keys MoveRight;
        public Keys MoveUp;
        public Keys MoveDown;

        // constructor
        public KeyBindings()
        {
           Shoot = Keys.J;
           Combine = Keys.K;
           MoveLeft = Keys.A;
           MoveRight = Keys.D;
           MoveUp = Keys.W;
           MoveDown = Keys.S;
        }
    }

    public class ButtonBindings
    {
        public Buttons Shoot;
        public Buttons Combine;
        public Buttons MoveLeft;
        public Buttons MoveRight;
        public Buttons MoveUp;
        public Buttons MoveDown;

        // constructor
        public ButtonBindings()
        {
            Shoot = Buttons.A;
            Combine = Buttons.RightShoulder;
            MoveLeft = Buttons.DPadLeft;
            MoveRight = Buttons.DPadRight;
            MoveUp = Buttons.DPadUp;
            MoveDown = Buttons.DPadDown;
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
        private GamePadState[] PrevPadState = new GamePadState[4];
        private GamePadState[] CurrPadState = new GamePadState[4];

        private KeyboardState PrevKeyState;
        private KeyboardState CurrKeyState;

        // constructors
        private InputManager()
        {
            PrevKeyState = Keyboard.GetState();

            for (int current = 0; current < 4; ++current)
                PrevPadState[current] = GamePad.GetState((PlayerIndex)current);
        }

        // methods
        public void Update()
        {
            CurrKeyState = Keyboard.GetState();

            for (int current = 0; current < 4; ++current)
                CurrPadState[current] = GamePad.GetState((PlayerIndex)current);
        }

        public void OnEndFrame()
        {
           // update the previous state
           PrevKeyState = CurrKeyState;

            for (int current = 0; current < 4; ++current)
                PrevPadState[current] = CurrPadState[current];

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

        public bool GamePadPress(PlayerIndex playerIndex, Buttons eButton)
        {
            // is the controller connected?
            if (!CurrPadState[(int)playerIndex].IsConnected)
                return false;

            // is eButton being pressed now?
            if (CurrPadState[(int)playerIndex].IsButtonDown(eButton))
            {
                // if eButton was not down last frame,
                if (!PrevPadState[(int)playerIndex].IsButtonDown(eButton))
                {
                    // eButton has been pressed
                    return true;
                }
            }

            // eButton was not pressed
            return false;
        }

        public bool GamePadReleased(PlayerIndex playerIndex, Buttons eButton)
        {
            // is the controller connected?
            if (!CurrPadState[(int)playerIndex].IsConnected)
                return false;

            // is eButton down now?
            if (GamePadPress(playerIndex, eButton))
            {
                // it hasn't been released yet
                return false;
            }
            // was eButton down last frame?
            else if (CurrPadState[(int)playerIndex].IsButtonDown(eButton))
            {
                // eButton was released this frame
                return true;
            }

            // it hasn't been released yet
            return false;

        }

        public bool GamePadDown(PlayerIndex playerIndex, Buttons eButton)
        {
            // is the controller connected?
            if (!CurrPadState[(int)playerIndex].IsConnected)
                return false;

            // is eButton down now?
            if (CurrPadState[(int)playerIndex].IsButtonDown(eButton))
            {
                // was eButton down last frame?
                if (PrevPadState[(int)playerIndex].IsButtonDown(eButton))
                {
                    // eButton is being held down
                    return true;
                }
            }

            // eButton is not being held down
            return false;
        }
    }
}
