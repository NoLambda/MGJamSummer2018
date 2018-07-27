using MGJamSummer2018.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGJamSummer2018.Scenes
{
    public class GameScene : Scene
    {
        public Texture2D texture;

        public GameScene(string name) : base(name){ }

        public override void LoadContent()
        {
            texture = AssetManager.Instance.GetSprite("cube");
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, new Vector2(320 / 2, 180 / 2), Color.White);
        }
    }
}