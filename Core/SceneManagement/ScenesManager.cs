using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MGJamSummer2018.Core
{
    /// <summary>
    /// Manage all the scenes of the game.
    /// </summary>
    public class ScenesManager
    {
        /// <summary> Contain all the scenes of the game. </summary>
        public List<Scene> Container { get; }

        /// <summary> Current state of the game <see cref="SetStartingScene(Scene)"/> <see cref="SwitchScene(Scene)"/> </summary>
        public Scene CurrentScene { get; private set; }

        /// <summary> When the scene change fire this event. </summary>
        public event EventHandler<SceneEventArgs> SceneChanged;

        /// <summary> Make a singleton for the current class. </summary>
        public static ScenesManager Instance { get { return instance ?? (instance = new ScenesManager()); } }
        private static ScenesManager instance;

        /// <summary>
        /// Initialize the container at the start.
        /// </summary>
        public ScenesManager()
        {
            Container = new List<Scene>();
        }

        /// <summary>
        /// Update the current scene.
        /// </summary>
        public void Update(GameTime gameTime) => CurrentScene.Update(gameTime);

        /// <summary>
        /// Draw the current scene.
        /// If the game allow virtual resolution, render it with the virtual resolution
        /// else render it normally.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch, GameTime time)
        {
            if (GraphicsManager.Instance.AllowVirtualResolution)
            {
                GraphicsManager.Instance.GraphicsDeviceManager.GraphicsDevice.SetRenderTarget(GraphicsManager.Instance.RenderVirtualResolution);
                GraphicsManager.Instance.GraphicsDeviceManager.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
                GraphicsManager.Instance.GraphicsDeviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null , CameraManager.Instance.Transform);
                CurrentScene.Draw(time);
                spriteBatch.End();
                GraphicsManager.Instance.GraphicsDeviceManager.GraphicsDevice.SetRenderTarget(null);
                GraphicsManager.Instance.GraphicsDeviceManager.GraphicsDevice.Clear(Color.Black);
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Matrix.CreateScale(GraphicsManager.Instance.GraphicsDeviceManager.GraphicsDevice.Viewport.Width / GraphicsManager.Instance.VirtualResolution.X, GraphicsManager.Instance.GraphicsDeviceManager.GraphicsDevice.Viewport.Width / GraphicsManager.Instance.VirtualResolution.X, 1f) * Matrix.CreateTranslation(GraphicsManager.Instance.GraphicsDeviceManager.GraphicsDevice.Viewport.Width / 2, GraphicsManager.Instance.GraphicsDeviceManager.GraphicsDevice.Viewport.Height / 2, 0));
                spriteBatch.Draw(GraphicsManager.Instance.RenderVirtualResolution, new Vector2(0, 0), null, Color.White, 0f, new Vector2(GraphicsManager.Instance.RenderVirtualResolution.Width / 2, GraphicsManager.Instance.RenderVirtualResolution.Height / 2), 1f, SpriteEffects.None, 0f);
                spriteBatch.End();
            }
            else
            {
                GraphicsManager.Instance.GraphicsDeviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone);
                CurrentScene.Draw(time);
                spriteBatch.End();
            }
        }

        /// <summary>
        /// Add all the scenes from the list to the container.
        /// </summary>
        /// <param name="scenes">Scenes to add.</param>
        public void Populate(List<Scene> scenes)
        {
            Container.AddRange(scenes);
        }

        /// <summary>
        /// Set the default scene that you want to play when you
        /// start the game.
        /// </summary>
        public void SetStartingScene(Scene scene)
        {
            CurrentScene = scene;
            SceneChanged += scene.OnSceneChanged;
            OnSceneChanged();
        }

        /// <summary>
        /// Change from the current scene to the target scene.
        /// </summary>
        /// <param name="scene">The scene you want to play</param>
        public void SwitchScene(Scene scene)
        {
            CurrentScene.UnloadContent();
            Console.WriteLine($"[{CurrentScene.Name}]: Unloaded.");
            CurrentScene = scene;
            SceneChanged += scene.OnSceneChanged;
            OnSceneChanged();
        }

        /// <summary>
        /// Event, when scene changed fire OnSceneChanged()
        /// </summary>
        protected virtual void OnSceneChanged() => SceneChanged?.Invoke(this, new SceneEventArgs() { Scene = CurrentScene });
    }
}