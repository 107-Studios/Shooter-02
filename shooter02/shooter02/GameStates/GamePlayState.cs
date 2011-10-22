using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using shooter02.Interfaces;
using shooter02.Threading;
using shooter02.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using shooter02.ObjectManager;
using shooter02.Managers.Events;

namespace shooter02.GameStates
{
    public sealed class GamePlayState : GameState
    {
        private static readonly GamePlayState instance = new GamePlayState();

        public static GamePlayState Instance
        {
            get
            {
                return instance;
            }
        }

        // singleton data members

        /* the following fields are for threading purposes*/
        DoubleBuffer doubleBuffer;
        ShooterRender renderManager;
        ShooterUpdater updateManager;
        /*------------------------------------------------*/

        public void EnterState()
        {
            // allocation of dataz
            doubleBuffer = new DoubleBuffer();
            renderManager = new ShooterRender(doubleBuffer, StateManager.Instance.GameInstance);
            updateManager = new ShooterUpdater(doubleBuffer, StateManager.Instance.GameInstance);

            // load game object's "update/render data" to both the update/render managers
            // TODO: These need to be removed and initialized only when the designated player
            // presses start to join
            shooter02.GameObjects.CPlayer temp = ObjectFactory.CreatePlayer1('1');
            updateManager.GameDataObjects.Add(temp.UpdateData);
            renderManager.RenderDataObjects.Add(temp.RenderData);
            ObjectManager.CObjectManager.Instance.AddObject(temp);

            shooter02.GameObjects.CPlayer temp2 = ObjectFactory.CreatePlayer2('1');
            updateManager.GameDataObjects.Add(temp2.UpdateData);
            renderManager.RenderDataObjects.Add(temp2.RenderData);
            ObjectManager.CObjectManager.Instance.AddObject(temp2);

            shooter02.GameObjects.CPlayer temp3 = ObjectFactory.CreatePlayer3('1');
            updateManager.GameDataObjects.Add(temp3.UpdateData);
            renderManager.RenderDataObjects.Add(temp3.RenderData);
            ObjectManager.CObjectManager.Instance.AddObject(temp3);

            shooter02.GameObjects.CPlayer temp4 = ObjectFactory.CreatePlayer4('1');
            updateManager.GameDataObjects.Add(temp4.UpdateData);
            renderManager.RenderDataObjects.Add(temp4.RenderData);
            ObjectManager.CObjectManager.Instance.AddObject(temp4);
            //////////////////////////////////////////////////////////////////////////////

            // start the update function on a new thread
            updateManager.StartOnNewThread();
        }

        public void ExitState()
        {
            // take care of threading garbage
            doubleBuffer.CleanUp();
            if (updateManager.RunningThread != null)
                updateManager.RunningThread.Abort();
        }

        public bool Update(GameTime gameTime)
        {
            EventManager.Instance.ProcessEvents();
            return true;
        }

        public void Render(GameTime gameTime)
        {
            // signal update thread to start
            doubleBuffer.GlobalStartFrame(gameTime);

            // begin rendering
            renderManager.DoFrame();

            //wait for update thread to finish and signal a new frame will begin soon
            doubleBuffer.GlobalSync();
        }

        public void addMessage(ChangeMessage msg)
        {
            updateManager.addChangeMessage(msg);
        }
    }
}
