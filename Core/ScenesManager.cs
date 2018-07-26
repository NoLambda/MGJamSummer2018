using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MGJamSummer2018.Core
{
    public class ScenesManager
    {
        public List<Scene> Container { get; }
        public Scene CurrentScene { get; private set; }
        public ContentManager ContentManager { get; private set; }

        public event EventHandler<SceneEventArgs> SceneChanged;

        public static ScenesManager Instance { get { return instance ?? (instance = new ScenesManager()); } }
        private static ScenesManager instance;

        public ScenesManager()
        {
            Container = new List<Scene>();
        }

        public void Initialize(ContentManager contentManager)
        {
            ContentManager = contentManager;
        }

        public void Update(GameTime gameTime) => CurrentScene.Update(gameTime);

        public void Draw(SpriteBatch spriteBatch)
        {
            if (GraphicsManager.Instance.AllowVirtualResolution)
            {
                GraphicsManager.Instance.GraphicsDeviceManager.GraphicsDevice.SetRenderTarget(GraphicsManager.Instance.RenderVirtualResolution);
                GraphicsManager.Instance.GraphicsDeviceManager.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
                GraphicsManager.Instance.GraphicsDeviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
                CurrentScene.Draw(spriteBatch);
                spriteBatch.End();
                GraphicsManager.Instance.GraphicsDeviceManager.GraphicsDevice.SetRenderTarget(null);
                GraphicsManager.Instance.GraphicsDeviceManager.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.CreateScale(GraphicsManager.Instance.GraphicsDeviceManager.GraphicsDevice.Viewport.Width / GraphicsManager.Instance.VirtualResolution.X, GraphicsManager.Instance.GraphicsDeviceManager.GraphicsDevice.Viewport.Width / GraphicsManager.Instance.VirtualResolution.X, 1f) * Matrix.CreateTranslation(GraphicsManager.Instance.GraphicsDeviceManager.GraphicsDevice.Viewport.Width / 2, GraphicsManager.Instance.GraphicsDeviceManager.GraphicsDevice.Viewport.Height / 2, 0));
                spriteBatch.Draw(GraphicsManager.Instance.RenderVirtualResolution, new Vector2(0, 0), null, Color.White, 0f, new Vector2(GraphicsManager.Instance.RenderVirtualResolution.Width / 2, GraphicsManager.Instance.RenderVirtualResolution.Height / 2), 1f, SpriteEffects.None, 0f);
                spriteBatch.End();
            }
            else
            {
                GraphicsManager.Instance.GraphicsDeviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone);
                CurrentScene.Draw(spriteBatch);
                spriteBatch.End();
            }
        }

        public void Populate(List<Scene> scenes)
        {
            Container.AddRange(scenes);
        }

        public void SetStartingScene(Scene scene)
        {
            CurrentScene = scene;
            SceneChanged += scene.OnSceneChanged;
            OnSceneChanged();
        }

        public void SwitchScene(Scene scene)
        {
            CurrentScene.UnloadContent();
            Console.WriteLine($"[{CurrentScene.Name}]: Unloaded.");
            CurrentScene = scene;
            SceneChanged += scene.OnSceneChanged;
            OnSceneChanged();
        }

        protected virtual void OnSceneChanged() => SceneChanged?.Invoke(this, new SceneEventArgs() { Scene = CurrentScene });
    }
}