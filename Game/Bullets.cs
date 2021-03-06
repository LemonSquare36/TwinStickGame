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

namespace TwinStick
{
    class Bullets : Polygons
    {
        protected Texture2D bullet;
        protected Vector2 velocity;
        protected bool isVisible;
        public string type;
        public int damage;
        

        public Bullets(List<Vector2> bullets) : base(bullets)
        {
            
        }

        public void Initialize()
        {

        }

        public void SetVelocity(Vector2 mousePos)
        {
            velocity.X = mousePos.X - Placement.X;
            velocity.Y = mousePos.Y - Placement.Y;
            velocity.Normalize();
            velocity *= 40;
        }
        //Loads the texture 2D's using image name
        public override void LoadContent(float X, float Y, string type)
        {
            Placement.X = X;
            Placement.Y = Y;
            if(type == "Red")
            {
                texture = Main.GameContent.Load<Texture2D>("Sprites/Bullets/Enemy Bullet");
            }
            if(type == "Blue")
            {
                texture = Main.GameContent.Load<Texture2D>("Sprites/Bullets/Player Bullet");
            }
            setrange();
        }

        //Draws the Images with current Texture
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Placement, null, null, verticies[0], rotation, new Vector2(1, 1), Color.White);
        }

        public void MoveBullet(Camera camera)
        {
            Placement += velocity;
        }
    }
}
