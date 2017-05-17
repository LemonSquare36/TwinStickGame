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
    class AreaManager : ScreenManager
    {
        protected static Hashtable shapeVerts = new Hashtable();
        Timer bulletaddtime = new Timer();
        bool elapsed = true;
        bool canfire = true;
        bool enemyelapsed = true;


        MouseState oldMouse = new MouseState();

        

        //Gets the Hit boxes from Shape List or Enemy List
        protected void RetrieveShapes()
        {
            string Resource = "Shapes/shapeList.txt";
            StreamReader shapeConfig = new StreamReader(Path.Combine(Main.GameContent.RootDirectory, Resource));

            string line;
            string key = "";
            List<Vector2> verticies = new List<Vector2>();
            while ((line = shapeConfig.ReadLine()) != null)
            {
                try
                {
                    string[] VertCords = (line.Split(','));
                    float xVert = (float)Convert.ToDouble(VertCords[0]);
                    float yVert = (float)Convert.ToDouble(VertCords[1]);
                    Vector2 myVector2 = new Vector2(xVert, yVert);
                    verticies.Add(myVector2);

                }
                catch
                {

                    if (key != null)
                    {
                        shapeVerts[key] = verticies;
                        verticies = new List<Vector2>();
                    }
                    key = line;
                }
            }
            shapeConfig.Close();
        }


        //Uses the Positions from Shape list to make collision
        protected bool Collision(Polygons Shape, Polygons Shape2)
        {
            bool collision = true;
            bool inrange = false;
            bool notinrange = false;
            double Range = Shape.getRange() + Shape2.getRange();

            if (Math.Abs(Distance(Shape.getRealPos(0), Shape2.getRealPos(0))) < Range)
            {
                inrange = true;
            }

            if (inrange)
            {
                // Y is for the verticies one higher than i; I named it Y since it rhymes with i;
                int Y = 2;
                // Z is the same as Y but for Shape2; Named that since it is after Y;
                int Z = 2;

                for (int i = 1; i < Shape.getNumVerticies(); i++)
                {
                    if (Y == Shape.getNumVerticies())
                    {
                        Y = 1;
                    }
                    if (!Shape.Projection(Shape2, Shape.NormalVector(i, Y)))
                    {
                        collision = false;
                    }
                    Y++;
                }

                for (int i = 1; i < Shape2.getNumVerticies(); i++)
                {
                    if (Z == Shape2.getNumVerticies())
                    {
                        Z = 1;
                    }
                    if (!Shape2.Projection(Shape, Shape2.NormalVector(i, Z)))
                    {
                        collision = false;
                    }
                    Z++;
                }

                return collision;
            }
            return notinrange;
        }

        //distance calc
        protected double Distance(Vector2 point1, Vector2 point2)
        {
            double X = Math.Pow((point2.X - point1.X), 2);
            double Y = Math.Pow((point2.Y - point1.Y), 2);

            double unit = Math.Sqrt(X + Y);
            return unit;
        }

        #region Player Bullet Code
        //Add new bullet
        private void addnewbullet(Camera camera, Vector2 startpoint, ref List<Bullets> bulletList, string type)
        {
            float rotation;
            Vector2 worldPosition = Vector2.Zero;
            MouseState curMouse = Mouse.GetState();
            try
            {
                worldPosition.X = curMouse.X / (float)(Main.gameWindow.ClientBounds.Width / 1600f);
                worldPosition.Y = curMouse.Y / (float)(Main.gameWindow.ClientBounds.Height / 960f);
            }
            catch { }

            Vector2 mouseLoc = new Vector2(worldPosition.X, worldPosition.Y);
            GetMousePosWorld(camera, ref mouseLoc);

            Vector2 direction = mouseLoc - startpoint;
            rotation = (float)(Math.Atan2(direction.Y, direction.X)) + (float)Math.PI / 2;

            Bullets newBullet = CreateBullet("bullet", startpoint, mouseLoc, rotation, type);

            bulletList.Add(newBullet);
        }
        //get the pos in game for the mouse
        private void GetMousePosWorld(Camera camera, ref Vector2 mouseLoc)
        {
            float scaledMouseX = mouseLoc.X;
            float scaledMouseY = mouseLoc.Y;
            mouseLoc.X = scaledMouseX - camera.Position.X;
            mouseLoc.Y = scaledMouseY - camera.Position.Y;
            Debug.WriteLine("mouse1: " + mouseLoc.X + " " + mouseLoc.Y);
        }
        //add a bullet to the list
        protected void ShootBullet(MouseState mouse, Camera cam, Vector2 startpoint, ref List<Bullets> bulletList, string type, Character player)
        {
            if (!player.dead)
            {
                if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
                {
                    canfire = true;
                }
                else
                {
                    canfire = false;
                }

                if (elapsed == true && canfire == true)
                {
                    addnewbullet(cam, startpoint, ref bulletList, type);
                    elapsed = false;
                    bulletaddtime.Stop();
                    bulletaddtime.Start();
                }
                oldMouse = mouse;
            }
        }
        //Timer code
        protected void TimerSetUp()
        {
            bulletaddtime.Elapsed += BulletTimerElasped;
            bulletaddtime.Interval = 150;
        }
        //Elapsed functuon for timer
        private void BulletTimerElasped(object source, ElapsedEventArgs e)
        {
            elapsed = true;
        }
        #endregion

        #region Enemy Bullet Code
        private void AddNewEnemyBullet(Camera camera, Vector2 startpoint, ref List<Bullets> enemyBullets, string type, Vector2 shootat)
        {
            float rotation;
            
            Vector2 direction = shootat - startpoint;
            rotation = (float)(Math.Atan2(direction.Y, direction.X)) + (float)Math.PI / 2;

            Bullets newBullet = CreateBullet("bullet", startpoint, shootat, rotation, type);
            enemyBullets.Add(newBullet);
        }

        protected void EnemyShootBullet(Vector2 shootat, Camera cam, Vector2 startpoint, ref List<Bullets> bulletList, Enemy enemy,string type)
        {
            type = "Red";

            if (enemy.enemyelasped == true)
            {
                AddNewEnemyBullet(cam, startpoint, ref bulletList, type, shootat);
                enemy.enemyelasped = false;
                enemy.enemybulletaddtime.Stop();
                enemy.enemybulletaddtime.Start();
            }
        }

        #endregion
        //Creates the Shapes of Polygon Class
        protected Polygons CreateShape(string shapeName)
        {
            List<Vector2> NewList = (List<Vector2>)shapeVerts[shapeName];
            Polygons myPolygon = new Polygons(NewList);
            return myPolygon;
        }
        protected Character CreateCharacter(string shapeName)
        {
            List<Vector2> NewList = (List<Vector2>)shapeVerts[shapeName];
            Character myPolygon = new Character(NewList);
            return myPolygon;
        }
        protected Bullets CreateBullet(string shapeName, Vector2 pos, Vector2 mousePos, float rotation, string type)
        {
            List<Vector2> NewList = (List<Vector2>)shapeVerts[shapeName];
            Bullets bullet = new Bullets(NewList);
            bullet.RealPos();
            bullet.rotation = rotation;
            bullet.LoadContent(pos.X, pos.Y, type);
            bullet.SetVelocity(mousePos);
            return bullet;
        }
        protected Enemy CreateEnemy(string shapeName)
        {
            List<Vector2> NewList = (List<Vector2>)shapeVerts[shapeName];
            Enemy myPolygon = new Enemy(NewList);
            return myPolygon;
        }

        public event EventHandler changeScreen;
        protected void OnScreenChanged(object sender, EventArgs eventArgs)
        {
            changeScreen?.Invoke(this, EventArgs.Empty);
        }
    }
}
