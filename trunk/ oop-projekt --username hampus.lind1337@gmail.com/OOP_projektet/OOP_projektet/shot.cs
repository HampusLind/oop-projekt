using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OOP_projektet
{
    public class Shot
    {
        public Rectangle hitbox;
        public Texture2D texture;
        public Vector2 center;
        public Vector2 position;
        public bool visible;
        public float speed;

        public Shot(Texture2D newtexture)
        {
            speed = 10;
            texture = newtexture;
            visible = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
