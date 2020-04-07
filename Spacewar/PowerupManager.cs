using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacewar
{
    //Ton
    class PowerupManager
    {
        Random rnd = new Random();
        double powerTimer = 10000; //10sec
        List<Powerup> powerUps = new List<Powerup>();

        int height;

        public Texture2D powerUpTexture { get; set; }

        public PowerupManager(int height, Texture2D powerup)
        {
            this.height = height;
            powerUpTexture = powerup;

        }

        public void add()
        {
            Powerup powerUp = new Powerup(powerUpTexture, new Vector2(rnd.Next(1500), rnd.Next(800)), Vector2.Zero ,new Point(50), height);

            powerUps.Add(powerUp);

        }

        public void Draw(SpriteBatch sb)
        {
            foreach (Powerup item in powerUps)
            {
                item.Draw(sb); 
            }
        }

        //timer för powerups
        public void Update(GameTime time)
        {
            powerTimer -= time.ElapsedGameTime.TotalMilliseconds;
            if (powerTimer < 0)
            {
                add();
                powerTimer = 10000;
            }
            
            foreach (Powerup item in powerUps)
            {
                item.Update();
            }

        }

    }
}
