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
    class GameState
    {
        public enum gameState { Playing, Loading, Puased }

        protected static Hashtable tempshapeVerts = new Hashtable();

        Camera camera = new Camera();
        Vector3 screenScale = Vector3.Zero;
        Color color = Color.Blue;
        Color prevColor;

        SpriteBatch spriteBatch;
        GraphicsDeviceManager graphicsManager;

        int loadingInterval;

        #region Declare the Screens
        ScreenManager CurrentScreen;

        //areas
        TestArea Test;
        AnotherTestArea Atest;
        //Menus
        MainMenu MainMenu;
        #endregion

        //Constructor
        public GameState()
        {
            #region Initialize the Screens
            //areas
            Test = new TestArea();
            Atest = new AnotherTestArea();
            //Menus
            MainMenu = new MainMenu();
            #endregion

            MainMenu.ChangeScreen += HandleScreenChanged;
        }

        public void Initialize()
        {

            if (CurrentScreen == null)
            {
                CurrentScreen = MainMenu;
            }
            CurrentScreen.Initialize();
        }

        //Loads the Content for The screens
        public void LoadContent(SpriteBatch spriteBatchMain, GraphicsDeviceManager graphicsManagerMain)
        {
            spriteBatch = spriteBatchMain;
            graphicsManager = graphicsManagerMain;

            CurrentScreen.LoadContent(spriteBatch);

        }

        //The update function for changing the screen and for using functions of the current screens
        public void Update()
        {
            KeyboardState key = Keyboard.GetState();
            camera.Move(key);
            if (key.IsKeyDown(Keys.Q))
            {
                if (CurrentScreen == Test)
                    CurrentScreen = Atest;
                else if (CurrentScreen == Atest)
                    CurrentScreen = Test;

                Initialize();
                LoadContent(spriteBatch, graphicsManager);
            }

            CurrentScreen.Update(camera, graphicsManager);
        }

        //Draws the images and textures we use
        public void Draw()
        {
            var viewMatrix = camera.Transform(Main.graphicsDevice);

            Main.graphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, viewMatrix * Matrix.CreateScale(1));
            CurrentScreen.Draw();

            spriteBatch.End();
        }

        //The Event that Changes the Screens
        public void HandleScreenChanged(object sender, EventArgs eventArgs)
        {
            bool Load = true;
            //Next Screen is Based off the buttons Name (not garenteed to even load a new screen)
            switch (CurrentScreen.getNextScreen())
            {
                case "Play":
                    CurrentScreen = Test;
                    break;

                case "Exit":
                    Environment.Exit(0);
                    break;

                case "FullScreen":
                    camera.ChangeScreenSize(graphicsManager);
                    Load = false;
                    break;

                default:
                    Load = false;
                    break;
            }
            //Resets the button on the screen
            CurrentScreen.ButtonReset();
            //Loads if a new screen is activated
            if (Load)
            {
                Initialize();
                LoadContent(spriteBatch, graphicsManager);
            }
        }
    }
}
