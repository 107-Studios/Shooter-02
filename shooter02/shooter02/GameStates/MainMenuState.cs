using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using shooter02.Interfaces;
using Microsoft.Xna.Framework;

namespace shooter02.GameStates
{
    public sealed class MainMenuState : GameState
    {
        private static readonly MainMenuState instance = new MainMenuState();

        public static MainMenuState Instance
        {
            get
            {
                return instance;
            }
        }

        // singleton data members


        public void EnterState()
        {

        }

        public void ExitState()
        {

        }

        public bool Update(GameTime gameTime)
        {
            return true;
        }

        public void Render(GameTime gameTime)
        {
            
        }

    }
}
