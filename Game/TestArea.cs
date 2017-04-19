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
        /*protected Vector2 position { get; set; }
        protected float scale { get; set; }
        protected Vector2 origin { get; set; }
        public Color color { get; set; }*/
        Texture2D Cube;

        public override void Initialize()
        {
            
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
            Triangle1.RealPos();
            getKey();
            player.Rotate(12, Key,camera);
            player.MovePlayer(Key,camera);

            foreach(Bullets bullet in bulletsList)
            {
                bullet.RealPos();
                bullet.Draw(spriteBatch);
            }
            foreach (Bullets bullet in bulletsList)
            {
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

        protected void addnewbullet()
        {
            bulletsList.Add(CreateBullet("bullet", player.Placement, player.Placement));
        }

        public void ShootBullet(MouseState mouse, KeyboardState Key, Camera camera)
        {
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                addnewbullet();
            }
        }
    }
}
