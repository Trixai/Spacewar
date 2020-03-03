using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacewar
{
    class Player : GameObject
    {
        public Player(Texture2D texture, Vector2 pos, Vector2 velocity, Point size, int Wheight) : base(texture,pos,velocity,size,Wheight)
        {

        }

        float rotation = 0; //Radians
        Vector2 vectorScale;
        public int health { get; private set; }
        int deathCount;
        float fireRate;
        //Weapon weapon;
        float powerTimer;
        bool powerActivated = false;

        float maxSpeed = 15f;
        public void Thrust(float speed)
        {
            Velocity += new Vector2((float)Math.Cos(-rotation), (float)Math.Sin(-rotation))*speed;
            Velocity = new Vector2(MathHelper.Clamp(Velocity.X, -maxSpeed, maxSpeed), MathHelper.Clamp(Velocity.Y, -maxSpeed, maxSpeed));
        }

        public Vector2 CalculateX(int width, int height)
        {
            var normal = Vector2.Normalize(Velocity);
            var angle = Math.Atan(normal.Y/normal.X);
            float x, y, k = 0;
            float tempx = 0;

            k = Position.Y - (float)Math.Tan(angle) * Position.X;

            if (Position.Y > height)
            {
                tempx = (-k) / (float)Math.Tan(angle);

                if(tempx<0)
                {
                    y = k;
                    x = 0;
                }
                else if(tempx>width)
                {
                    y = (float) Math.Tan(angle) * width + k;
                    x = width;
                }
                else
                {
                    y = 0;
                    x = tempx;
                }

            }
            else
            {
                tempx = (height - k) / (float)Math.Tan(angle);
                if (tempx < 0)
                {
                    y = k;
                    x = 0;
                }
                else if (tempx > width)
                {
                    y = (float)Math.Tan(angle) * width + k;
                    x = width;
                }
                else
                {
                    y = height;
                    x = tempx;
                }
            }
            return new Vector2(x, y);
        }
        public Vector2 CalculateY(int width, int height)
        {
            var normal = Vector2.Normalize(Velocity);
            var angle = Math.Atan(normal.Y / normal.X);
            float x, y, k = 0;
            float tempy = 0;

            k = Position.Y - (float)Math.Tan(angle) * Position.X;

            if(Position.X>width)
            {
                tempy = k;
                if (tempy<0)
                {
                    x = (-k)/(float)Math.Tan(angle);
                    y = 0;
                }
                else if(tempy>height)
                {
                    x = (height - k) / (float)Math.Tan(angle);
                    y = height;
                }
                else
                {
                    x = 0;
                    y = k;
                }
            }
            else
            {
                tempy = width*(float)Math.Tan(angle)+ k;
                if (tempy < 0)
                {
                    x = (-k) / (float)Math.Tan(angle);
                    y = 0;
                }
                else if (tempy > height)
                {   
                    x = (height - k) / (float)Math.Tan(angle);
                    y = height;
                }
                else
                {
                    x = width;
                    y = width*(float)Math.Tan(angle)+k;
                }
            }

            return new Vector2(x, y);
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

        public bool Intersect(Rectangle rect)
        {
            return Hitbox.Intersects(rect);
        }
    }
}
