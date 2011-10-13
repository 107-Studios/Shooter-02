using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using shooter02.Threading;
using Microsoft.Xna.Framework;
using shooter02.ObjectManager;

namespace shooter02.Managers
{
    class ShooterUpdater : UpdateManager
    {
        public ShooterUpdater(DoubleBuffer doubleBuffer, Game game) : base(doubleBuffer, game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            CObjectManager.Instance.Update(gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void addChangeMessage(ChangeMessage msg)
        {
            messageBuffer.Add(msg);
        }
    }
}
