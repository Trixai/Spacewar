using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Spacewar
{
    public class Game1 : Game
    {
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;
        Random rnd = new Random();
        Powerup powerup;

        const int width = 1600;
        const int height = 900;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            
            player = new Player(Content.Load<Texture2D>("birdie"), new Vector2(500, 250), new Vector2(0, 0), new Point(100, 100),height);

            powerup = new Powerup(Content.Load<Texture2D>("ball_1"), new Vector2(rnd.Next(1,1500), rnd.Next(1,800)), Vector2.Zero , new Point(50,50),height);
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            powerup.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Space)) player.Thrust(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.D)) player.Turn(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.A)) player.Turn(-0.1f);


            if (player.Position.X > width) player.Position = player.CalculateY(width,height);
            if (player.Position.X < 0) player.Position = player.CalculateY(width, height);
            if(player.Position.Y > height) player.Position = player.CalculateX(width, height);
            if (player.Position.Y < 0) player.Position = player.CalculateX(width, height);
            
            player.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            player.Draw(spriteBatch);

            powerup.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
