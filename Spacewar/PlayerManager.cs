using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spacewar
{
    //Gjord av Samuel
    public class PlayerManager
    {
        public PlayerManager(Texture2D texture1, Texture2D texture2,Point size1,Point size2,int WWidth,int WHeight)
        {
            random = new Random();
            players = new Player[2];
            Wsize = new Point(WWidth, WHeight);

            players[0] = new Player(texture1,RandomPos(),Vector2.Zero,size1,WWidth,WHeight);
            players[1] = new Player(texture2,RandomPos(),Vector2.Zero,size2,WWidth,WHeight);
        }

        //Function that pulls the players towards a position, strength of the pull dictated by the product of a constnat and the inverse square of the distance.
        public void Pull(Vector2 Position, float strength)
        {
            foreach(var player in players)
            {
                float distance = Vector2.Distance(player.Position, Position);
                float relativeStrength = strength / (float)Math.Sqrt(distance);
                var pullVector = relativeStrength*(Vector2.Normalize(new Vector2(Position.X-player.Position.X, Position.Y - player.Position.Y)));
                player.Velocity += pullVector;
            }
        }

        private Point Wsize;
        private Random random;
        public Player[] players;

        //Creates a random position for the player to spawn on.
        public Vector2 RandomPos() { return new Vector2(random.Next(0, Wsize.X), random.Next(0, Wsize.Y)); }
    }
}
