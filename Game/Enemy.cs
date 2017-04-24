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
            base.LoadContent(X, Y);    
        }
    }
}
