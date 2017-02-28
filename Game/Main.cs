using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;
using System.IO;
using System.Collections;

namespace TwinStick
{
    public class Main : Game
    {
        Global global = new Global();
        GameState gameState = new GameState();

        SpriteBatch spriteBatch;
        GraphicsDeviceManager graphicsManager;

        //Allows other classes to load code from content manager - Convient
        private static ContentManager content;
        public static ContentManager GameContent
        {
            get { return content; }
            set { content = value; }
        }
        //
        private static GameWindow window;
        public static GameWindow gameWindow
        {
            get { return window; }
            set { window = value; }
        }
        private static GraphicsDevice graphics;
        public static GraphicsDevice graphicsDevice
        {
            get { return graphics; }
            set { graphics = value; }
        }


        //Constructor
        public Main()
        {
            Content.RootDirectory = "Content";
            content = Content;
            window = Window;
            IsMouseVisible = true;

            graphicsManager = new GraphicsDeviceManager(this);

        }
        //Utilizes the crash manager and Initializes GameState
        protected override void Initialize()
        {
            graphics = GraphicsDevice;
            spriteBatch = new SpriteBatch(GraphicsDevice);

            gameState.Initialize();
            base.Initialize();
        }

        //Loads Everything
        protected override void LoadContent()
        {
            gameState.LoadContent(spriteBatch, graphicsManager);
        }
        //Updates the Game
        protected override void Update(GameTime gameTime)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            gameState.Update();
            base.Update(gameTime);

        }

        //Draws the Game
        protected override void Draw(GameTime gameTime)
        {
            gameState.Draw();
            base.Draw(gameTime);
        }
    }
}
