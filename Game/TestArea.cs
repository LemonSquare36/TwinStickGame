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
    class TestArea : AreaManager
    {
        private Character player;
        /*protected Vector2 position { get; set; }
        protected float scale { get; set; }
        protected Vector2 origin { get; set; }
        public Color color { get; set; }*/
        Texture2D Cube;

        public override void Initialize()
        {
            
        }

        public override void LoadContent(SpriteBatch spriteBatchmain)
        {
            spriteBatch = spriteBatchmain;

            MakeShapes();
            
            player.LoadContent(100,300);

            Cube = Main.GameContent.Load<Texture2D>("Sprites/WhiteCube");
        }

        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            player.RealPos();
            getKey();
            player.Rotate(12, Key,camera);
            player.MovePlayer(Key,camera);

        }

        public override void Draw()
        {
            player.Draw(spriteBatch);
            spriteBatch.Draw(Cube, player.getRealPos(0), Color.Blue);
            spriteBatch.Draw(Cube, player.getRealPos(1), Color.Blue);
            spriteBatch.Draw(Cube, player.getRealPos(2), Color.Blue);
            spriteBatch.Draw(Cube, player.getRealPos(3), Color.Blue);
            spriteBatch.Draw(Cube, player.getRealPos(4), Color.Blue);
        }

        private void MakeShapes()
        {
            RetrieveShapes();

            player = CreateCharacter("player");
        }
    }
}
