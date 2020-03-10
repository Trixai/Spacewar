using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spacewar
{
    static class GameElements
    {
        static Texture2D menuSprite;
        static Vector2 menuPos;
        static Player player;
        static Interface interFace;
        static Powerup powerup;

        static Random rnd = new Random();

        public enum State { Menu, Run, Quit };
        public static State currentState;

        public static void Initialize()
        {
            
        }

        public static void LoadContent(ContentManager content)
        {
            menuSprite = content.Load<Texture2D>("menu");
            menuPos.X = 0;
            menuPos.Y = 0;

            player = new Player(content.Load<Texture2D>("birdie"), new Vector2(500, 250), new Vector2(0, 0), new Point(100, 100));
            powerup = new Powerup(content.Load<Texture2D>("ball_1"), new Vector2(rnd.Next(1, 1500), rnd.Next(1, 800)), Vector2.Zero, new Point(50, 50));
            interFace = new Interface(content.Load<SpriteFont>("Test"), 100);
        }

        public static State MenuUpdate()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.S))
                return State.Run;
            if (keyboardState.IsKeyDown(Keys.Q))
                return State.Quit;

            return State.Menu;
        }

        public static void MenuDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menuSprite, menuPos, Color.White);
        }

        public static State RunUpdate(GameTime gameTime)
        {
            powerup.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Space)) player.Thrust(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.D)) player.Turn(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.A)) player.Turn(-0.1f);


            if (player.Position.X > 1600) player.Position = new Vector2(0, 900 - player.Position.Y);
            if (player.Position.X < 0) player.Position = new Vector2(1600, 900 - player.Position.Y);
            if (player.Position.Y > 900) player.Position = new Vector2(1600 - player.Position.X, 0);
            if (player.Position.Y < 0) player.Position = new Vector2(1600 - player.Position.X, 900);

            player.Update();

            //if (player.TakeDamage() < interFace.health)
            //{
            //    interFace.health -= 1;
            //}
            //else if (player.TakeDamage() != interFace.health)
            //{
            //    interFace.health = player.TakeDamage();
            //}

            //if (!player.IsAlive)
            //  return State.Menu;

            return State.Run;

        }

        public static void RunDraw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);

            powerup.Draw(spriteBatch);

            interFace.Draw("Health", spriteBatch, 10, 10);
            interFace.Draw("Health", spriteBatch, 1520, 10);
            interFace.Draw(Convert.ToString(interFace.health), spriteBatch, 10, 50);
        }
    }
}
