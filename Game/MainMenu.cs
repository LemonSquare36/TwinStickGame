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
        Button Play, Exit, FullScreen;
        Texture2D PlayUnpressed, PlayPressed, ExitUnpressed, ExitPressed, FullScreenUnpressed, FullScreenPressed;

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void LoadContent(SpriteBatch spriteBatchmain)
        {
            spriteBatch = spriteBatchmain;
            PlayUnpressed = Main.GameContent.Load<Texture2D>("Buttons/Play");
            PlayPressed = Main.GameContent.Load<Texture2D>("Buttons/PlayPressed");
            ExitUnpressed = Main.GameContent.Load<Texture2D>("Buttons/Exit");
            ExitPressed = Main.GameContent.Load<Texture2D>("Buttons/ExitHighlight");
            FullScreenUnpressed = Main.GameContent.Load<Texture2D>("Buttons/FullScreen");
            FullScreenPressed = Main.GameContent.Load<Texture2D>("Buttons/FullScreenPressed");

            Play = new Button(new Vector2(500, 350), 600, 220, PlayPressed, PlayUnpressed, "Play");
            Exit = new Button(new Vector2(500, 700), 600, 220, ExitUnpressed, ExitPressed, "Exit");
            FullScreen = new Button(new Vector2(500, 50), 600, 220, FullScreenPressed, FullScreenUnpressed, "FullScreen");

            Play.ButtonClicked += ButtonClicked;
            Exit.ButtonClicked += ButtonClicked;
            FullScreen.ButtonClicked += ButtonClicked;
        }

        public override void Update(Camera camera, GraphicsDeviceManager graphicsManager)
        {
            //Get current mouse state
            Vector2 worldPosition = MousePos();

            Play.Update(mouse, worldPosition);
            Exit.Update(mouse, worldPosition);
            FullScreen.Update(mouse, worldPosition);

            Debug.WriteLine(worldPosition);
            Debug.WriteLine(mouse.Position);

        }

        public override void Draw()
        {
            Play.Draw(spriteBatch);
            Exit.Draw(spriteBatch);
            FullScreen.Draw(spriteBatch);
        }

        //Used for edge detection
        public override void ButtonReset()
        {
            Play.ButtonReset();
            Exit.ButtonReset();
            FullScreen.ButtonReset();
        }
    }
}
