using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spacewar
{
    class Healthbar
    {
        public int actualHealth;
        public int visibleHealth;

        public int fullWidth = 406;

        public Healthbar(Texture2D healthBarP1, Texture2D healthBarP2, int actualHealth, int visibleHealth)
        {
            this.actualHealth = actualHealth;
            this.visibleHealth = visibleHealth;
        }

        public void ResetHealth(int health)
        {
            actualHealth = visibleHealth = health;
        }

        public void Update()
        {
            if (actualHealth < visibleHealth)
            {
                visibleHealth -= 1;
            }
            else if (actualHealth != visibleHealth)
            {
                visibleHealth = actualHealth;
            }

             = (healthbar.actualHealth / 100) * fullWidth;
        }
    }
}
