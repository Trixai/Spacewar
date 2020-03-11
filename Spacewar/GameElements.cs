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
        static PlayerManager playerManager;
        static Interface interFace;
        static Healthbar healthbar;
        static Powerup powerup;
        static Game game;
        static Texture2D background;

        static Random rnd = new Random();

        static int width = 1600;
        static int height = 900;

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

            var size = new Point(100, 100);

            player = new Player(content.Load<Texture2D>("player1"), new Vector2(500, 250), new Vector2(0, 0), new Point(100, 100), width, height);
            playerManager = new PlayerManager(content.Load<Texture2D>("player1"), content.Load<Texture2D>("player2"), size, size, width, height);
            powerup = new Powerup(content.Load<Texture2D>("ball_1"), new Vector2(rnd.Next(1, 1500), rnd.Next(1, 800)), Vector2.Zero, new Point(50, 50), height);
            interFace = new Interface(content.Load<SpriteFont>("Test"), 0, 0);
            healthbar = new Healthbar(content.Load<Texture2D>("p1healthbar"), content.Load<Texture2D>("p2healthbar"), 100, 100);
            background = content.Load<Texture2D>("background");
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

            if (player.Position.X > 1600) player.Position = new Vector2(0, 900 - player.Position.Y);
            if (player.Position.X < 0) player.Position = new Vector2(1600, 900 - player.Position.Y);
            if (player.Position.Y > 900) player.Position = new Vector2(1600 - player.Position.X, 0);
            if (player.Position.Y < 0) player.Position = new Vector2(1600 - player.Position.X, 900);

            if (Keyboard.GetState().IsKeyDown(Keys.S)) playerManager.players[0].Thrust(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.D)) playerManager.players[0].Turn(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.A)) playerManager.players[0].Turn(-0.1f);

            if (Keyboard.GetState().IsKeyDown(Keys.Down)) playerManager.players[1].Thrust(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) playerManager.players[1].Turn(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) playerManager.players[1].Turn(-0.1f);

            foreach (var player in playerManager.players)
            {

                player.Update();

                if (player.HitCircular(powerup.Radius, powerup.Position))
                {
                    healthbar.actualHealth -= 1;
                }
            }

            playerManager.Pull(new Vector2(width/2,height/2), 1f);

            return State.Run;
        }

        public static void RunDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);

            foreach (var player in playerManager.players)
            {
                player.Draw(spriteBatch);
            }
            powerup.Draw(spriteBatch);

            interFace.Draw("Health", spriteBatch, 725, 10);
            interFace.Draw("Points", spriteBatch, 730, 50);
            interFace.Draw("Kills", spriteBatch, 742, 90);

            //interFace.Draw(Convert.ToString(interFace.health), spriteBatch, 635, 10);
            //interFace.Draw(Convert.ToString(interFace.health), spriteBatch, 900, 10);

            interFace.Draw(Convert.ToString(interFace.points), spriteBatch, 675, 50);
            interFace.Draw(Convert.ToString(interFace.points), spriteBatch, 900, 50);

            interFace.Draw(Convert.ToString(interFace.kills), spriteBatch, 675, 90);
            interFace.Draw(Convert.ToString(interFace.kills), spriteBatch, 900, 90);
        }
    }
}
