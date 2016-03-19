using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace TeamPlatformer
{
    abstract class DynamicObject
    {
        public Vector2 position;
        public Vector2 velocity;
        public float deltaTime;

        protected float scale;
        protected float rotation;

        public Rectangle boundingRectangle;

        protected void gainForce(Vector2 force)
        {
            this.velocity.X += force.X * deltaTime;
            this.velocity.Y += force.Y * deltaTime;
        }

        abstract public void LoadContent(ContentManager content);

        abstract public void Update(GameTime gameTime);

        abstract public void Draw(SpriteBatch spriteBatch);
    }
}
