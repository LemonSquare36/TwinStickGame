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
    class Enemy : Entity
    {
        protected Vector2 velocity;
        public string enemyType;
        public string aiType;
        public int enemyInterval;
        int enemySpeed;
        public bool enemyelasped = false;

        public Timer enemybulletaddtime = new Timer(150);

        public Enemy(List<Vector2> numbers) : base(numbers)
        {

        }
        public void enemyTimerSetUp()
        {
            enemybulletaddtime.Elapsed += enemyBulletTimerElasped;
            enemybulletaddtime.Interval = enemyInterval;
            enemybulletaddtime.Start();
        }

        public void Initialize()
        {

        }

        public void enemyBulletTimerElasped(object source, ElapsedEventArgs e)
        {
            enemyelasped = true;
        }
        public override void LoadContent(float X, float Y, string enemytype)
        {
            enemyType = enemytype;
            enemySpeed = 6;

            Placement.X = X;
            Placement.Y = Y;
            setrange();

            if(enemytype == "Bonzai")
            {
                texture = Main.GameContent.Load<Texture2D>("Sprites/Enemies/Enemy Claymore");
                aiType = "Stupid";
                HP = 5;
                Damage = 2;
                enemySpeed = 7;
            }
            if(enemytype == "Goon")
            {
                texture = Main.GameContent.Load<Texture2D>("Sprites/Enemies/Enemy Gun");
                aiType = "Ranged";
                HP = 3;
                Damage = 8;
                enemyInterval = 500;
                enemySpeed = 7;
            }
            if(enemytype == "Assassin")
            {
                texture = Main.GameContent.Load<Texture2D>("Sprites/Enemies/Enemy Knife");
                aiType = "Stupid";
                HP = 3;
                Damage = 3;
                enemySpeed = 10;
            }
            if(enemytype == "Angry Josh")
            {
                texture = Main.GameContent.Load<Texture2D>("Sprites/Enemies/Enemy Minigun");
                aiType = "Ranged";
                enemyInterval = 50;
                HP = 12;
                Damage = 6;
                enemySpeed = 6;
            }
            if(enemytype == "Rambo")
            {
                texture = Main.GameContent.Load<Texture2D>("Sprites/Enemies/Enemy Rifle");
                aiType = "Ranged";
                HP = 5;
                enemyInterval = 150;
                Damage = 10;
                enemySpeed = 10;
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
        public void Retreat(Vector2 placement)
        {
            MoveEnemyPlacement(placement);
            OldPosition = Placement;
            Placement -= velocity*1.75f;
        }

        private void MoveEnemyPlacement(Vector2 placement)
        {
            velocity.X = placement.X - Placement.X;
            velocity.Y = placement.Y - Placement.Y;
            velocity.Normalize();
            velocity *= enemySpeed;
        }
        public int GetEnemyInterval()
        {
            return enemyInterval;
        }
    }
}
