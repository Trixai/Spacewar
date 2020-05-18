using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Spacewar
{
    //Alex gjorde klassen, alla i gruppen har ändrat i koden
    static class GameElements
    {
        static Texture2D menuSprite;
        static Texture2D subMenuSprite;
        static Texture2D pauseSprite;
        static Texture2D background;
        static Vector2 backgroundPos;
        static PlayerManager playerManager;
        static InterfaceText interfaceText;
        static Healthbar healthBar;
        static InterfaceManager interfaceManager;
        static Game game;
        static Blackhole blackhole;
        static PowerupManager powerupManager;

        static Song bgmusic;
        static List<SoundEffect> soundEffects;
        static List<SoundEffectInstance> instances;
        static bool isPlaying = false;

        static Effect playerEffect1;
        static Effect playerEffect2;
        //static Weapons Weapons;

        static Random rnd = new Random();

        static int width = 1600;
        static int height = 900;

        public enum State { Menu, Run, Pause, SubMenu, Quit };
        public static State currentState;

        public static void Initialize() { }

        public static void LoadContent(ContentManager content)
        {
            backgroundPos.X = 0;
            backgroundPos.Y = 0;

            menuSprite = content.Load<Texture2D>("menu");
            subMenuSprite = content.Load<Texture2D>("subMenu");
            pauseSprite = content.Load<Texture2D>("pauseMenu");            

            bgmusic = content.Load<Song>("bgmusic");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.1f;


            var size = new Point(100, 100);

            playerManager = new PlayerManager(content.Load<Texture2D>("player1"), content.Load<Texture2D>("player2"), size, size, width, height);
            powerupManager = new PowerupManager(height, content.Load<Texture2D>("ball_1"));
            interfaceManager = new InterfaceManager(content.Load<Texture2D>("p1healthbar2"), content.Load<Texture2D>("p2healthbar2"), new Rectangle(53, 6, 100, 31), new Rectangle(1141, 6, 100, 31), 100, 100,
                content.Load<SpriteFont>("font1"), content.Load<SpriteFont>("font2"), content.Load<SpriteFont>("font3"), 0, 0, 0, 0, 0, 0, 180f);

            interfaceText = new InterfaceText(content.Load<SpriteFont>("font1"), content.Load<SpriteFont>("font2"), content.Load<SpriteFont>("font3"), 0, 0, 0);
            background = content.Load<Texture2D>("background");
            healthBar = new Healthbar(content.Load<Texture2D>("p1healthbar"), new Rectangle(53, 6, 100, 31), 100);
            blackhole = new Blackhole(content.Load<Texture2D>("empty"), new Vector2(width / 2, height / 2), Vector2.Zero, new Point(10, 10), height, 1f);

            playerEffect1 = content.Load<Effect>("playereffect1");
            playerEffect2 = content.Load<Effect>("playereffect2");

            soundEffects = new List<SoundEffect>();
            soundEffects.Add(content.Load<SoundEffect>("p1thruster"));
            soundEffects.Add(content.Load<SoundEffect>("p1gun"));
            soundEffects.Add(content.Load<SoundEffect>("p2thruster"));
            soundEffects.Add(content.Load<SoundEffect>("p2gun"));

            instances = new List<SoundEffectInstance>();
            instances.Add(soundEffects[0].CreateInstance());
            instances.Add(soundEffects[1].CreateInstance());
            instances.Add(soundEffects[2].CreateInstance());
            instances.Add(soundEffects[3].CreateInstance());


            //weapons = new Weapons(Content.Load<Texture2D>("projectile_1"));
        }

        public static State MenuUpdate()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.S))
            {
                MediaPlayer.Play(bgmusic);
                return State.Run;
            }
            if (keyboardState.IsKeyDown(Keys.Q))
                return State.Quit;

            return State.Menu;
        }

        public static void MenuDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menuSprite, backgroundPos, Color.White);
        }

        public static State RunUpdate(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                MediaPlayer.Pause();
                return State.Pause;
            }

            interfaceManager.Timer(gameTime);

            //Resets everything after game end
            if (interfaceManager.endGame == true)
            {
                interfaceManager.timeCounter = 180f;

                foreach (var player in playerManager.players)
                {
                    player.Health = 100;
                    player.Position = playerManager.RandomPos();
                    player.killCount = 0;
                    player.deathCount = 0;
                }

                instances[0].Stop();
                instances[1].Stop();
                instances[2].Stop();
                instances[3].Stop();

                interfaceManager.endGame = false;

                return State.SubMenu;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                playerManager.players[0].Thrust(0.1f);

                instances[0].IsLooped = true;
                instances[0].Volume = 0.1f;
                instances[0].Play();
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.S))
            {
                instances[0].Stop();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D)) playerManager.players[0].Turn(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.A)) playerManager.players[0].Turn(-0.1f);

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                instances[1].IsLooped = true;
                instances[1].Volume = 0.05f;
                instances[1].Play();
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.W))
            {
                instances[1].Stop();
            }

            /*var bullet = Weapons.Clone() as Weapons;
            bullet.Direction = this.Direction;
            bullet.Position = this.Positon;
            bullet.LinearVelocity = this.LinearVelocity * 2;
            bullet.LifeSpan = 3f;   
            bullet.Parent = this;
            spirtes.Add(weapons); */

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                playerManager.players[1].Thrust(0.1f);

                instances[2].IsLooped = true;
                instances[2].Volume = 0.1f;
                instances[2].Play();
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Down))
            {
                instances[2].Stop();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right)) playerManager.players[1].Turn(0.1f);
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) playerManager.players[1].Turn(-0.1f);

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                instances[3].IsLooped = true;
                instances[3].Volume = 0.05f;
                instances[3].Play();
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Up))
            {
                instances[3].Stop();
            }

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

                if (player.HitCircular(blackhole.Radius, blackhole.Position))
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
                interfaceManager.interfaceTexts[i].deaths = playerManager.players[i].deathCount;
            }

            //Create resizeable rectangles for healthbars
            interfaceManager.healthBars[0].healthRectangle = new Rectangle(53, 6, Convert.ToInt32((interfaceManager.healthBars[0].health / healthBar.maxHealth) * healthBar.fullWidth), 31);
            interfaceManager.healthBars[1].healthRectangle = new Rectangle(Convert.ToInt32(1141 + (1 - (interfaceManager.healthBars[1].health / healthBar.maxHealth)) * healthBar.fullWidth), 6, Convert.ToInt32((interfaceManager.healthBars[1].health / healthBar.maxHealth) * healthBar.fullWidth), 31);


            playerManager.Pull(blackhole.Position, blackhole.Force);

            interfaceManager.Winner();

            powerupManager.Update(gameTime);

            return State.Run;
        }

        public static void RunDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            spriteBatch.End();

            //Player thrust effect
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, playerEffect1, null);
            }
            else
            {
                spriteBatch.Begin();
            }

            playerManager.players[0].Draw(spriteBatch);

            spriteBatch.End();

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, playerEffect2, null);
            }
            else
            {
                spriteBatch.Begin();
            }

            playerManager.players[1].Draw(spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin();

            foreach (var healthbar in interfaceManager.healthBars)
            {
                healthbar.Draw(spriteBatch);
            }

            if (Convert.ToString(interfaceManager.interfaceTexts[0].points).Length > 1)
            {
                interfaceManager.interfaceTexts[0].Draw(Convert.ToString(interfaceManager.interfaceTexts[0].points), interfaceManager.interfaceTexts[0].font2, spriteBatch, 700, 0);
            }
            else
            {
                interfaceManager.interfaceTexts[0].Draw(Convert.ToString(interfaceManager.interfaceTexts[0].points), interfaceManager.interfaceTexts[0].font2, spriteBatch, 715, 0);
            }

            if (Convert.ToString(interfaceManager.interfaceTexts[1].points).Length > 1)
            {
                interfaceManager.interfaceTexts[1].Draw(Convert.ToString(interfaceManager.interfaceTexts[1].points), interfaceManager.interfaceTexts[0].font2, spriteBatch, 845, 0);
            }
            else
            {
                interfaceManager.interfaceTexts[1].Draw(Convert.ToString(interfaceManager.interfaceTexts[1].points), interfaceManager.interfaceTexts[0].font2, spriteBatch, 855, 0);
            }

            //Draw kills and deaths
            interfaceManager.interfaceTexts[0].Draw(Convert.ToString(interfaceManager.interfaceTexts[0].kills), interfaceManager.interfaceTexts[0].font1, spriteBatch, 495, -2);
            interfaceManager.interfaceTexts[1].Draw(Convert.ToString(interfaceManager.interfaceTexts[1].kills), interfaceManager.interfaceTexts[0].font1, spriteBatch, 1005, -2);
            interfaceManager.interfaceTexts[0].Draw(Convert.ToString(interfaceManager.interfaceTexts[0].deaths), interfaceManager.interfaceTexts[0].font1, spriteBatch, 590, -2);
            interfaceManager.interfaceTexts[1].Draw(Convert.ToString(interfaceManager.interfaceTexts[1].deaths), interfaceManager.interfaceTexts[0].font1, spriteBatch, 1100, -2);

            interfaceText.Draw(interfaceManager.timeText, interfaceManager.interfaceTexts[0].font1, spriteBatch, 760, 85);

            powerupManager.Draw(spriteBatch);
        }

        public static State PauseUpdate()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.C))
            {
                MediaPlayer.Resume();
                return State.Run;
            }

            if (keyboardState.IsKeyDown(Keys.R))
            {
                foreach (var player in playerManager.players)
                {
                    player.deathCount = 0;
                    player.killCount = 0;
                    player.Health = 100;
                    player.Position = playerManager.RandomPos();
                    player.Velocity = Vector2.Zero;
                }

                MediaPlayer.Play(bgmusic);
                return State.Run;
            }

            if (keyboardState.IsKeyDown(Keys.M))
            {
                interfaceManager.timeCounter = 180f;

                foreach (var player in playerManager.players)
                {
                    player.Health = 100;
                    player.Position = playerManager.RandomPos();
                    player.killCount = 0;
                    player.deathCount = 0;
                    player.Velocity = Vector2.Zero;
                }

                return State.Menu;
            }

            if (keyboardState.IsKeyDown(Keys.Q))
                return State.Quit;

            return State.Pause;
        }

        public static void PauseDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pauseSprite, backgroundPos, Color.White);
        }


        public static State SubMenuUpdate()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.R))
            {
                foreach (var player in playerManager.players)
                {
                    player.deathCount = 0;
                    player.killCount = 0;
                    player.Health = 100;
                    player.Position = playerManager.RandomPos();
                    player.Velocity = Vector2.Zero;
                }

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
            spriteBatch.Draw(subMenuSprite, backgroundPos, Color.White);
            interfaceText.Draw(interfaceManager.winner, interfaceManager.interfaceTexts[0].font3, spriteBatch, 794, 220);
        }
    }
}
