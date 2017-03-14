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
    class ScreenManager
    {
        protected KeyboardState Key;
        protected SpriteBatch spriteBatch;
        protected string nextScreen;

        //Holds Initialize
        public virtual void Initialize()
        {

        }
        //Holds LoadContent and the font if called
        public virtual void LoadContent(SpriteBatch spriteBatchmain)
        {
            
        }
        //Holds Update
        public virtual void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {

        }
        //Holds Draw
        public virtual void Draw()
        {

        }
        public void getKey()
        {
            Key = Keyboard.GetState();
        }

        //Holds the Function
        public virtual void ButtonReset()
        {

        }
        //Gets the next screen
        public string getNextScreen()
        {
            return nextScreen;
        }
    }
}
