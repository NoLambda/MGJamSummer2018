using MGJamSummer2018.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGJamSummer2018.Entities
{
    public class Player
    {
        public Texture2D Texture { get; set; }
        public Rectangle Bounds { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        public bool isGrounded { get; set; }

        private float pixelsPerSeconds;
        private float gravity;

        public Player()
        {
            pixelsPerSeconds = 64;
            gravity = 92;
        }

        public void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("cube");
        }

        public void Update(GameTime gameTime)
        {
            CameraManager.Instance.Follow(this, .45f);

            Velocity = Vector2.Zero;

            Bounds = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);

            if (isGrounded)
            {
                System.Console.WriteLine(" grounded");
            }
            else
            {
                Position -= new Vector2(0, -gravity * (float)gameTime.ElapsedGameTime.TotalSeconds);
                System.Console.WriteLine("not grounded!");
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left)) Velocity = new Vector2(-1,0);
            if (Keyboard.GetState().IsKeyDown(Keys.Right)) Velocity = new Vector2(1, 0);

            Position +=  Velocity * pixelsPerSeconds *  (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
