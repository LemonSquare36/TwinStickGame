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
        private List<Enemy> enemyList = new List<Enemy>();
        private Character player;
        MouseState mouse = new MouseState();
        Camera cam = new Camera();
        Texture2D Cube;
        Enemy Bonzai;

        public override void Initialize()
        {
            TimerSetUp();
        }


        public override void LoadContent(SpriteBatch spriteBatchmain)
        {
            spriteBatch = spriteBatchmain;

            MakeShapes();
            #region Add Enemies to List
            enemyList.Add(Bonzai);
            #endregion

            #region LoadContents
            player.LoadContent(100,500);
            Triangle1.LoadContent(100, 100, "Triangle");
            Bonzai.LoadContent(150, 150, "Bonzai");

            #endregion


            Cube = Main.GameContent.Load<Texture2D>("Sprites/WhiteCube");
        }

        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            player.RealPos();
            cam = camera;
            camera.Follow(new Vector2 (-player.Placement.X, -player.Placement.Y));
            Triangle1.RealPos();
            getKey();
            player.Rotate(Key,camera);
            mouse = Mouse.GetState();
            ShootBullet(mouse, cam, player.getRealPos(1), ref bulletsList);

            foreach (Bullets bullet in bulletsList.ToList())
            {
                bullet.MoveBullet(camera);
                bullet.RealPos();
                bool collide = Collision(Triangle1, bullet);
                if (collide)
                {
                    bullet.Stop();
                    bulletsList.Remove(bullet);
                }
            }

            foreach (Enemy enemy in enemyList.ToList())
            {
                enemy.MoveEnemy(player.getRealPos(2));

                enemy.RealPos();
                bool collide = Collision(player, enemy);
                if (collide)
                {
                    player.removeHP(enemy.GetDamage());
                    enemy.Stop();
                }
                foreach (Bullets bullet in bulletsList.ToList())
                {
                    collide = Collision(enemy, bullet);
                    if (collide)
                    {
                        bulletsList.Remove(bullet);
                        enemy.removeHP(1);
                        if(enemy.getHP() <= 0)
                        {
                            enemyList.Remove(enemy);
                        }
                    }
                }

            } 
            player.MovePlayer(Key);
        }

        public override void Draw()
        {
            player.Draw(spriteBatch);
            player.DrawHud(spriteBatch);
            Triangle1.Draw(spriteBatch);

            foreach (Enemy enemy in enemyList)
            {
                enemy.Draw(spriteBatch);
            }

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
            Bonzai = CreateEnemy("bonzaienemy");
        }
    }
}
