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
    class AnotherTestArea : AreaManager
    {
        Polygons Triangle1, Triangle2, Triangle3;
        Texture2D Cube;

        public override void Initialize()
        {

        }

        public override void LoadContent(SpriteBatch spriteBatchmain)
        {
            spriteBatch = spriteBatchmain;
            MakeShapes();

            Triangle1.LoadContent(100, 100);
            Triangle2.LoadContent(600, 100);

        }

        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            getKey();
            Triangle1.MoveShape(Key);
            Triangle1.RealPos();
            Triangle2.RealPos();


            bool collide = Collision(Triangle1, Triangle2);
            if (collide)
            {
                Triangle1.Stop();

            }
        }

        public override void Draw()
        {
            Triangle1.Draw(spriteBatch);
            Triangle2.Draw(spriteBatch);
        }

        private void MakeShapes()
        {
            RetrieveShapes();

            Triangle1 = CreateShape("triangle");
            Triangle2 = CreateShape("triangle");
            Triangle3 = CreateShape("triangle");
        }
    }
}
