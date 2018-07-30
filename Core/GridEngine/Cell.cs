using MGJamSummer2018.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGJamSummer2018.Core
{
    public class Cell
    {
        /// <summary> Texture of the cell. </summary>
        public Texture2D Texture { get; set; }

        /// <summary> Position of the cell. </summary>
        public Vector2 Position { get; set; }

        /// <summary> Collision of the cell. </summary>
        public Rectangle BoundingBox { get; set; }

        /// <summary> Is the cell collideable ?. </summary>
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
