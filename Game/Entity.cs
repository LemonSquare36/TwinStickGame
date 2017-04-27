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
    class Entity : Polygons
    {
        protected int HP;

        public int getHP()
        {
            return HP;
        }
        public void removeHP(int damage)
        {
            HP -= damage;
        }
        public Entity(List<Vector2> numbers) : base(numbers)
        {

        }
    }
}
