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
        Random rnd = new Random();

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
            GameElements.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

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
