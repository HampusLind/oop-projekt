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
    class Player
    {
        public Texture2D gameovertexture;
        public Texture2D texture;
        public Texture2D shottexture;
        public Vector2 position;
        public Vector2 gameoverposition;
        public int speed;
        public int health;
        public float shotdelay;
        public Rectangle hitbox;
        public bool kolliderar;
        public List<Shot> shotlist;

        //konstruktor
        public Player()
        {
            shotlist=new List<Shot>();
            texture = null;
            position = new Vector2(300, 300);
            speed = 10;
            kolliderar = false;
            shotdelay = 20;
            health = 5;
            gameovertexture = null;
            gameoverposition = new Vector2(0, -1000);
        }

        //laddar i klassen
        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("tank green");
            shottexture = Content.Load<Texture2D>("playershot");
            gameovertexture = Content.Load<Texture2D>("game over");
            
        }

        //ritar
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.Draw(gameovertexture, gameoverposition, Color.White);
            foreach (Shot s in shotlist)
                s.Draw(spriteBatch);
        }

        //uppdaterar
        public void Update(GameTime gameTime)
        { 
            //keystate
            KeyboardState keyState = Keyboard.GetState();

            //hitbox för spelare
            hitbox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            //kontroller
            //skjuter
            if (keyState.IsKeyDown(Keys.Space))
                shooting();
            UpdateShot();
            //upp
            if (keyState.IsKeyDown(Keys.W))
                position.Y = position.Y - speed;
            //vänster
            if (keyState.IsKeyDown(Keys.A))
                position.X = position.X - speed;
            //ner
            if (keyState.IsKeyDown(Keys.S))
                position.Y = position.Y + speed;
            //höger
            if (keyState.IsKeyDown(Keys.D))
                position.X = position.X + speed;

            //gränser för skärmen
            if (position.X <= 0)
                position.X = 0;

            if (position.X >= 800 - texture.Width)
                position.X = 800 - texture.Width;

            if (position.Y <= 0)
                position.Y = 0;

            if (position.Y >= 800 - texture.Height)
                position.Y = 800 - texture.Height;
        }

        //metod för skjutande och skottens position
        public void shooting()
        {
            //skjut om shotdelay blir "resetad"
            if (shotdelay >= 0)
                shotdelay--;

            //om shotdelay blir noll skapa nytt skott och lägg till det i listan
            if(shotdelay<=0)
            {
                Shot newshot = new Shot(shottexture);
                newshot.position = new Vector2(position.X + 50 - newshot.texture.Width / 2, position.Y + 30);

                newshot.visible = true;

                if (shotlist.Count < 20)
                    shotlist.Add(newshot);

            }
            if (shotdelay == 0)
                shotdelay = 20;

        }
        // update för skott
        public void UpdateShot()
        {
            //när skotten åker utanför skärmen tas de bort från listan och försvinner
            foreach (Shot s in shotlist)
            {
                //hitbox för skotten i listan
                s.hitbox = new Rectangle((int)s.position.X, (int)s.position.Y, s.texture.Width, s.texture.Height);
                //rörelse för skott
                s.position.Y = s.position.Y - s.speed;
                //om skottet går utanför skärmen blir det osynligt
                if (s.position.Y <= 0)
                    s.visible = false;
            }
            //gå igenom listan och kolla om några skott är osynliga, ta sedan bort osynliga
            for (int i = 0; i < shotlist.Count; i++)
            {
                if (!shotlist[i].visible)
                {
                    shotlist.RemoveAt(i);
                    i--;
                }
            }


        }
    }
}
