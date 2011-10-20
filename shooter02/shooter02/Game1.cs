using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using shooter02.Threading;
using shooter02.Managers;
using shooter02.GameStates;
using IndependentResolutionRendering;
using shooter02.Misc;

namespace shooter02
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        StateManager stateManager = StateManager.Instance;
        InputManager inputManager = InputManager.Instance;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Add your initialization logic here
            Resolution.Init(ref graphics);
            graphics.IsFullScreen = SaveInfo.Instance.Fullscreen;
            ObjectManager.CObjectManager.Instance.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            stateManager.Initialize(this, spriteBatch, this.Content);
            stateManager.PushState(GamePlayState.Instance);

            // load in game save
            SaveInfo.Instance.Load();

            // set resolution
            Vector2 saveGameRes = SaveInfo.Instance.ScreenResolution;

            Resolution.SetVirtualResolution(1280, 720);
            Resolution.SetResolution((int)saveGameRes.X, (int)saveGameRes.Y, false);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // recieve up-to-date keyboard/gamepad states
            inputManager.Update();

            // Allows the game to exit
            if ( inputManager.KeyPressed(Keys.Escape) || GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                SaveInfo.Instance.Save();
                this.Exit();
            }


            // run stateManager's states
            stateManager.RunState(gameTime);

            base.Update(gameTime);

            // store this frame's input
            inputManager.OnEndFrame();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //clear screen
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            // Add your drawing code here
            base.Draw(gameTime);

        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            stateManager.ClearStates();
            base.OnExiting(sender, args);
        }
    }
}
