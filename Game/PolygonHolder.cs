using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TwinStick
{
    public class PolygonHolder
    {
        //Lets polygon have a draw with override so it can be used and overriden
        public virtual void Draw(SpriteBatch spritebatch)
        {

        }
        public virtual void LoadContent(float X, float Y)
        {
        }
    }
}
