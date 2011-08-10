using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace shooter02.Interfaces
{
    interface GameState
    {
        private string m_szStateName;

        public GameState()
        {
            m_szStateName = "";
        }

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

        public void Render()
        {
        }
    }
}
