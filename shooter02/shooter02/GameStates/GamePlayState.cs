﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using shooter02.Interfaces;
using shooter02.Threading;
using shooter02.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using shooter02.ObjectManager;

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
            shooter02.GameObjects.CPlayer temp = ObjectFactory.createPlayer1();
            updateManager.GameDataObjects.Add(temp.UpdateData);
            renderManager.RenderDataObjects.Add(temp.RenderData);
            ObjectManager.CObjectManager.Instance.AddObject(temp);

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
