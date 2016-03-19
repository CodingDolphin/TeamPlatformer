using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace TeamPlatformer
{
    class MenuScreen : GameScreen
    {
        InputManager input;

        //Menü Grafiken und Positionen

        private Texture2D GameNameTexture;
        private Texture2D BackgroundTexture;
        private Rectangle TitelRectangle;
        private Rectangle BackgroundRectangle;
        public List<MenuEntry> MenuEntryList;
        private int currentButton { get; set; }

        //Hintergund Musik und Soundeffekte

        private Song backgroundMusic;

        public MenuScreen(int screenWidth, int screenHeight)
        {
            input = new InputManager();
            MenuEntryList = new List<MenuEntry>();

            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;

            //Menü Einträge hier hinzufügen choice mit ScreenManager Konstanten vergleichen

            MenuEntryList.Add(new Button("PLAY", 1, true));
            MenuEntryList.Add(new Button("OPTION", 2, true));
            MenuEntryList.Add(new Button("EXIT", 3, true));
            currentButton = 1;
        }

        public override void LoadContent(ContentManager content)
        {
            //Logo Textur laden und positionieren

            GameNameTexture = content.Load<Texture2D>("ProjectAssets/MenuAssets/GraphicAssets/GameName1");
            TitelRectangle = new Rectangle(screenWidth / 2 - (GameNameTexture.Width / 2), screenHeight / 6 - (GameNameTexture.Height / 2), GameNameTexture.Width, GameNameTexture.Height);

            //Hintergrund Textur laden und positionieren

            BackgroundTexture = content.Load<Texture2D>("ProjectAssets/MenuAssets/GraphicAssets/MenuBackground2");
            BackgroundRectangle = new Rectangle(0, 0, screenWidth, screenHeight);

            //Musik Pfad hier eingeben

            backgroundMusic = content.Load<Song>("ProjectAssets/MenuAssets/SoundAssets/Mischief");

            //Menu Grafiken Pfad übergeben

            MenuEntryList[0].LoadContent(content, "ProjectAssets/MenuAssets/GraphicAssets/playButton");
            MenuEntryList[1].LoadContent(content, "ProjectAssets/MenuAssets/GraphicAssets/optionButton");
            MenuEntryList[2].LoadContent(content, "ProjectAssets/MenuAssets/GraphicAssets/exitButton");
        }

        public override void Update(GameTime gameTime)
        {
            input.Update(gameTime);

            if (MediaPlayer.State == MediaState.Stopped)
                MediaPlayer.Play(backgroundMusic);

            foreach (MenuEntry entry in MenuEntryList)
            {
                entry.checkSelected(input.getMousePosition(),input.GetLeftClick());
                entry.Update(gameTime);

                if (entry is Button)
                {
                    var button = (Button)entry;
                    if (currentButton == button._buttonID)
                    {
                        bool finish = button.SlideAppear();
                        if (finish)
                            currentButton++;
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(BackgroundTexture, BackgroundRectangle, Color.White);
            spriteBatch.Draw(GameNameTexture, TitelRectangle, Color.White);
            foreach (MenuEntry entry in MenuEntryList)
            {
                entry.Draw(spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
