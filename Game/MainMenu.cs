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
    class MainMenu : MenuManager
    {
        Button Play;
        Texture2D PlayUnpressed, PlayPressed;

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent(SpriteBatch spriteBatchmain)
        {
            spriteBatch = spriteBatchmain;
            PlayUnpressed = Main.GameContent.Load<Texture2D>("Buttons/Play");
            PlayPressed = Main.GameContent.Load<Texture2D>("Buttons/PlayPressed");

            Play = new Button(new Vector2(300, 200), 600, 220, PlayUnpressed, PlayPressed, "Play");

        }

        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            //Get current mouse state
            Vector2 worldPosition = MousePos();

            Play.Update(mouse, worldPosition);
            Debug.WriteLine(worldPosition);
            Debug.WriteLine(mouse.Position);
            Play.ButtonClicked += ButtonClicked;
        }

        public override void Draw()
        {
            Play.Draw(spriteBatch);
        }

        //Used for edge detection
        public override void ButtonReset()
        {
            Play.ButtonReset();

        }
    }
}
