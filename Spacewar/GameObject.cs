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

        int HEIGHT = 900; 
        //public Rectangle Hitbox { get { return new Rectangle(Position.ToPoint(), Size); } }
        public Rectangle Hitbox { get { return new Rectangle(new Point((int)Position.X,HEIGHT-(int)Position.Y), Size); } }

        public GameObject(Texture2D texture,Vector2 pos, Vector2 velocity, Point size)
        {
            Position = pos;
            Velocity = velocity;
            Size = size;
            Texture = texture;
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
