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
        static Texture2D background;
        static Vector2 menuPos;
        static PlayerManager playerManager;
        static InterfaceText interfaceText;
        static Healthbar healthBar;
        static InterfaceManager interfaceManager;
        static Powerup powerup;
        static Game game;
        static Blackhole blackhole;

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

            playerManager = new PlayerManager(content.Load<Texture2D>("player1"), content.Load<Texture2D>("player2"), size, size, width, height);
            powerup = new Powerup(content.Load<Texture2D>("ball_1"), new Vector2(rnd.Next(50, width-50), rnd.Next(50, height-50)), Vector2.Zero, new Point(50, 50), height);
            interfaceText = new InterfaceText(content.Load<SpriteFont>("Test"), 0, 0);
            background = content.Load<Texture2D>("background");
            healthBar = new Healthbar(content.Load<Texture2D>("p1healthbar"), new Rectangle(53, 6, 100, 31), 100);
            interfaceManager = new InterfaceManager(content.Load<Texture2D>("p1healthbar2"), content.Load<Texture2D>("p2healthbar2"), new Rectangle(53, 6, 100, 31), new Rectangle(1141, 6, 100, 31), 100, 100,
                content.Load<SpriteFont>("Test"), 0, 0, 0, 0);
            blackhole = new Blackhole(content.Load<Texture2D>("empty"), new Vector2(width / 2, height / 2), Vector2.Zero, new Point(10, 10), height,1f);
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

            if (Keyboard.GetState().IsKeyDown(Keys.S)) playerManager.players[0].Thrust(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.D)) playerManager.players[0].Turn(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.A)) playerManager.players[0].Turn(-0.1f);

            if (Keyboard.GetState().IsKeyDown(Keys.Down)) playerManager.players[1].Thrust(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) playerManager.players[1].Turn(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) playerManager.players[1].Turn(-0.1f);

            foreach (var player in playerManager.players)
            {
                player.Update();

                if(player.HitCircular(powerup.Radius,powerup.Position))
                {
                    player.Damage(1);
                }

                if(player.HitCircular(blackhole.Radius,blackhole.Position))
                {
                    player.Damage(100);
                }

                if (player.Health <= 0)
                {
                    player.deathCount += 1;
                    player.Position = playerManager.RandomPos();
                    player.Health = 100;
                    player.Velocity = Vector2.Zero;
                }
            }

            for (int i = 0; i < playerManager.players.Length; i++)
            {
                interfaceManager.healthBars[i].health = playerManager.players[i].Health;
                interfaceManager.interfaceTexts[i].kills = playerManager.players[i].killCount;
                interfaceManager.interfaceTexts[i].points = playerManager.players[i].killCount - playerManager.players[i].deathCount;
            }
            interfaceManager.healthBars[0].healthRectangle = new Rectangle(53, 6, Convert.ToInt32((interfaceManager.healthBars[0].health / healthBar.maxHealth) * healthBar.fullWidth), 31);
            interfaceManager.healthBars[1].healthRectangle = new Rectangle(Convert.ToInt32(1141+(1-(interfaceManager.healthBars[1].health / healthBar.maxHealth))*healthBar.fullWidth), 6, Convert.ToInt32((interfaceManager.healthBars[1].health / healthBar.maxHealth) * healthBar.fullWidth), 31);


            playerManager.Pull(blackhole.Position, blackhole.Force);

            return State.Run;
        }

        public static void RunDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);

            foreach (var player in playerManager.players)
            {
                player.Draw(spriteBatch);
            }

            interfaceManager.healthBars[0].Draw(spriteBatch);
            interfaceManager.healthBars[1].Draw(spriteBatch);

            interfaceManager.interfaceTexts[0].Draw(Convert.ToString(interfaceManager.interfaceTexts[0].points), spriteBatch, 675, 50);
            interfaceManager.interfaceTexts[1].Draw(Convert.ToString(interfaceManager.interfaceTexts[1].points), spriteBatch, 900, 50);

            powerup.Draw(spriteBatch);

            interfaceText.Draw("Points", spriteBatch, 730, 50);
            interfaceText.Draw("Kills", spriteBatch, 742, 90);
        }
    }
}
