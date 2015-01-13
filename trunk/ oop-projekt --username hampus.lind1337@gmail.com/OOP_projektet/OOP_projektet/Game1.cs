using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace OOP_projektet
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random random = new Random();
        //lista för isblock
        List<IceBlock> IceList = new List<IceBlock>();
        Player Tank = new Player();
        Background BG = new Background();
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 800;
            this.Window.Title = "The GHUNTER HERMAN experience";
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Tank.LoadContent(Content);
            BG.LoadContent(Content);
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            //uppdaterar för varje isblock i listan och kollar efter kollisioner
            foreach (IceBlock i in IceList)
            {
                //isblock som kolliderar med spelaren försvinner från listan
                if (i.hitbox.Intersects(Tank.hitbox))
                {
                    i.visible = false;
                    Tank.health = Tank.health - 1;
                    if (Tank.health <= 0)
                    {
                        Tank.gameoverposition.Y = 0;
                        Tank.gameoverposition.X = 0;
                    }
                       

                }

                //gå igenom listan för skotten, om ett skott nudder ett isblock försvinner båda
                for (int q=0; q < Tank.shotlist.Count; q++)
                {
                    if (i.hitbox.Intersects(Tank.shotlist[q].hitbox))
                    {
                        i.visible = false;
                        Tank.shotlist.ElementAt(q).visible = false;
                    }
                }

                    i.Update(gameTime);
            }
            LoadIceBlocks();
            Tank.Update(gameTime);
            BG.Update(gameTime);
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            BG.Draw(spriteBatch);
            foreach (IceBlock i in IceList)
            {
                i.Draw(spriteBatch);
            }
            Tank.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        //funktion för att ladda isblock
        public void LoadIceBlocks()
        {
            int randY = random.Next(-600, -50);
            int randX = random.Next(0, 750);

            //om det finns färre än 5 isblock på skärmen skapas fler
            if (IceList.Count < 5)
            {
                IceList.Add(new IceBlock(Content.Load<Texture2D>("isblock"), new Vector2(randX, randY)));
            }
            //om några isblock är osynliga tas de bort ur listan (samma funktion som skotten)
            for (int q = 0; q < IceList.Count; q++)
            {
                if (!IceList[q].visible)
                {
                    IceList.RemoveAt(q);
                    q--;
                }
            }
        }
    }
}
