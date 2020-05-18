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
    class Health : Powerup
    {
        int health = +20;

        public Health(Texture2D texture, Vector2 pos, Vector2 velocity, Point size, int height) : base(texture, pos, velocity, size, height)
        {


        }

        public override void DopowerUp(Player player)
        {
            player.Health += 10; 
        }
    }
}
