using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TeamPlatformer
{
   abstract class GameScreen
    {
        public enum ScreenStatus { Activ, Inactiv, inTransition }

        public event ScreenHandler ScreenChange;

        protected void NotifyScreenChange(string input)
        {
            var handler = ScreenChange;
            if (handler != null)
                handler(input);
        }

        protected int screenWidth;
        protected int screenHeight;

        abstract public void LoadContent(ContentManager content);

        abstract public void Update(GameTime gameTime);

        abstract public void Draw(SpriteBatch spriteBatch);
    }
}
