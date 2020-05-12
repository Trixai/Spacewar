using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spacewar
{
    //Alex
    class InterfaceText
    {
        public SpriteFont font1;
        public SpriteFont font2;
        public SpriteFont font3;
        public int points;
        public int kills;
        public int deaths;

        public InterfaceText(SpriteFont font1, SpriteFont font2, SpriteFont font3, int points, int kills, int deaths)
        {
            this.font1 = font1;
            this.font2 = font2;
            this.font3 = font3;
            this.points = points;
            this.kills = kills;
            this.deaths = deaths;
        }

        public void Draw(string text, SpriteFont font, SpriteBatch spriteBatch, int X, int Y)
        {
            spriteBatch.DrawString(font, text, new Vector2(X, Y), Microsoft.Xna.Framework.Color.White);
        }
    }
}