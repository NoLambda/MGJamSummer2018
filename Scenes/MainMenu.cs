using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MGJamSummer2018.Core;

namespace MGJamSummer2018.Scenes
{
    public class MainMenu : Scene
    {
        public Texture2D texture;

        public MainMenu(string name) : base(name) { }

        public override void LoadContent()
        {
            texture = AssetManager.Instance.GetSprite("cube");
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, new Vector2(320 / 2, 180 / 2), Color.Red);
        }

        public override void Update(GameTime time)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                ScenesManager.Instance.SwitchScene(ScenesManager.Instance.Container.Find(x => x.Name == "Game"));
        }
    }
}