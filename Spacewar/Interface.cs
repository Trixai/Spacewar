﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spacewar
{
    class Interface
    {
        public SpriteFont font;
        public int health;

        public Interface(SpriteFont font, int health)
        {
            this.font = font;
            this.health = health;
        }

        public void Draw(string text, SpriteBatch spriteBatch, int X, int Y)
        {
            spriteBatch.DrawString(font, text, new Vector2(X, Y), Microsoft.Xna.Framework.Color.White);
        }


    }
}