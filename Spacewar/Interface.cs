using System;
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
        public int points;
        public int kills;

        public Interface(SpriteFont font, int points, int kills)
        {
            this.font = font;
            this.points = points;
            this.kills = kills;
        }

        public void Draw(string text, SpriteBatch spriteBatch, int X, int Y)
        {
            spriteBatch.DrawString(font, text, new Vector2(X, Y), Microsoft.Xna.Framework.Color.White);
        }


    }
}