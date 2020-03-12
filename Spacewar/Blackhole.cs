using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacewar
{
    public class Blackhole : GameObject
    {
        public Blackhole(Texture2D texture, Vector2 pos, Vector2 velocity, Point size, int Wheight, float force) : base(texture, pos, velocity, size, Wheight)
        {
            Force = force;
        }

        public float Force;
    }
}
