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
    class FireRate : Powerup
    {
        float fireRate = +0.2f;

        public FireRate(Texture2D texture, Vector2 pos, Vector2 velocity, Point size, int height) : base(texture, pos, velocity, size, height)
        {


        }
    }
}
