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

        public void Initialize()
        {
            
        }

        public void LoadContent()
        {
            MakeShapes();

            player.LoadContent(100,300);
        }

        public void Update()
        {
            player.Rotate(.02f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);
        }

        private void MakeShapes()
        {
            RetrieveShapes();

            player = CreateCharacter("player");
        }
    }
}
