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
        }
    }
}
