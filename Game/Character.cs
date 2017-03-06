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

namespace TwinStick
{
    class Character : Polygons
    {
        //Constructor
        public Character(List<Vector2> numbers) : base(numbers)
        {
            rotation = 0;
            foreach (Vector2 num in numbers)
            {
                verticies.Add(num);
            }
        }

        //Loads the texture 2D's using image name
        public override void LoadContent(float X, float Y)
        {
            Placement.X = X;
            Placement.Y = Y;
            texture = Main.GameContent.Load<Texture2D>("Sprites/TestSprites/tempPlayerSprite");
        }
        //Draws the Images with current Texture
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Placement, null, null, verticies[0], rotation, new Vector2(1, 1), Color.White);
        }

        //Roatates the Shape
        public void Rotate(float rotate, KeyboardState keyState)
        {
            if(keyState.IsKeyDown(Keys.Q))
            {
                rotation += rotate;
            }
            else if (keyState.IsKeyDown(Keys.E))
            {
                rotation -= rotate;
            }
        }  
        public void MovePlayer(KeyboardState Key)
        {
            if (Key.IsKeyDown(Keys.K))
            {
                Placement.Y++;                
            }
            if (Key.IsKeyDown(Keys.I))
            {
                Placement.Y--;
            }
            if (Key.IsKeyDown(Keys.J))
            {
                Placement.X--;
            }
            if (Key.IsKeyDown(Keys.L))
            {
                Placement.X++;
            }
        }   
    }
}
