using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacewar
{
    class Player : GameObject
    {
        public Player(Texture2D texture, Vector2 pos, Vector2 velocity, Point size) : base(texture,pos,velocity,size)
        {

        }

        float rotation = 0; //Radians
        Vector2 vectorScale;
        int health;
        int deathCount;
        float fireRate;
        //Weapon weapon;
        float powerTimer;
        bool powerActivated = false;


        float maxSpeed = 15f;
        public void Thrust(float speed)
        {
            Velocity += new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation))*speed;
            Velocity = new Vector2(MathHelper.Clamp(Velocity.X, -maxSpeed, maxSpeed), MathHelper.Clamp(Velocity.Y, -maxSpeed, maxSpeed));
        }

        public void Turn(float rotate)
        {
            rotation += rotate;
            rotation %= 2 * (float)Math.PI;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture,Hitbox,new Rectangle(0,0,Texture.Width,Texture.Height),Color.White,rotation,new Vector2(Texture.Width/2,Texture.Height/2),SpriteEffects.None,0);
        }
    }
}
