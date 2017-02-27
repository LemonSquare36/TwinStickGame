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
using System.IO;
using System.Collections;

namespace TwinStick
{
    class TestArea : TempCollisionHolder
    {
        private Character player;
        /*protected Vector2 position { get; set; }
        protected float scale { get; set; }
        protected Vector2 origin { get; set; }
        public Color color { get; set; }*/

        public void Initialize()
        {
            
        }

        public void LoadContent()
        {
            MakeShapes();
            
            player.LoadContent(100,300);
        }

        public void Update(KeyboardState Key)
        {
            player.Rotate(0.1f, Key);
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
