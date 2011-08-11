using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using shooter02.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using IndependentResolutionRendering;
using Microsoft.Xna.Framework.Content;

namespace shooter02.Managers
{
    public sealed class StateManager
    {

        private static readonly StateManager instance = new StateManager();
        public static StateManager Instance
        {
            get
            {
                return instance;
            }
        }

        private Game game = null;
        public Game GameInstance
        {
            get
            {
                return game;
            }
        }

        private SpriteBatch spriteBatch = null;
        public SpriteBatch SpriteBatchInstance
        {
            get
            {
                return spriteBatch;
            }
        }

        private ContentManager contentManager = null;
        public ContentManager ContentManagerInstance
        {
            get
            {
                return contentManager;
            }
        }

        //singleton data members
        private List<GameState> m_vStates;

        private StateManager()
        {
            m_vStates = new List<GameState>();
        }

        public void Initialize(Game game, SpriteBatch spriteBatch, ContentManager contentManager)
        {
            this.game = game;
            this.spriteBatch = spriteBatch;
            this.contentManager = contentManager;
        }

        public void EnterState(GameState newState)
        {
            if (null == newState)
                return;

            m_vStates.Add(newState);
            m_vStates.Last().EnterState();
        }

        public void ExitState()
        {
            if (0 == m_vStates.Count)
                return;

            m_vStates.Last().ExitState();
        }

        public bool RunState(GameTime gameTime)
        {
            // if no more states, GG
            if (0 == m_vStates.Count)
                return false;

            //clear screen
            spriteBatch.GraphicsDevice.Clear(Color.Black);

            Resolution.BeginDraw();

            spriteBatch.Begin(SpriteSortMode.Immediate,
                                BlendState.AlphaBlend,
                                SamplerState.LinearClamp,
                                null,
                                null,
                                null,
                                Resolution.getTransformationMatrix());

            bool bNews = true;
            foreach (GameState item in m_vStates)
            {
                bNews = item.Update(gameTime);

                if (false == bNews)
                {
                    spriteBatch.End();
                    return bNews;
                }

                item.Render(gameTime);
            }

            spriteBatch.End();
            return bNews;
        }

        public void ChangeState(GameState newState)
        {
            // clean up each state
            foreach (GameState item in m_vStates)
            {
                item.ExitState();
            }

            // remove all states from stack
            m_vStates.Clear();

            // start a new stack with the newState at the bottom
            m_vStates.Add(newState);
        }

        public void PushState(GameState newState)
        {
            // make sure newState is valid
            if (null == newState)
                return;

            // add the newState to the top of the stack
            m_vStates.Add(newState);

            // enter the new state
            m_vStates.Last().EnterState();
        }

        public void PopState()
        {
            // make sure there is a state to pop
            if (0 == m_vStates.Count)
                return;

            // clean up state's garbage
            m_vStates.Last().ExitState();

            // remove the state from stack
            m_vStates.Remove(m_vStates.Last());
        }

        public void PopState(int index)
        {
            // make sure there is a state to pop
            if (0 == m_vStates.Count || index > m_vStates.Count)
                return;

            // clean specified state's garbage
            m_vStates[index].ExitState();

            // remove state from stack
            m_vStates.RemoveAt(index);
        }

        public void ClearStates()
        {
            foreach (GameState item in m_vStates)
            {
                item.ExitState();
            }

            m_vStates.Clear();
        }
    }
}
