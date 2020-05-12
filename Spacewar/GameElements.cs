using System;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spacewar
{
    static class GameElements
    {
        static Texture2D menuSprite;
        static Texture2D subMenuSprite;
        static Texture2D background;
        static Vector2 menuPos;
        static Vector2 subMenuPos;
        static PlayerManager playerManager;
        static InterfaceText interfaceText;
        static Healthbar healthBar;
        static InterfaceManager interfaceManager;
        static Game game;
        static Blackhole blackhole;
        static PowerupManager powerupManager;
        static Powerup powerup;
        //static Weapons Weapons;

        static Random rnd = new Random();

        static int width = 1600;
        static int height = 900;

        public enum State { Menu, Run, SubMenu, Quit };
        public static State currentState;

        public static void Initialize() { }

        public static void LoadContent(ContentManager content)
        {
            menuSprite = content.Load<Texture2D>("menu");
            menuPos.X = 0;
            menuPos.Y = 0;

            subMenuSprite = content.Load<Texture2D>("subMenu");
            subMenuPos.X = 0;
            subMenuPos.Y = 0;

            var size = new Point(100, 100);

            playerManager = new PlayerManager(content.Load<Texture2D>("player1"), content.Load<Texture2D>("player2"), size, size, width, height);
            powerupManager = new PowerupManager(height, content.Load<Texture2D>("ball_1"));
            interfaceText = new InterfaceText(content.Load<SpriteFont>("font1"), content.Load<SpriteFont>("font2"), 0, 0);
            background = content.Load<Texture2D>("background");
            healthBar = new Healthbar(content.Load<Texture2D>("p1healthbar"), new Rectangle(53, 6, 100, 31), 100);
            interfaceManager = new InterfaceManager(content.Load<Texture2D>("p1healthbar2"), content.Load<Texture2D>("p2healthbar2"), new Rectangle(53, 6, 100, 31), new Rectangle(1141, 6, 100, 31), 100, 100,
                content.Load<SpriteFont>("font1"), content.Load<SpriteFont>("font2"), 0, 0, 0, 0, 180f);
            blackhole = new Blackhole(content.Load<Texture2D>("empty"), new Vector2(width / 2, height / 2), Vector2.Zero, new Point(10, 10), height,1f);
            //weapons = new Weapons(Content.Load<Texture2D>("projectile_1"));
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                //return State.Pause;
                game.Exit();
            }

            interfaceManager.Timer(gameTime);

            if (interfaceManager.end == true)
            {
                interfaceManager.healthBars[0].health = 100;
                playerManager.players[0].Position = playerManager.RandomPos();
                interfaceManager.interfaceTexts[0].points = 0;

                interfaceManager.healthBars[1].health = 100;
                playerManager.players[1].Position = playerManager.RandomPos();
                interfaceManager.interfaceTexts[1].points = 0;

                interfaceManager.end = false;
                interfaceManager.timeCounter = 180f;

                return State.SubMenu;
            }

           

            if (Keyboard.GetState().IsKeyDown(Keys.S)) playerManager.players[0].Thrust(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.D)) playerManager.players[0].Turn(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.A)) playerManager.players[0].Turn(-0.1f);
            /*if (Keyboard.GetState().IsKeyDown(Keys.Tab)) var bullet = Weapons.Clone() as Weapons;
            bullet.Direction = this.Direction;
            bullet.Position = this.Positon;
            bullet.LinearVelocity = this.LinearVelocity * 2;
            bullet.LifeSpan = 3f;   
            bullet.Parent = this;
            spirtes.Add(weapons); */


            if (Keyboard.GetState().IsKeyDown(Keys.Down)) playerManager.players[1].Thrust(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) playerManager.players[1].Turn(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) playerManager.players[1].Turn(-0.1f);
            /*if (Keyboard.GetState().IsKeyDown(Keys.Space)) var bullet = Weapons.Clone() as Weapons;
            bullet.Direction = this.Direction;
            bullet.Position = this.Positon;
            bullet.LinearVelocity = this.LinearVelocity * 2;
            bullet.LifeSpan = 3f;
            bullet.Parent = this;
            spirtes.Add(weapons);*/

            foreach (var player in playerManager.players)
            {
                player.Update();

                foreach(var powerup in powerupManager.powerUps.ToArray())
                {
                    if(player.HitCircular(powerup.Radius, powerup.Position))
                    {
                        powerupManager.remove(powerup);
                        
                    }
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

            interfaceManager.Winner();

            powerupManager.Update(gameTime);

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

            if (Convert.ToString(interfaceManager.interfaceTexts[0].points).Length >= 3)
            {
                interfaceManager.interfaceTexts[0].Draw(Convert.ToString(interfaceManager.interfaceTexts[0].points), interfaceManager.interfaceTexts[0].font1, spriteBatch, 675, -4);
            }
            else
            {
                interfaceManager.interfaceTexts[0].Draw(Convert.ToString(interfaceManager.interfaceTexts[0].points), interfaceManager.interfaceTexts[0].font1, spriteBatch, 695, -4);
            }

            interfaceManager.interfaceTexts[1].Draw(Convert.ToString(interfaceManager.interfaceTexts[1].points), interfaceManager.interfaceTexts[0].font1, spriteBatch, 870, -4);

            interfaceManager.interfaceTexts[0].Draw(Convert.ToString(interfaceManager.interfaceTexts[0].kills), interfaceManager.interfaceTexts[0].font1, spriteBatch, 705, 32);
            interfaceManager.interfaceTexts[1].Draw(Convert.ToString(interfaceManager.interfaceTexts[1].kills), interfaceManager.interfaceTexts[0].font1, spriteBatch, 870, 32);

            interfaceText.Draw(interfaceManager.timeText, interfaceManager.interfaceTexts[0].font1, spriteBatch, 750, 100);
            powerupManager.Draw(spriteBatch);

        }
            

        public static State SubMenuUpdate()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.R))
            {
                interfaceManager.healthBars[0].health = 100;
                playerManager.players[0].Position = playerManager.RandomPos();
                interfaceManager.interfaceTexts[0].points = 0;
                interfaceManager.interfaceTexts[0].kills = 0;

                interfaceManager.healthBars[1].health = 100;
                playerManager.players[1].Position = playerManager.RandomPos();
                interfaceManager.interfaceTexts[1].points = 0;
                interfaceManager.interfaceTexts[1].kills = 0;

                return State.Run;
            }

            if (keyboardState.IsKeyDown(Keys.M))
            {
                return State.Menu;
            }
          
            if (keyboardState.IsKeyDown(Keys.Q))
            {
                return State.Quit;
            }

            return State.SubMenu;
        }

        public static void SubMenuDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(subMenuSprite, subMenuPos, Color.White);
            interfaceText.Draw(interfaceManager.winner, interfaceManager.interfaceTexts[0].font2, spriteBatch, 794, 220);
        }
    }
}
