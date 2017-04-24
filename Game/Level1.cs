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
    class Level1 : AreaManager
    {
        private List<Bullets> bulletsList = new List<Bullets>();
        private Character player;
        MouseState mouse = new MouseState();
        Timer bulletaddtime = new Timer();
        bool elapsed = true;
        Camera cam = new Camera();

        public override void Initialize()
        {
            bulletaddtime.Elapsed += BulletTimerElasped;
            bulletaddtime.Interval = 500;
        }

        public override void LoadContent(SpriteBatch spriteBatchmain)
        {
            spriteBatch = spriteBatchmain;

            MakeShapes();

            player.LoadContent(100, 300);

        }

        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            cam = camera;
            getKey();
            player.Rotate(12, Key, camera);
            player.MovePlayer(Key, camera);
            mouse = Mouse.GetState();
            ShootBullet(mouse);

            foreach (Bullets bullet in bulletsList.ToList())
            {
                bullet.MoveBullet(camera);
                bullet.RealPos();
            }
        }

        public override void Draw()
        {
            player.Draw(spriteBatch);

            foreach (Bullets bullet in bulletsList)
            {
                bullet.Draw(spriteBatch);
            }
        }

        private void MakeShapes()
        {
            RetrieveShapes();

            player = CreateCharacter("player");
        }

       

        private void BulletTimerElasped(object source, ElapsedEventArgs e)
        {
            elapsed = true;
        }
    }
}
