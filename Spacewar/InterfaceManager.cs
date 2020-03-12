using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spacewar
{
    class InterfaceManager
    {
        public Healthbar[] healthBars;
        public InterfaceText[] interfaceTexts;

        public InterfaceManager(Texture2D p1HealthTexture, Texture2D p2HealthTexture, Rectangle p1HealthRectangle, Rectangle p2HealthRectangle, float p1Health, float p2Health, 
            SpriteFont font, int p1Points, int p1Kills, int p2Points, int p2Kills)
        {
            healthBars = new Healthbar[2];
            healthBars[0] = new Healthbar(p1HealthTexture, p1HealthRectangle, p1Health);
            healthBars[1] = new Healthbar(p2HealthTexture, p2HealthRectangle, p2Health);

            interfaceTexts = new InterfaceText[2];
            interfaceTexts[0] = new InterfaceText(font, p1Points, p1Kills);
            interfaceTexts[1] = new InterfaceText(font, p2Points, p2Kills);
        }
    }
}
