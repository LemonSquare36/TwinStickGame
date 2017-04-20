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
    class TestArea : AreaManager
    {
        Polygons Triangle1;
        private List<Bullets> bulletsList = new List<Bullets>();
        private Character player;
        MouseState mouse = new MouseState();
        Timer bulletaddtime = new Timer();
        bool elapsed = true;
        Camera cam = new Camera();
        /*protected Vector2 position { get; set; }
        protected float scale { get; set; }
        protected Vector2 origin { get; set; }
        public Color color { get; set; }*/
        Texture2D Cube;

        public override void Initialize()
        {
            bulletaddtime.Elapsed += BulletTimerElasped;
            bulletaddtime.Interval = 500;
        }

        public override void LoadContent(SpriteBatch spriteBatchmain)
        {
            spriteBatch = spriteBatchmain;

            MakeShapes();
            
            player.LoadContent(100,300);
            Triangle1.LoadContent(100, 100);
            

            Cube = Main.GameContent.Load<Texture2D>("Sprites/WhiteCube");
        }

        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            cam = camera;
            Triangle1.RealPos();
            getKey();
            player.Rotate(12, Key,camera);
            player.MovePlayer(Key,camera);
            mouse = Mouse.GetState();
            ShootBullet(mouse);

            foreach (Bullets bullet in bulletsList.ToList())
            {
                bullet.MoveBullet(camera);
                bullet.RealPos();
                bool collide = Collision(bullet, Triangle1);
                if (collide)
                {
                    bullet.Stop();
                    bulletsList.Remove(bullet);
                }
            }
        }

        public override void Draw()
        {
            player.Draw(spriteBatch);
            Triangle1.Draw(spriteBatch);

            foreach (Bullets bullet in bulletsList)
            {
                bullet.Draw(spriteBatch);
            }
        }

        private void MakeShapes()
        {
            RetrieveShapes();

            player = CreateCharacter("player");
            Triangle1 = CreateShape("triangle");
        }

        protected void addnewbullet(Camera camera)
        {
            MouseState curMouse = Mouse.GetState();
            Vector2 mouseLoc = new Vector2(curMouse.X, curMouse.Y);
            GetMousePosWorld(camera, ref mouseLoc);
            Bullets newBullet = CreateBullet("bullet", player.Placement, mouseLoc);
     
            bulletsList.Add(newBullet);
        }
        public void GetMousePosWorld(Camera camera, ref Vector2 mouseLoc)
        {
            float scaledMouseX = mouseLoc.X * 2;
            float scaledMouseY = mouseLoc.Y * 2;
            mouseLoc.X = scaledMouseX - camera.Position.X;
            mouseLoc.Y = scaledMouseY - camera.Position.Y;
            Debug.WriteLine("mouse1: " + mouseLoc.X + " " + mouseLoc.Y);
        }
        public void ShootBullet(MouseState mouse)
        {
            if (mouse.LeftButton == ButtonState.Pressed && elapsed == true)
            {
                addnewbullet(cam);
                elapsed = false;
                bulletaddtime.Stop();
                bulletaddtime.Start();
            }
        }
        private void BulletTimerElasped(object source, ElapsedEventArgs e)
        {
            elapsed = true;
        }
        
    }
}
