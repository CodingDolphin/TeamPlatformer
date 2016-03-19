using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TeamPlatformer
{
    public delegate void ScreenHandler(string input);

    class ScreenManager : DrawableGameComponent
    {
        //XNA Components

        SpriteBatch spriteBatch;
        ContentManager content;
        GraphicsDeviceManager graphics;

        //Konstanten

        private const string play = "PLAY";
        private const string exit = "EXIT";
        private const string options = "OPTIONS";
        private const string highScore = "HIGHSTORE";

        //Klassen Variablen

        List<GameScreen> screenList;
        GameScreen currentScreen;

        public ScreenManager(Game game): base(game)
        {
            //XNA Components

            graphics = new GraphicsDeviceManager(game);
            content = new ContentManager(game.Content.ServiceProvider, game.Content.RootDirectory);

            //Start Settings für Auflösung und FullScreen hier einstellen

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            //Hier weitere Screens hinzufügen

            screenList = new List<GameScreen>();
            screenList.Add(new IntroScreen(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            screenList.Add(new MenuScreen(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            screenList.Add(new PlayScreen(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            screenList.Add(new PauseScreen(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight));
            

            //Alle Events hier registrieren

            var menu = (MenuScreen)screenList[1];
            foreach (MenuEntry entry in menu.MenuEntryList)
            {
                entry.onClickHandler += OnClickHandler;
            }

            screenList[0].ScreenChange += OnIntroEnd;

            //Startscreen festlegen

            currentScreen = screenList[0];
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (GameScreen screen in screenList)
            {
                screen.LoadContent(content);
            }

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
            base.Update(gameTime);
        }

        private void OnIntroEnd(string input)
        {
            //Nach dem Intro immer zum Menü Screenlist[1]

            currentScreen = screenList[1];
        }

        private void OnClickHandler(string choice)
        {
            if (play == choice.ToUpper())
                currentScreen = screenList[2];
            if (exit == choice.ToUpper())
                Game.Exit();
        }


        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(13,100,208));
            currentScreen.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
