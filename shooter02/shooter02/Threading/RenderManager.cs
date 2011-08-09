using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace shooter02.Threading
{
    class RenderManager
    {
        public List<RenderData> RenderDataObjects { get; set; }
        private DoubleBuffer doubleBuffer;
        private GameTime gameTime;
        protected ChangeBuffer messageBuffer;
        protected Game game;

        public RenderManager(DoubleBuffer doubleBuffer, Game game)
        {
            this.doubleBuffer = doubleBuffer;
            this.game = game;
            this.RenderDataObjects = new List<RenderData>();
        }

        public virtual void LoadContent()
        {

        }

        public void DoFrame()
        {
            doubleBuffer.StartRenderProcessing(out messageBuffer, out gameTime);
            this.Draw(gameTime);
            doubleBuffer.SubmitRender();
        }

        public virtual void Draw(GameTime gameTime)
        {

        }
    }
}
