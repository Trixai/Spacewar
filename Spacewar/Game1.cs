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
        PlayerManager playerManager;
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

            GameElements.currentState = GameElements.State.Menu;
            GameElements.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var texture = Content.Load<Texture2D>("birdie");
            var size = new Point(100, 100);
            playerManager = new PlayerManager(texture, texture, size, size, width,height);

            powerup = new Powerup(Content.Load<Texture2D>("ball_1"), new Vector2(rnd.Next(1,1500), rnd.Next(1,800)), Vector2.Zero , new Point(50,50),height);
            GameElements.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();


            if (Keyboard.GetState().IsKeyDown(Keys.S)) playerManager.players[0].Thrust(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.D)) playerManager.players[0].Turn(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.A)) playerManager.players[0].Turn(-0.1f);

            if (Keyboard.GetState().IsKeyDown(Keys.Down)) playerManager.players[1].Thrust(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) playerManager.players[1].Turn(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) playerManager.players[1].Turn(-0.1f);

            foreach (var player in playerManager.players)
            {

                player.Update();

                if (player.HitCircular(powerup.Radius,powerup.Position))
                {
                    Exit();
                }
            }
            switch (GameElements.currentState)
            {
                case GameElements.State.Run:
                    GameElements.currentState = GameElements.RunUpdate(gameTime);
                    break;
                case GameElements.State.Quit:
                    this.Exit();
                    break;
                default:
                    GameElements.currentState = GameElements.MenuUpdate();
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            foreach (var player in playerManager.players)
            {
                player.Draw(spriteBatch);
            }
            powerup.Draw(spriteBatch);

            
            switch (GameElements.currentState)
            {
                case GameElements.State.Run:
                    GameElements.RunDraw(spriteBatch);
                    break;
                case GameElements.State.Quit:
                    this.Exit();
                    break;
                default:
                    GameElements.MenuDraw(spriteBatch);
                    break;
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
