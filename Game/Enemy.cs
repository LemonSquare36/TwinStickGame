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
    class Enemy : Entity
    {
        protected Vector2 velocity;
        public string enemyType;
        string aiType;
        bool agro;

        public Enemy(List<Vector2> numbers) : base(numbers)
        {

        }

        public void Initialize()
        {

        }

        public override void LoadContent(float X, float Y, string enemytype)
        {
            enemyType = enemytype;

            Placement.X = X;
            Placement.Y = Y;
            setrange();

            if(enemytype == "Bonzai")
            {
                texture = Main.GameContent.Load<Texture2D>("Sprites/Enemies/Enemy Claymore");
                aiType = "Stupid";
                HP = 4;
                Damage = 2;
            }
            if(enemytype == "Goon")
            {
                texture = Main.GameContent.Load<Texture2D>("Sprites/Enemies/Enemy Gun");
                aiType = "Ranged";
                HP = 2;
                Damage = 8;
            }
            if(enemytype == "Assassin")
            {
                texture = Main.GameContent.Load<Texture2D>("Sprites/Enemies/Enemy Knife");
                aiType = "Stupid";
                HP = 3;
                Damage = 8;
            }
            if(enemytype == "Angry Josh")
            {
                texture = Main.GameContent.Load<Texture2D>("Sprites/Enemies/Enemy Minigun");
                aiType = "Ranged";
                HP = 8;
                Damage = 6;
            }
            if(enemytype == "Rambo")
            {
                texture = Main.GameContent.Load<Texture2D>("Sprites/Enemies/Enemy Rifle");
                aiType = "Ranged";
                HP = 5;
                Damage = 10;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
                spriteBatch.Draw(texture, Placement, null, null, verticies[0], rotation, new Vector2(1, 1), Color.White);
        }

        public void MoveEnemy(Vector2 placement)
        {
            if(aiType == "Stupid")
            {
                MoveEnemyPlacement(placement);
                OldPosition = Placement;
                Placement += velocity;
            }
            if(aiType == "Ranged")
            {
                MoveEnemyPlacement(placement);
                OldPosition = Placement;
                Placement += velocity;
            }
            Vector2 direction =  placement - Placement;
            rotation = (float)(Math.Atan2(direction.Y, direction.X)) + (float)Math.PI / 2;
        }

        private void MoveEnemyPlacement(Vector2 placement)
        {
            velocity.X = placement.X - Placement.X;
            velocity.Y = placement.Y - Placement.Y;
            velocity.Normalize();
            velocity *= 10;
        }

        public void GetAI(string aitype)
        {
            aitype = aiType;
            if(aitype == "Stupid")
            {

            }

            if(aitype == "Ranged")
            {

            }
        }

        //Fix rotation code for enemy little by little
        /*public void RotateEnemy(KeyboardState keyState, Camera camera)
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
            GetPlayerPosWorld(camera, ref mouseLoc);
            Vector2 direction = mouseLoc - Placement;
            rotation = (float)(Math.Atan2(direction.Y, direction.X)) + (float)Math.PI / 2;
        }
        private void GetPlayerPosWorld(Camera camera, ref Vector2 mouseLoc)
        {
            float scaledMouseX = Placement.X;
            float scaledMouseY = Placement.Y;
            Placement.X = scaledMouseX - camera.Position.X;
            Placement.Y = scaledMouseY - camera.Position.Y;
            Debug.WriteLine("mouse1: " + mouseLoc.X + " " + mouseLoc.Y);
        }*/
    }
}
