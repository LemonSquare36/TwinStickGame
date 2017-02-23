﻿using System;
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
        static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera = new Camera();
        TestArea Test;

        Texture2D Cube;

        //Hashtable for storing the verticies
        protected static Hashtable shapeVerts = new Hashtable();

        Polygons Triangle1, Triangle2;

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

        //Constructor
        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = Content;
            window = Window;
            IsMouseVisible = true;
        }
        //Utilizes the crash manager and Initializes GameState
        protected override void Initialize()
        {
            Test = new TestArea();
            Test.Initialize();
            base.Initialize();
        }

        //Loads Everything
        protected override void LoadContent()
        {
            Cube = GameContent.Load<Texture2D>("Sprites/WhiteCube");

            MakeShapes();
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Test.LoadContent();

            Triangle1.LoadContent(100, 100);
            Triangle2.LoadContent(600, 100);
        }
        //Updates the Game
        protected override void Update(GameTime gameTime)
        {
            KeyboardState key = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            base.Update(gameTime);
            camera.Move(key);

            Test.Update();

            Triangle1.RealPos();
            Triangle2.RealPos();

            Triangle1.MoveShape(key);
            bool collide = Collision(Triangle1, Triangle2);
            if (collide)
            {
                Triangle1.Stop();

            }
            Debug.WriteLine(collide);
        }

        //Draws the Game
        protected override void Draw(GameTime gameTime)
        {
            var viewMatrix = camera.Transform(GraphicsDevice);

            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, null, null, null, null, viewMatrix * Matrix.CreateScale(1));
            Triangle1.Draw(spriteBatch);
            Triangle2.Draw(spriteBatch);

            spriteBatch.Draw(Cube, Triangle1.getRealPos(0), Color.White);
            spriteBatch.Draw(Cube, Triangle1.getRealPos(1), Color.Red);
            spriteBatch.Draw(Cube, Triangle1.getRealPos(2), Color.Blue);
            spriteBatch.Draw(Cube, Triangle1.getRealPos(3), Color.Gray);

            Test.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void MakeShapes()
        {
            RetrieveShapes();

            Triangle1 = CreateShape("triangle");
            Triangle2 = CreateShape("triangle");
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