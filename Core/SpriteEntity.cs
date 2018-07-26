using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace MGJamSummer2018.Core
{
    class SpriteEntity : Entity
    {
        protected SpriteSheet spriteSheet;
        protected Vector2 origin;
        protected Color color = Color.White;
        protected int currIndex, layer;
        public SpriteEntity(string sheetPath, string _name, int sheetIndex = 0, Entity _parent = null, uint _layer = 0) : base(_name, _parent, _layer)
        {
            spriteSheet = new SpriteSheet(sheetPath, sheetIndex);
            origin = new Vector2(spriteSheet.Width / 2, spriteSheet.Height / 2);
        }

        public void SetAnchor(Anchor anchor)
        {
            switch (anchor)
            {
                case Anchor.UpLeft: origin = new Vector2(0, 0); break;
                case Anchor.Up: origin = new Vector2(spriteSheet.Width / 2, 0); break;
                case Anchor.UpRight: origin = new Vector2(spriteSheet.Width, 0); break;
                case Anchor.MiddleLeft: origin = new Vector2(0, spriteSheet.Height / 2); break;
                case Anchor.Middle: origin = new Vector2(spriteSheet.Width / 2, spriteSheet.Height / 2); break;
                case Anchor.MiddleRight: origin = new Vector2(spriteSheet.Width, spriteSheet.Height / 2); break;
                case Anchor.DownLeft: origin = new Vector2(0, spriteSheet.Height); break;
                case Anchor.Down: origin = new Vector2(spriteSheet.Width / 2, spriteSheet.Height); break;
                case Anchor.DownRight: origin = new Vector2(spriteSheet.Width, spriteSheet.Height); break;
            }
        }

        public Vector2 Origin { get => origin; }
        public Vector2 Centre { get => new Vector2(spriteSheet.Width / 2, spriteSheet.Height / 2); }
        public Color SpriteColor { get => color; set => color = value; }

        public int Width { get => spriteSheet.Width; }
        public int Height { get => spriteSheet.Height; }
        public bool Mirror { get => spriteSheet.Mirrored; set => spriteSheet.Mirrored = value; }

        public override Rectangle CollisionBox
        {
            get
            {
                int left = (int)(Position.X - origin.X);
                int top = (int)(Position.Y - origin.Y);
                return new Rectangle(left, top, Width, Height);
            }
        }

        public bool CollidesWith(SpriteEntity other)
        {
            return CollisionBox.Intersects(other.CollisionBox);
        }
    }

    public enum Anchor
    {
        UpLeft,
        Up,
        UpRight,
        MiddleLeft,
        Middle,
        MiddleRight,
        DownLeft,
        Down,
        DownRight
    }
}
