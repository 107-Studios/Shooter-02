using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Threading;

namespace shooter02.Threading
{
    class UpdateManager
    {
        public List<UpdateData> GameDataObjects { get; set; }
        private DoubleBuffer doubleBuffer;
        private GameTime gameTime;
        protected ChangeBuffer messageBuffer;
        protected Game game;
        public Thread RunningThread { get; set; }

        public UpdateManager(DoubleBuffer doubleBuffer, Game game)
        {
            this.doubleBuffer = doubleBuffer;
            this.game = game;
            this.GameDataObjects= new List<UpdateData>();
        }

        public void DoFrame()
        {
            doubleBuffer.StartUpdateProcessing(out messageBuffer, out gameTime);
            this.Update(gameTime);
            doubleBuffer.SubmitUpdate();
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        private void Run()
        {
            #if XBOX
                Thread.CurrentThread.SetProcessorAffintiy(5);
            #endif

            while (true)
                DoFrame();
        }

        public void StartOnNewThread()
        {
            ThreadStart ts = new ThreadStart(Run);
            RunningThread = new Thread(ts);
            RunningThread.Start();
        }
    }
}
