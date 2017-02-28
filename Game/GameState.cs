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

        Camera camera = new Camera();
        Vector3 screenScale = Vector3.Zero;
        Color color = Color.Blue;
        Color prevColor;

        SpriteBatch spriteBatch;
        GraphicsDeviceManager graphicsManager;

        TestArea Test;
        Polygons Triangle1, Triangle2, Triangle3;
        Texture2D Cube;

        int loadingInterval;

        ScreenManager CurrentScreen;

        //Hashtable for storing the verticies
        protected static Hashtable shapeVerts = new Hashtable();

        //Constructor
        public GameState()
        {
            CurrentScreen = new ScreenManager();
        }

        public void Initialize()
        {
            Test = new TestArea();
            Test.Initialize();
            CurrentScreen.Initialize();
        }

        //Loads the Content for The screens
        public void LoadContent(SpriteBatch spriteBatchMain, GraphicsDeviceManager graphicsManagerMain)
        {
            spriteBatch = spriteBatchMain;
            graphicsManager = graphicsManagerMain;

            CurrentScreen.LoadContent(spriteBatch);

            Cube = Main.GameContent.Load<Texture2D>("Sprites/WhiteCube");

            MakeShapes();

            Test.LoadContent();

            Triangle1.LoadContent(100, 100);
            Triangle2.LoadContent(600, 100);
        }

        //The update function for changing the screen and for using functions of the current screens
        public void Update()
        {
            KeyboardState key = Keyboard.GetState();
            camera.Move(key);

            Test.Update(key);

            Triangle1.MoveShape(key);
            Triangle1.RealPos();
            Triangle2.RealPos();


            bool collide = Collision(Triangle1, Triangle2);
            if (collide)
            {
                Triangle1.Stop();

            }
            Debug.WriteLine(collide);

            CurrentScreen.Update(camera, graphicsManager, Main.graphicsDevice);
        }

        //Draws the images and textures we use
        public void Draw()
        {
            var viewMatrix = camera.Transform(Main.graphicsDevice);

            Main.graphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, viewMatrix * Matrix.CreateScale(1));
            Triangle1.Draw(spriteBatch);
            Triangle2.Draw(spriteBatch);

            spriteBatch.Draw(Cube, Triangle1.getRealPos(0), Color.White);
            spriteBatch.Draw(Cube, Triangle1.getRealPos(1), Color.Red);
            spriteBatch.Draw(Cube, Triangle1.getRealPos(2), Color.Blue);
            spriteBatch.Draw(Cube, Triangle1.getRealPos(3), Color.Gray);

            Test.Draw(spriteBatch);

            spriteBatch.End();
        }

        private void MakeShapes()
        {
            RetrieveShapes();

            Triangle1 = CreateShape("triangle");
            Triangle2 = CreateShape("triangle");
            Triangle3 = CreateShape("triangle");
        }

        //Gets the Hit boxes from Shape List or Enemy List
        protected void RetrieveShapes()
        {
            string Resource = "Shapes/shapeList.txt";
            StreamReader shapeConfig = new StreamReader(Path.Combine(Main.GameContent.RootDirectory, Resource));

            string line;
            string key = "";
            List<Vector2> verticies = new List<Vector2>();
            while ((line = shapeConfig.ReadLine()) != null)
            {
                try
                {
                    string[] VertCords = (line.Split(','));
                    float xVert = (float)Convert.ToDouble(VertCords[0]);
                    float yVert = (float)Convert.ToDouble(VertCords[1]);
                    Vector2 myVector2 = new Vector2(xVert, yVert);
                    verticies.Add(myVector2);

                }
                catch
                {

                    if (key != null)
                    {
                        shapeVerts[key] = verticies;
                        verticies = new List<Vector2>();
                    }
                    key = line;
                }
            }
            shapeConfig.Close();
        }


        //Uses the Positions from Shape list to make collision
        protected bool Collision(Polygons Shape, Polygons Shape2)
        {
            bool collision = true;

            // Y is for the verticies one higher than i; I named it Y since it rhymes with i;
            int Y = 2;
            // Z is the same as Y but for Shape2; Named that since it is after Y;
            int Z = 2;

            for (int i = 1; i < Shape.getNumVerticies(); i++)
            {
                if (Y == Shape.getNumVerticies())
                {
                    Y = 1;
                }
                if (!Shape.Projection(Shape2, Shape.NormalVector(i, Y)))
                {
                    collision = false;
                }
                Y++;
            }

            for (int i = 1; i < Shape2.getNumVerticies(); i++)
            {
                if (Z == Shape2.getNumVerticies())
                {
                    Z = 1;
                }
                if (!Shape2.Projection(Shape, Shape2.NormalVector(i, Z)))
                {
                    collision = false;
                }
                Z++;
            }
            return collision;
        }

        //Creates the Shapes of Polygon Class
        protected Polygons CreateShape(string shapeName)
        {
            List<Vector2> NewList = (List<Vector2>)shapeVerts[shapeName];
            Polygons myPolygon = new Polygons(NewList);
            return myPolygon;
        }
    }
}
