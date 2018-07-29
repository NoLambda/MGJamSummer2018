using MGJamSummer2018.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGJamSummer2018.Core
{
    public class Cell
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public Rectangle BoundingBox { get; set; }
        public bool Collideable { get; set; }

        public Cell(Texture2D texture, Vector2 position, bool collideable)
        {
            Texture = texture;
            Position = position;
            Collideable = collideable;
            BoundingBox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        public bool CollidesWith(Rectangle coll)
        {
            if (Collideable && coll.Intersects(BoundingBox))
                return true;
            return false;
        }
    }
}
