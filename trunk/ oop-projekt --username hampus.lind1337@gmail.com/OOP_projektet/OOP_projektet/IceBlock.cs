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
    
    public class IceBlock
    {
        public Rectangle hitbox;
        public Texture2D texture;
        public Vector2 position;
        public int speed;

        public bool visible;
        Random random=new Random();
        public float randomX;
        public float randomY;

        public IceBlock(Texture2D newtexture, Vector2 newposition)
        {
            visible = true;
            texture = newtexture;
            speed = 4;
            position = newposition;
            //750 för att den inte ska spawna utanför x-axeln
            randomX = random.Next(0, 750);
            randomY = random.Next(-600, -50);
        }

        public void LoadContent(ContentManager Content)
        {
            //används inte för tillfället, kanske senare
        }

        public void Update(GameTime gameTime)
        {
            //ställ in hitbox
            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            //uppdatera rörelser
            position.Y = position.Y + speed;
            if (position.Y >= 800)
                position.Y = -50;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (visible == true)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }
    }
}
