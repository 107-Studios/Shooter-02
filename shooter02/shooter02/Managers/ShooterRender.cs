using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using shooter02.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using shooter02.Managers;

namespace shooter02.Managers
{
    class ShooterRender : RenderManager
    {

        public ShooterRender(DoubleBuffer doubleBuffer, Game game) : base(doubleBuffer, game)
        {
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (RenderData item in RenderDataObjects)
            {
             StateManager.Instance.SpriteBatchInstance.Draw(item.texture2D, item.position, item.color);           
            }
            base.Draw(gameTime);
        }
    }
}
