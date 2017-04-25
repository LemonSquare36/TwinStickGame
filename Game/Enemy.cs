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

        public Enemy(List<Vector2> numbers) : base(numbers)
        {

        }

        public void Initialize()
        {

        }

        public void LoadContent(float X, float Y)
        {
            texture = Main.GameContent.Load<Texture2D>("Sprites/Enemies/Enemy Claymore");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Placement, null, null, verticies[0], rotation, new Vector2(1, 1), Color.White);
        }

        public void MoveEnemy()
        {
            Placement += velocity;
        }

        public void MoveEnemyPlacement(Vector2 placement)
        {
            velocity.X = placement.X - Placement.X;
            velocity.Y = placement.Y - Placement.Y;
            velocity.Normalize();
            velocity *= 40;
        }
    }
}
