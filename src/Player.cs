using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midiGame
{
    class Player : GameObject
    {
        public enum Direction {
            LEFT = -1,
            RIGHT = 1,
            UP = -1,
            DOWN = 1}

        private float moveSpeed = 5f;
        private Rectangle boundingRect;

        public Player(Texture2D texture, Vector2 position, Rectangle boundingRect) : base(texture, position)
        {
            this.texture = texture;
            this.position = position;
            this.boundingRect = boundingRect;
        }

        public void move(Direction horizontal, Direction vertical)
        {
            Vector2 deltaPosition = new Vector2(0, 0);

            float newX = (float) horizontal * moveSpeed;
            float newY = (float) vertical * moveSpeed;
            if(newX + position.X < boundingRect.Right && newX + position.X > boundingRect.Left)
            {
                deltaPosition += new Vector2(newX, 0);
            }
            if(newY + position.Y > boundingRect.Top && newY + position.Y < boundingRect.Bottom)
            {
                deltaPosition += new Vector2(0, newY);
            }

            position += deltaPosition;
        }

        public new void update(GameTime gameTime)
        {
            base.update(gameTime);
            if(InputManager.IsKeyPressed(Keys.Left))
            {
                move(Direction.LEFT, 0);
            } else if (InputManager.IsKeyPressed(Keys.Right))
            {
                move(Direction.RIGHT, 0);
            }

            if (InputManager.IsKeyPressed(Keys.Up))
            {
                move(0, Direction.UP);
            }
            if (InputManager.IsKeyPressed(Keys.Down))
            {
                move(0, Direction.DOWN);
            }
        }
    }
}
