using MGJamSummer2018.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGJamSummer2018.Entities
{
    public class Player : SpriteEntity
    {
        public bool IsGrounded { get; private set; }
        public bool IsCollided { get; private set; }

        private Grid gameWorld;
        private float movementSpeed;
        private const float gravity = 92;
        public const int feetPixelOffset = 2;

        public Player(Grid _gameWorld) : base("MainCharacter", "PLAYER", null, 0)
        {
            gameWorld = _gameWorld;
            ToggleAnimation();
            PlayAnimation("SNEAKING");
            LoopAnimation = true;
            movementSpeed = 64;
            SetAnchor(Anchor.Middle);
            localPos = new Vector2(20, -20);
        }

        public override void Update(GameTime gameTime)
        {
            CameraManager.Instance.Follow(this, .45f);
            // Movement etc.
            Input();
            if (!IsGrounded)
                Velocity -= new Vector2(0, -gravity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            Velocity *= movementSpeed;
            // Apply the Movement.
            Vector2 previousPos = localPos;
            base.Update(gameTime);
            WorldCollisionChecker();
            if (IsCollided)
                localPos = previousPos;
        }

        public void Draw(GameTime gTime, SpriteBatch spriteBatch)
        {
            base.Draw(gTime);
        }

        public void WorldCollisionChecker()
        {
            Rectangle feetBox = new Rectangle(BoundingBox.Left, BoundingBox.Bottom + feetPixelOffset, BoundingBox.Width, 0);
            IsGrounded = false;
            IsCollided = false;
            foreach(Layer l in gameWorld.Layers)
            {
                foreach(Cell c in l.Cells)
                {
                    if (c.CollidesWith(feetBox))
                        IsGrounded = true;
                    if (c.CollidesWith(BoundingBox))
                        IsCollided = true;
                        
                }
            }
        }
        public void Input()
        {
            Velocity = Vector2.Zero;
            if (InputManager.Instance.KeyDown(Keys.A))
            {
                Mirror = true;
                Velocity = new Vector2(-1, 0);
            }
            if (InputManager.Instance.KeyDown(Keys.D))
            {
                Mirror = false;
                Velocity = new Vector2(1, 0);
            }
            if(Velocity == Vector2.Zero) { PlayAnimation("IDLE"); } else { PlayAnimation("SNEAKING"); }
        }
    }
}
