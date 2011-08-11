using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace shooter02.Interfaces
{
    public interface GameState
    {
        void EnterState();
        void ExitState();
        bool Update(GameTime gameTime);
        void Render(GameTime gameTime);
    }
}
