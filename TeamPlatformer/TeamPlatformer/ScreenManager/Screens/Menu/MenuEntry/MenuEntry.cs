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
    abstract class MenuEntry
    {
        public event ScreenHandler onClickHandler;

        protected Vector2 position;
        protected Rectangle boundingRectangle;
        protected Rectangle sourceRectangle;
        protected float _rotation { get; set; }
        protected float _scale { get; set; }
        protected float deltaTime;

        protected string _choice { get; set; }
        public bool _isSelected { get; set; }

        protected void onClick(string choice)
        {
            var handler = onClickHandler;
            if (handler != null)
                handler(choice);
        }

        public void checkSelected(Vector2 mousePosition, bool mouseClick)
        {
            Rectangle mouseRectangle = new Rectangle((int)mousePosition.X, (int)mousePosition.Y, 1, 1);

            if (mouseRectangle.Intersects(boundingRectangle))
            {
                this._isSelected = true;
                if (mouseClick)
                {
                    onClick(this._choice);
                }
            }
            else
            {
                this._isSelected = false;
            }
        }

        abstract public void LoadContent(ContentManager content, string assetName);

        abstract public void Update(GameTime gameTime);

        abstract public void Draw(SpriteBatch spriteBatch);

    }
}
