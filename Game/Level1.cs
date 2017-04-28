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
        Polygons treeborderB, treeborderT, treeborderL, destroyedCabin, burnedRemains;

        public override void Initialize()
        {
            TimerSetUp();
        }

        public override void LoadContent(SpriteBatch spriteBatchmain)
        {
            spriteBatch = spriteBatchmain;

            MakeShapes();
            #region polylistAdd
            polyList.Add(treeborderB);
            polyList.Add(treeborderT);
            polyList.Add(treeborderL);
            polyList.Add(burnedRemains);
            polyList.Add(destroyedCabin);
            #endregion

            player.LoadContent(100, 300);
            treeborderB.LoadContent(-414, 1790, "WorldSprites/Treeborder bottom");
            treeborderT.LoadContent(-356, -1586, "WorldSprites/Treeborder top");
            treeborderL.LoadContent(-2000, 0, "WorldSprites/Treeborder left");
            destroyedCabin.LoadContent(-1050, -700, "WorldSprites/Destroyed Cabin");
            burnedRemains.LoadContent( 800, -600, "WorldSprites/Burned Remains");
        }

        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            player.RealPos();
            cam = camera;
            camera.Follow(new Vector2(-player.Placement.X, -player.Placement.Y));
            getKey();
            player.Rotate(Key, camera);
            mouse = Mouse.GetState();

            ShootBullet(mouse, cam, player.Placement, ref bulletsList);

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
            treeborderB = CreateShape("treeborderb");
            treeborderT = CreateShape("treebordert");
            treeborderL = CreateShape("treeborderl");
            destroyedCabin = CreateShape("destroyedcabin");
            burnedRemains = CreateShape("burnedremains");
        }
    }
}
