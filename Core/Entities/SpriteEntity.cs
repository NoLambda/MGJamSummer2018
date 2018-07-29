using System;
using Microsoft.Xna.Framework;
using MGJamSummer2018.Core;
using Microsoft.Xna.Framework.Graphics;
namespace MGJamSummer2018.Entities
{
    public class SpriteEntity : Entity
    {
        protected SpriteSheet spriteSheet;
        protected Vector2 origin;
        protected Color color = Color.White;
        protected int currIndex;
        public SpriteEntity(string sheetPath, string _name, Entity _parent = null, uint _layer = 0) : base(_name, _parent, _layer)
        {
            spriteSheet = AssetManager.Instance.GetSpriteSheet(sheetPath);
            origin = new Vector2(spriteSheet.Width / 2, spriteSheet.Height / 2);
        }

        public override void Update(GameTime gTime)
        {
            base.Update(gTime);
            spriteSheet.Update(gTime);
        }

        public override void Draw(GameTime gTime)
        {
            base.Draw(gTime);
            spriteSheet.Draw(Position, origin, color);
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

        public void PlayAnimation(string name) { spriteSheet.PlayAnimation(name); }
        public void ToggleAnimation() => spriteSheet.ToggleAnimation();
        public bool LoopAnimation { get => spriteSheet.LoopAnimation; set => spriteSheet.LoopAnimation = value; }

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
