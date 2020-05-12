﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacewar
{
    //Ton
    class PlayerSpeed : Powerup
    {
        float playerSpeed = +0.5f;

        public PlayerSpeed(Texture2D texture, Vector2 pos, Vector2 velocity, Point size, int height) : base(texture, pos, velocity, size, height)
        {


        }

        public override void DopowerUp(Player player)
        {
            player.Thrust(10f);
        }
    }
}
