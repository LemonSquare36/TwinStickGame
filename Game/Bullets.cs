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
    class Bullets : Polygons
    {
        public Bullets(List<Vector2> numbers) : base(numbers)
        {
            /*rotation = 0;
            foreach (Vector2 num in numbers)
            {
                verticies.Add(num);
            }*/
        }

        public void Initialize()
        {

        }

        //Loads the texture 2D's using image name
        public override void LoadContent(float X, float Y)
        {
            Placement.X = X;
            Placement.Y = Y;
            texture = Main.GameContent.Load<Texture2D>("Sprites/TestSprites/Bullet");
        }

        //Draws the Images with current Texture
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Placement, null, null, verticies[0], rotation, new Vector2(1, 1), Color.White);
        }

        //Roatates the Shape
        public void Rotate(float rotate, KeyboardState keyState, Camera camera)
        {
            /*MouseState curMouse = Mouse.GetState();
            Vector2 mouseLoc = new Vector2(curMouse.X, curMouse.Y);
            GetMousePosWorld(camera, ref mouseLoc);
            Debug.WriteLine("mouse1: " + mouseLoc.X + " " + mouseLoc.Y);
            Vector2 direction = mouseLoc - Placement;
            rotation = (float)(Math.Atan2(direction.Y, direction.X)) + (float)Math.PI / 2;*/
        }

        private void GetMousePosWorld(Camera camera, ref Vector2 mouseLoc)
        {
            float scaledMouseX = mouseLoc.X * 2;
            float scaledMouseY = mouseLoc.Y * 2;
            mouseLoc.X = scaledMouseX - camera.Position.X;
            mouseLoc.Y = scaledMouseY - camera.Position.Y;
            Debug.WriteLine("mouse1: " + mouseLoc.X + " " + mouseLoc.Y);
        }

        public void ShootBullet(KeyboardState Key, Camera camera)
        {
            Movement = Vector2.Zero;
        }
    }
}
