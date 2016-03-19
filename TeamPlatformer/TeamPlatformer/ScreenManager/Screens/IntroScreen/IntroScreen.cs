using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace TeamPlatformer
{
    class IntroScreen : GameScreen
    {
        //Intro Länge hier einstellen

        private const int introDuration = 5;
        private double elapsedTime = 0f;

        private Texture2D Intro;
        private Rectangle BackgroundRectangle;

        private SoundEffect LogoSound;

        public IntroScreen(int screenWidth, int screenHeight)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;

            BackgroundRectangle = new Rectangle(0, 0, screenWidth, screenHeight);
        }

        public override void LoadContent(ContentManager content)
        {
            Intro = content.Load<Texture2D>("ProjectAssets/IntroAssets/GraphicAssets/Logo1");
            LogoSound = content.Load<SoundEffect>("ProjectAssets/IntroAssets/SoundAssets/LogoSound");

            LogoSound.Play();
        }

        public override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;

            if(elapsedTime >= introDuration)
            {
                OnIntroEnd();
            }
        }

        protected void OnIntroEnd()
        {
            elapsedTime = 0;
            NotifyScreenChange(string.Empty);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Intro, BackgroundRectangle, Color.White);
            spriteBatch.End();
        }
    }
}
