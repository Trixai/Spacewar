using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacewar
{
    public class GameObject
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Point Size { get; set; }
        public float Radius { get; set; }

        private int WHeight;
        public Rectangle Hitbox{ get { return new Rectangle(new Point((int)Position.X,WHeight-(int)Position.Y), Size); } }

        public bool HitCircular(float radius,Vector2 position)
        {
            var dx = Position.X - position.X;
            var dy = Position.Y - position.Y;
            var distance = Math.Sqrt(dx * dx + dy * dy);

            return (distance < Radius + radius);
        }

        public GameObject(Texture2D texture,Vector2 pos, Vector2 velocity, Point size, int windowHeight)
        {
            Position = pos;
            Velocity = velocity;
            Size = size;
            Texture = texture;
            WHeight = windowHeight;
            Radius = (size.X + size.Y) / 4f;
        }

        public virtual void Update()
        {
            Position += Velocity;
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, Hitbox, Color.White);
        }
    }
}
