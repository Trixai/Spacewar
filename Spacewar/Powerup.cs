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
        float powerTimer = +20.4f;

        public PowerUps(Texture2D texture, Vector2 position): base(texture, position)
        {

            
        }
    }
}
