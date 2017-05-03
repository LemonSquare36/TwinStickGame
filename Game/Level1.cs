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
    class Level1 : AreaManager
    {
        private List<Bullets> bulletsList = new List<Bullets>();
        private List<Polygons> polyList = new List<Polygons>();
        private List<Enemy> enemyList = new List<Enemy>();
        private Character player;
        MouseState mouse = new MouseState();
        Camera cam = new Camera();
        Polygons treeborderB, treeborderB2, treeborderB3, treeborderT, treeborderL, treeborderL2, treeborderR, treeborderR2, destroyedCabin, burnedRemains, wallTop, wallBottom, well, tavern1, tavern2, tower;
        Polygons tree1, tree2, tree3, tree4, tree5, tree6, tree7, tree8, tree9, tree10, tree11, tree12, stump1, stump2, stump3, stump4, stump5, stump6, stump7, stump8, stump9, stump10, stump11;
        Enemy bonzia1, bonzia2, bonzia3, bonzia4;
        Texture2D singlebrush;

        public override void Initialize()
        {
            TimerSetUp();
        }

        public override void LoadContent(SpriteBatch spriteBatchmain)
        {
            spriteBatch = spriteBatchmain;

            MakeShapes();
            #region polylistAdd
            polyList.Add(wallTop);
            polyList.Add(wallBottom);
            polyList.Add(tower);
            polyList.Add(treeborderB);
            polyList.Add(treeborderB2);
            polyList.Add(treeborderB3);
            polyList.Add(treeborderT);
            polyList.Add(treeborderL);
            polyList.Add(treeborderL2);
            polyList.Add(treeborderR);
            polyList.Add(treeborderR2);
            polyList.Add(burnedRemains);
            polyList.Add(destroyedCabin);
            polyList.Add(well);
            polyList.Add(tavern1);
            polyList.Add(tavern2);
            polyList.Add(tree1);
            polyList.Add(tree2);
            polyList.Add(tree3);
            polyList.Add(tree4);
            polyList.Add(tree5);
            polyList.Add(tree6);
            polyList.Add(tree7);
            polyList.Add(tree8);
            polyList.Add(tree9);
            polyList.Add(tree10);
            polyList.Add(tree11);
            polyList.Add(tree12);
            polyList.Add(stump1);
            polyList.Add(stump2);
            polyList.Add(stump3);
            polyList.Add(stump4);
            polyList.Add(stump5);
            polyList.Add(stump6);
            polyList.Add(stump7);
            polyList.Add(stump8);
            polyList.Add(stump9);
            polyList.Add(stump10);
            polyList.Add(stump11);

            #endregion
            #region enemyListAdd
            enemyList.Add(bonzia1);
            enemyList.Add(bonzia2);
            enemyList.Add(bonzia3);
            enemyList.Add(bonzia4);
            #endregion

            player.LoadContent(100, 300);
            wallTop.LoadContent(1375, -710, "WorldSprites/Wall Top");
            wallBottom.LoadContent(1375, 1100, "WorldSprites/wall bottom");
            tower.LoadContent(2100, 1010, "WorldSprites/destroyedtower");
            treeborderB.LoadContent(-414, 1790, "WorldSprites/Treeborder bottom");
            treeborderB2.LoadContent(-414, 5100, "WorldSprites/Treeborder bottom");
            treeborderB3.LoadContent(3000, 5100, "WorldSprites/Treeborder bottom");
            treeborderT.LoadContent(-356, -1586, "WorldSprites/Treeborder top");
            treeborderL.LoadContent(-2000, 0, "WorldSprites/Treeborder left");
            treeborderL2.LoadContent(-2030, 3350, "WorldSprites/Treeborder left");
            treeborderR.LoadContent(5030, 0, "WorldSprites/Treeborder Right");
            treeborderR2.LoadContent(5030, 3350, "WorldSprites/Treeborder Right");
            destroyedCabin.LoadContent(-1150, -700, "WorldSprites/Destroyed Cabin");
            burnedRemains.LoadContent(800, -600, "WorldSprites/Burned Remains");
            well.LoadContent(-800, -200, "WorldSprites/well");
            tavern1.LoadContent(-1100, 200, "WorldSprites/Tavern1");
            tavern2.LoadContent(-866, 430, "WorldSprites/Tavern2");
            tree1.LoadContent(790, -200, "WorldSprites/Tree");
            tree2.LoadContent(850, 200, "WorldSprites/Tree");
            tree3.LoadContent(-300, 0, "WorldSprites/Tree");
            tree4.LoadContent(500, 800, "WorldSprites/Tree");
            tree5.LoadContent(-500, 1150, "WorldSprites/Tree");
            tree6.LoadContent(-1500, 1300, "WorldSprites/Tree");
            tree7.LoadContent(-1050, 550, "WorldSprites/Tree");
            tree8.LoadContent(-1150, 950, "WorldSprites/Tree");
            tree9.LoadContent(875, 1200, "WorldSprites/Tree");
            tree10.LoadContent(2290, - 794, "WorldSprites/Tree");
            tree11.LoadContent(3200, -1170, "WorldSprites/Tree");
            tree12.LoadContent(3750, -70, "WorldSprites/Tree");
            stump1.LoadContent(1900, 1500, "WorldSprites/stump");
            stump2.LoadContent(1200, 2400, "WorldSprites/stump");
            stump3.LoadContent(3000, 1000, "WorldSprites/stump");
            stump4.LoadContent(2070, -84, "WorldSprites/stump");
            stump5.LoadContent(2950, -600, "WorldSprites/stump");
            stump6.LoadContent(3542, 1550, "WorldSprites/stump");
            stump7.LoadContent(4050, 732, "WorldSprites/stump");
            stump8.LoadContent(4250, -874, "WorldSprites/stump");
            stump9.LoadContent(3950, 2290, "WorldSprites/stump");
            stump10.LoadContent(4400, 1900, "WorldSprites/stump");
            stump11.LoadContent(2770, 2550, "WorldSprites/stump");

            bonzia1.LoadContent(500, -100, "Bonzai");
            bonzia2.LoadContent(500, 100, "Bonzai");
            bonzia3.LoadContent(100, 100, "Bonzai");
            bonzia4.LoadContent(100, -100, "Bonzai");

            singlebrush = Main.GameContent.Load<Texture2D>("Sprites/WorldSprites/Extended brush");
        }

        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            player.RealPos();
            cam = camera;
            camera.Follow(new Vector2(-player.Placement.X, -player.Placement.Y));
            getKey();
            player.Rotate(Key, camera);
            mouse = Mouse.GetState();

            ShootBullet(mouse, cam, player.Placement, ref bulletsList, "Blue");

            foreach (Polygons poly in polyList)
            {
                poly.RealPos();
                bool collide = Collision(player, poly);
                if (collide)
                {
                    player.Stop();
                }
                foreach (Enemy enemy in enemyList)
                {
                    enemy.RealPos();
                    collide = Collision(enemy, poly);
                    if (collide)
                    {
                        enemy.Stop();
                    }
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
                foreach (Enemy enemy in enemyList.ToList())
                {
                    bool collide = Collision(enemy, bullet);
                    if (collide)
                    {
                        bulletsList.Remove(bullet);
                        enemy.removeHP(1);
                        if (enemy.getHP() <= 0)
                        {
                            enemyList.Remove(enemy);
                        }
                    }
                }
            }

            foreach (Enemy enemy in enemyList.ToList())
            {
                if (Distance(enemy.Placement, player.Placement) < 800)
                {
                    enemy.MoveEnemy(player.getRealPos(2));
                }
                bool collide = Collision(enemy, player);
                if(collide)
                {
                    player.removeHP(enemy.GetDamage());
                    enemy.Stop();
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
            foreach (Enemy enemy in enemyList.ToList())
            {
                enemy.Draw(spriteBatch);           
            }
            player.DrawHud(spriteBatch);
            spriteBatch.Draw(singlebrush, new Vector2(1150, 1700), Color.White);
            spriteBatch.Draw(singlebrush, new Vector2(1150, 4910), Color.White);
        }

        private void MakeShapes()
        {
            RetrieveShapes();

            player = CreateCharacter("player");
            wallTop = CreateShape("walltop");
            wallBottom = CreateShape("wallbottom");
            tower = CreateShape("tower");
            treeborderB = CreateShape("treeborderb");
            treeborderB2 = CreateShape("treeborderb");
            treeborderB3 = CreateShape("treeborderb");
            treeborderT = CreateShape("treebordert");
            treeborderL = CreateShape("treeborderl");
            treeborderL2 = CreateShape("treeborderl");
            treeborderR = CreateShape("treeborderr");
            treeborderR2 = CreateShape("treeborderr");
            destroyedCabin = CreateShape("destroyedcabin");
            burnedRemains = CreateShape("burnedremains");
            well = CreateShape("well");
            tavern1 = CreateShape("tavern1");
            tavern2 = CreateShape("tavern2");
            tree1 = CreateShape("tree");
            tree2 = CreateShape("tree");
            tree3 = CreateShape("tree");
            tree4 = CreateShape("tree");
            tree5 = CreateShape("tree");
            tree6 = CreateShape("tree");
            tree7 = CreateShape("tree");
            tree8 = CreateShape("tree");
            tree9 = CreateShape("tree");
            tree10 = CreateShape("tree");
            tree11 = CreateShape("tree");
            tree12 = CreateShape("tree");
            stump1 = CreateShape("stump");
            stump2 = CreateShape("stump");
            stump3 = CreateShape("stump");
            stump4 = CreateShape("stump");
            stump5 = CreateShape("stump");
            stump6 = CreateShape("stump");
            stump7 = CreateShape("stump");
            stump8 = CreateShape("stump");
            stump9 = CreateShape("stump");
            stump10 = CreateShape("stump");
            stump11 = CreateShape("stump");
            bonzia1 = CreateEnemy("bonzaienemy");
            bonzia2 = CreateEnemy("bonzaienemy");
            bonzia3 = CreateEnemy("bonzaienemy");
            bonzia4 = CreateEnemy("bonzaienemy");
        }
    }
}
