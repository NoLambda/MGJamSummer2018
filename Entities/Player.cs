using MGJamSummer2018.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGJamSummer2018.Entities
{
    public class Player : SpriteEntity
    {
        public bool isGrounded { get; set; }

        private float movementSpeed;
        private const float gravity = 92;

        public Player() : base("MainCharacter", "PLAYER", null, 0)
        {
            ToggleAnimation();
            PlayAnimation("SNEAKING");
            LoopAnimation = true;
            movementSpeed = 64;
            localPos = new Vector2(20, 5);
        }

        public override void Update(GameTime gameTime)
        {
            CameraManager.Instance.Follow(this, .45f);
            Input();
            if (!isGrounded)
                    Velocity -= new Vector2(0, -gravity * (float)gameTime.ElapsedGameTime.TotalSeconds);
            Velocity *= movementSpeed;
            base.Update(gameTime);
        }

        public void Draw(GameTime gTime, SpriteBatch spriteBatch)
        {
            base.Draw(gTime);
        }

        public void Input()
        {
            Velocity = Vector2.Zero;
            if (InputManager.Instance.KeyDown(Keys.A))
            {
                Mirror = true;
                Velocity += new Vector2(-1, 0);
            }
            if (InputManager.Instance.KeyDown(Keys.D))
            {
                Mirror = false;
                Velocity += new Vector2(1, 0);
            }
            if(Velocity == Vector2.Zero) { PlayAnimation("IDLE"); } else { PlayAnimation("SNEAKING"); }
        }
    }
}
