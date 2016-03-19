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
    class Button : MenuEntry
    {
        Texture2D buttonTexture;

        //Erscheinen der Buttons hier configurieren

        public bool slideAppear;
        public bool slideFinish;
        private float appearSpeed = 150f;
        private float _pulseValue = 0.003f;

        public int _buttonID { get; private set; }

        //Konstruktor

        public Button(string choice, int entryID, bool slideAppear)
        {
            this._buttonID = entryID;
            this._choice = choice;
            this.slideAppear = slideAppear;
            this.slideFinish = false;
            this._isSelected = false;
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            //Source und Bounding Rectangle erstellen

            buttonTexture = content.Load<Texture2D>(assetName);
            boundingRectangle = new Rectangle((int)position.X, (int)position.Y, buttonTexture.Width, buttonTexture.Height);
            sourceRectangle = new Rectangle((int)position.X, (int)position.Y, buttonTexture.Width, buttonTexture.Height);
            this._scale = 1f;

            //Unterschiedliche Start Position wenn SlideAppear true ist

            if(slideAppear)
            position = new Vector2(-buttonTexture.Width, buttonTexture.Height * _buttonID + 190 + (15 * _buttonID));
            else
            position = new Vector2(buttonTexture.Width / 2, buttonTexture.Height * _buttonID + 190 + (15 * _buttonID));

        }
        public override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            boundingRectangle.X = (int)position.X;
            boundingRectangle.Y = (int)position.Y;
            Pulse();
        }

        public void Pulse()
        {
            _scale = MathHelper.Clamp(_scale += _pulseValue, 0.97f, 1.03f);

            if (_isSelected)
            {
                if (_scale == 1.03f)
                    _pulseValue = _pulseValue * -1;

                if (_scale == 0.97f)
                    _pulseValue = _pulseValue * -1;
            }
            else
            {
                _scale = 1f;
            }
        }

        public bool SlideAppear()
        {
                if (slideAppear && position.X <= buttonTexture.Width / 2)
                {
                    position.X += appearSpeed * deltaTime;
                    return slideFinish = false;
                }
                else
                {
                    return slideFinish = true;
                }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(buttonTexture, position, sourceRectangle, Color.White, _rotation, Vector2.Zero, _scale, SpriteEffects.None, 1f);
        }
    }
}
