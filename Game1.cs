using Microsoft.Xna.Framework;
using MGJamSummer2018.Core;
using MGJamSummer2018.Scenes;
using System.Collections.Generic;

namespace MGJamSummer2018
{
    public class Game1 : Game
    {
        public Game1()
        {
            GraphicsManager.Instance.Init(this);
            GraphicsManager.Instance.SetWindowTitle("NoLambda Team | MGJam Summer 2018");
            GraphicsManager.Instance.SetResolution(new Vector2(640*1.5f, 360*1.5f), false);
            GraphicsManager.Instance.SetVirtualResolution(new Vector2(320, 180));
            GraphicsManager.Instance.SetMouseVisible(true);
            Content.RootDirectory = "Content";
            Start();
        }

        private void Start()
        {
            AssetManager.Instance.Init(Content);

            ScenesManager.Instance.Populate(new List<Scene>() {new MainMenu("Menu"),
                                                               new GameScene("Game") });

            ScenesManager.Instance.SetStartingScene(ScenesManager.Instance.Container.Find(x => x.Name == "Game"));
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.Instance.Update();
            ScenesManager.Instance.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            ScenesManager.Instance.Draw(gameTime, GraphicsManager.Instance.SpriteBatch);
            base.Draw(gameTime);
        }
    }
}
