using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacewar
{
    class Powerup : GameObject
    {
        Random randomPowerUp = new Random();
        int spawnTimer = 30000; //30sec 
        float playerSpeed = +0.5f;
        float fireRate = +0.2f;
        int health = +2;
        double powerTimer = 10000; //10sec

        public Powerup(Texture2D texture, Vector2 pos, Vector2 velocity, Point size) : base(texture, pos, velocity, size)
        {

            
        }

        public void Update(GameTime time) 
        {
            powerTimer -= time.ElapsedGameTime.TotalMilliseconds;
            if (powerTimer < 0) 
            {
                powerTimer = 10000;
            }
            base.Update();                               
        }
    }
}
