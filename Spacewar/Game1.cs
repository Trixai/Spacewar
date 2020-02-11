using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spacewar
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Player player;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            
            player = new Player(Content.Load<Texture2D>("birdie"), new Vector2(500, 250), new Vector2(0, 0), new Point(100, 100));
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Space)) player.Thrust(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.D)) player.Turn(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.A)) player.Turn(-0.1f);
            

            if (player.Position.X > 1600) player.Position = new Vector2(0, 900-player.Position.Y);
            if (player.Position.X < 0) player.Position = new Vector2(1600, 900-player.Position.Y);
            if(player.Position.Y > 900) player.Position = new Vector2(1600-player.Position.X, 0);
            if (player.Position.Y < 0) player.Position = new Vector2(1600-player.Position.X, 900);

            player.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            player.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
