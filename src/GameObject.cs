using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace midiGame
{
    class GameObject
    {

        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; } = new Vector2(0, 0);
        public Texture2D texture;

        public GameObject(Texture2D texture, Vector2 position)
        {
            this.position = position;
            this.texture = texture;
        }

        public Rectangle boundingBox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
        }

        public Vector2 getCentre
        {
            get
            {
                return new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);
            }
        }

        public GameObject clone()
        {
            var objectClone = new GameObject(this.texture, this.position);
            objectClone.velocity = this.velocity;
            return objectClone;
        }

        public virtual void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position: position, scale: new Vector2(0.5f, 0.5f));
        }

        public void update(GameTime gameTime)
        {
            this.position += velocity;
        }
    }
}