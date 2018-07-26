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
            GraphicsManager.Instance.SetResolution(new Vector2(640, 480), false);
            GraphicsManager.Instance.SetVirtualResolution(new Vector2(320, 180));
            GraphicsManager.Instance.SetMouseVisible(true);
            Content.RootDirectory = "Content";
            Start();
        }

        private void Start()
        {
            ScenesManager.Instance.Initialize(Content);

            ScenesManager.Instance.Populate(new List<Scene>() {new MainMenu("Menu"),
                                                               new GameScene("Game") });

            ScenesManager.Instance.SetStartingScene(ScenesManager.Instance.Container.Find(x => x.Name == "Menu"));
        }

        protected override void Update(GameTime gameTime)
        {
            ScenesManager.Instance.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            ScenesManager.Instance.Draw(GraphicsManager.Instance.SpriteBatch);
            base.Draw(gameTime);
        }
    }
}
