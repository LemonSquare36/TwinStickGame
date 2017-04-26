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
    class Character : Entity
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

        public void Initialize()
        {
            
        }

        //Loads the texture 2D's using image name
        public void LoadContent(float X, float Y)
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
        public void Rotate(KeyboardState keyState, Camera camera)
        {
            Vector2 worldPosition = Vector2.Zero;
            MouseState curMouse = Mouse.GetState();
            try
            {
                worldPosition.X = curMouse.X / (float)(Main.gameWindow.ClientBounds.Width / 1600.0);
                worldPosition.Y = curMouse.Y / (float)(Main.gameWindow.ClientBounds.Height / 960.0);
            }
            catch { }

            Vector2 mouseLoc = new Vector2(worldPosition.X, worldPosition.Y);
            GetMousePosWorld(camera, ref mouseLoc);
            Vector2 direction = mouseLoc - Placement;
            rotation = (float)(Math.Atan2(direction.Y, direction.X))+(float)Math.PI/2;
        }  
        private void GetMousePosWorld(Camera camera, ref Vector2 mouseLoc)
        {
            float scaledMouseX = mouseLoc.X;
            float scaledMouseY = mouseLoc.Y;
            mouseLoc.X = scaledMouseX - camera.Position.X;
            mouseLoc.Y = scaledMouseY - camera.Position.Y;
            Debug.WriteLine("mouse1: "+mouseLoc.X + " " + mouseLoc.Y);
        }
        public void MovePlayer(KeyboardState Key)
        {           
            Movement = Vector2.Zero;
            
            if (Key.IsKeyDown(Keys.S))
            {
                Movement = new Vector2(Movement.X, Movement.Y + 12f);
            }
            if (Key.IsKeyDown(Keys.W))
            {
                Movement = new Vector2(Movement.X, Movement.Y - 12f);
            }
            if (Key.IsKeyDown(Keys.A))
            {
                Movement = new Vector2(Movement.X - 12f, Movement.Y);
            }
            if (Key.IsKeyDown(Keys.D))
            {
                Movement = new Vector2(Movement.X + 12f, Movement.Y);
            }
            OldPosition = Placement;
            Placement += Movement;
        }
    }
}
