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
        public Enemy(List<Vector2> numbers) : base(numbers)
        {

        }

        public void Initialize()
        {

        }

        public override void LoadContent(float X, float Y)
        {
            texture = Main.GameContent.Load<Texture2D>("Sprites/TestSprites/tempPlayerSprite");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Placement, null, null, verticies[0], rotation, new Vector2(1, 1), Color.White);
        }

        public void MoveEnemy()
        {

        }
    }
}
