﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spacewar
{
    //Alex
    class Healthbar
    {
        private Texture2D healthTexture;
        public Rectangle healthRectangle;
        public float health;
        public int maxHealth = 100;
        public int fullWidth = 406;

        public Healthbar(Texture2D healthTexture, Rectangle healthRectangle, float health)
        {
            this.healthTexture = healthTexture;
            this.healthRectangle = healthRectangle;
            this.health = health;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthTexture, healthRectangle, Color.White);
        }
    }
}
