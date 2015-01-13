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
    public class Background
    {
        public Texture2D texture;
        public Vector2 BGposition1;
        public Vector2 BGposition2;
        public int speed;


        //konstruktor
        public Background()
        {
            texture = null;
            BGposition1 = new Vector2(0, 0);
            BGposition2 = new Vector2(0, -800);
            speed = 5;
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("bakgrund");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, BGposition1, Color.White);
            spriteBatch.Draw(texture, BGposition2, Color.White);

        }

        public void Update(GameTime gameTime)
        {
            //hastighet för bakgrund
            BGposition1.Y = BGposition1.Y + speed;
            BGposition2.Y = BGposition2.Y + speed;
            
            //upprepande bakgrund
            if (BGposition1.Y >= 800)
            {
                BGposition1.Y = 0;
                BGposition2.Y = -800;
            }
                   
        }
    }
}
