﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace TwinStick
{
    public class Polygons : PolygonHolder
    {

        // declaring texture 2D's
        protected Texture2D texture;


        protected float rotation;
        private List<Vector2> realPos = new List<Vector2>();
        public Vector2 Movement = Vector2.Zero;
        protected Vector2 OldPosition = new Vector2();
        public Vector2 Placement;


        //Holds Shapes Verticies
        protected List<Vector2> verticies = new List<Vector2>();

        public Polygons(List<Vector2> numbers)
        {
            rotation = 0;
            foreach (Vector2 num in numbers)
            {
                verticies.Add(num);
            }
        }

        //Get the RealPosition
        public Vector2 getRealPos(int Index)
        {
            return realPos[Index];
        }
        //Gets the list the verticies
        public List<Vector2> getVerticiesList()
        {
            return verticies;
        }
        //Gets the verticie not in its real position
        public Vector2 getVerticies(int vertNumber)
        {
            return verticies[vertNumber];
        }
        //Gets how many verticies there are 
        public int getNumVerticies()
        {
            return verticies.Count;
        }
        public List<Vector2> getRealPosList()
        {
            return realPos;
        }

        //Loads the texture 2D's using image name
        public override void LoadContent(float X, float Y)
        {
            Placement.X = X;
            Placement.Y = Y;
            texture = Main.GameContent.Load<Texture2D>("Sprites/Triangle");
        }
        //Draws the Images with current Texture
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Placement, null, null, verticies[0], rotation, new Vector2(1, 1), Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(texture, Placement, null, null, verticies[0], rotation, new Vector2(1, 1), color);
        }

        //Roatates the Shape
        public void Rotate(float rotate)
        {
            rotation += rotate;
        }
        //Move shape with arrow keys
        public void MoveShape(KeyboardState Key)
        {
            Movement = Vector2.Zero;

            if (Key.IsKeyDown(Keys.D))
            {
                Movement = new Vector2(Movement.X + 2f, Movement.Y);
            }
            if (Key.IsKeyDown(Keys.S))
            {
                Movement = new Vector2(Movement.X, Movement.Y + 1f);
            }
            if (Key.IsKeyDown(Keys.A))
            {
                Movement = new Vector2(Movement.X - 2f, Movement.Y);
            }
            if (Key.IsKeyDown(Keys.W))
            {
                Movement = new Vector2(Movement.X, Movement.Y - 1f);
            }
            OldPosition = Placement;
            Placement += Movement;
        }

        // returns the Normal Vector of a Line
        public Vector2 NormalVector(int Vert1, int Vert2)
        {
            return new Vector2(getRealPos(Vert1).Y - getRealPos(Vert2).Y, -(getRealPos(Vert1).X - getRealPos(Vert2).X));
        }

        //Project the shape along its normals to check for gaps (Collision Detection)
        public bool Projection(Polygons Shape, Vector2 P)
        {
            bool value = true;
            double minGap = 1;
            for (int X = 1; X < Shape.getNumVerticies(); X++)
            {
                for (int Y = 1; Y < getNumVerticies(); Y++)
                {

                    Vector2 C = new Vector2();
                    Vector2 B = new Vector2();
                    Vector2 A = new Vector2();
                    float DPa, DPb, DPc;
                    double gap;


                    C = new Vector2((getRealPos(0).X - Shape.getRealPos(0).X), (getRealPos(0).Y - Shape.getRealPos(0).Y));
                    A = new Vector2((getRealPos(0).X - getRealPos(Y).X), (getRealPos(0).Y - getRealPos(Y).Y));
                    B = new Vector2((Shape.getRealPos(0).X - Shape.getRealPos(X).X), (Shape.getRealPos(0).Y - Shape.getRealPos(X).Y));


                    P.Normalize();

                    DPa = Vector2.Dot(A, P);
                    DPb = Vector2.Dot(B, P);
                    DPc = Vector2.Dot(C, P);
                    gap = DPc - DPa + DPb;
                    if (gap < minGap)
                    {
                        minGap = gap;
                    }
                }
            }

            if (minGap <= 0)
            {
                value = true;
            }
            else if (minGap > 0)
            {
                value = false;
            }
            else
            {
                value = false;
            }
            return value;
        }
        //Find the realPos of the shapes using the images verticies
        public void RealPos()
        {
            Vector2 Pos, vertTemp;
            float theta = 0;
            float H, X, Y;
            List<Vector2> realPosTemp = new List<Vector2>();
            foreach (Vector2 verts in verticies)
            {
                if (verts == getVerticies(0))
                {
                    Pos.X = verts.X + Placement.X;
                    Pos.Y = verts.Y + Placement.Y;
                    realPosTemp.Add(Pos);
                    continue;
                }

                //if (vertTemp.X == verticies[0].X)
                //{
                //    vertTemp.X--;
                //}

                vertTemp.X = verts.X - getVerticies(0).X;
                vertTemp.Y = verts.Y - getVerticies(0).Y;

                theta = (float)Math.Atan(vertTemp.Y / vertTemp.X);
                H = (float)(vertTemp.X / Math.Cos(theta));
                X = (float)(H * Math.Cos(theta + rotation));
                Y = (float)(H * Math.Sin(theta + rotation));
                Pos = new Vector2(X + Placement.X, Y + Placement.Y);

                realPosTemp.Add(Pos);
            }
            realPos = realPosTemp;
        }

        public void Stop()
        {
            Placement -= Movement;
        }
    }
}