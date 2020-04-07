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
        public float timeCounter;
        public string timeText = "";
        public bool end;
        public string winner;

        public InterfaceManager(Texture2D p1HealthTexture, Texture2D p2HealthTexture, Rectangle p1HealthRectangle, Rectangle p2HealthRectangle, float p1Health, float p2Health, 
            SpriteFont font1, SpriteFont font2, int p1Points, int p1Kills, int p2Points, int p2Kills, float timeCounter)
        {
            healthBars = new Healthbar[2];
            healthBars[0] = new Healthbar(p1HealthTexture, p1HealthRectangle, p1Health);
            healthBars[1] = new Healthbar(p2HealthTexture, p2HealthRectangle, p2Health);

            interfaceTexts = new InterfaceText[2];
            interfaceTexts[0] = new InterfaceText(font1, font2, p1Points, p1Kills);
            interfaceTexts[1] = new InterfaceText(font1, font2, p2Points, p2Kills);

            this.timeCounter = timeCounter;
        }

        public void Timer(GameTime gameTime)
        {
            timeCounter -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeCounter <= 0)
            {
                timeCounter = 180f;
                end = true;
            }

            if (timeCounter >= 120)
            {
                if (Convert.ToString(Math.Round(timeCounter - 120f)).Length < 2)
                {
                    timeText = "02:0" + Convert.ToString(Math.Round(timeCounter - 120f));
                }
                else
                {
                    timeText = "02:" + Convert.ToString(Math.Round(timeCounter - 120f));
                }
            }
            else if (timeCounter >= 60)
            {
                if (Convert.ToString(Math.Round(timeCounter - 60f)).Length < 2)
                {
                    timeText = "01:0" + Convert.ToString(Math.Round(timeCounter - 60f));
                }
                else
                {
                    timeText = "01:" + Convert.ToString(Math.Round(timeCounter - 60f));
                }
            }
            else
            {
                if (Convert.ToString(Math.Round(timeCounter)).Length < 2)
                {
                    timeText = "00:0" + Convert.ToString(Math.Round(timeCounter));
                }
                else
                {
                    timeText = "00:" + Convert.ToString(Math.Round(timeCounter));
                }
            }
        }

        public void Winner()
        {
            if (interfaceTexts[0].points > interfaceTexts[1].points)
            {
                winner = "Player 1";
            }
            else if (interfaceTexts[1].points > interfaceTexts[0].points)
            {
                winner = "Player 2";
            }
        }
    }
}
