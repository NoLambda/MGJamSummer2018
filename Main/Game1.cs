﻿using Microsoft.Xna.Framework;
using MGJamSummer2018.Core;
using MGJamSummer2018.Scenes;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace MGJamSummer2018
{
    public class Game1 : Game
    {
        public Game1()
        {
            GraphicsManager.Instance.Init(this);
            GraphicsManager.Instance.SetWindowTitle("NoLambda Team | MGJam Summer 2018");
            GraphicsManager.Instance.SetResolution(new Vector2(320*2, 180*2), false);
            GraphicsManager.Instance.SetVirtualResolution(new Vector2(320, 180));
            GraphicsManager.Instance.SetMouseVisible(true);
            Content.RootDirectory = "Content";
            Start();
        }

        private void Start()
        {
            AssetManager.Instance.Init(Content);

            /// <summary> Add the game scenes. </summary>
            ScenesManager.Instance.Populate(new List<Scene>() {new MainMenu("Menu"),
                                                               new GameScene("Game") });

            /// <summary> Choose the scene you want to play at start. </summary>
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
            ScenesManager.Instance.Draw( GraphicsManager.Instance.SpriteBatch, gameTime);
            base.Draw(gameTime);
        }
    }
}
