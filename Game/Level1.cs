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
        private List<Polygons> polyList = new List<Polygons>();
        private Character player;
        MouseState mouse = new MouseState();
        Camera cam = new Camera();
        Polygons wall1, wall2, wall3, wall4;

        public override void Initialize()
        {
            TimerSetUp();
        }

        public override void LoadContent(SpriteBatch spriteBatchmain)
        {
            spriteBatch = spriteBatchmain;

            MakeShapes();
            #region polylistAdd
            polyList.Add(wall1);
            polyList.Add(wall2);
            polyList.Add(wall3);
            polyList.Add(wall4);
            #endregion

            player.LoadContent(100, 300);
            wall1.LoadContent(200, 300, "WorldSprites/Wall (1)");
            wall2.LoadContent(400, 200, "WorldSprites/Wall (1)");
            wall3.LoadContent(600, 300, "WorldSprites/Wall (1)");
            wall4.LoadContent(200, 100, "WorldSprites/Wall (1)");
        }

        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            player.RealPos();
            cam = camera;
            camera.Follow(new Vector2(-player.Placement.X + 372, -player.Placement.Y + 220));
            getKey();
            player.Rotate(Key, camera);
            mouse = Mouse.GetState();

            ShootBullet(mouse, cam, ref player.Placement, ref bulletsList);

            foreach (Polygons poly in polyList)
            {
                poly.RealPos();
                bool collide = Collision(player, poly);
                if (collide)
                {
                    player.Stop();
                }
            }

            foreach (Bullets bullet in bulletsList.ToList())
            {
                bullet.MoveBullet(camera);
                bullet.RealPos();
                foreach (Polygons poly in polyList)
                {
                    bool collide = Collision(bullet, poly);
                    if (collide)
                    {
                        bulletsList.Remove(bullet);
                    }
                }
            }
            player.MovePlayer(Key);
        }


        public override void Draw()
        {
            player.Draw(spriteBatch);

            foreach (Bullets bullet in bulletsList)
            {
                bullet.Draw(spriteBatch);
            }
            foreach (Polygons poly in polyList)
            {
                poly.Draw(spriteBatch);
            }
        }

        private void MakeShapes()
        {
            RetrieveShapes();

            player = CreateCharacter("player");
            wall1 = CreateShape("basewall");
            wall2 = CreateShape("basewall");
            wall3 = CreateShape("basewall");
            wall4 = CreateShape("basewall");
        }
    }
}
