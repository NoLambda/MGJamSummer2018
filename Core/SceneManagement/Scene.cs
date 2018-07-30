using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using MGJamSummer2018.Entities;
using System;

namespace MGJamSummer2018.Core
{
    /// <summary>
    /// Scene base class.
    /// </summary>
    public class Scene
    {
        /// <summary> Name of the scene. </summary>
        public string Name { get; }

        // TODO: Lightning
        protected EntityRoot rootEntity;

        /// <summary>
        /// Create a new rootEntity when a new scene is created,
        /// and assign it a name.
        /// </summary>
        /// <param name="name">The name to assign.</param>
        public Scene(string name)
        {
            Name = name;
            rootEntity = new EntityRoot();
        }

        public virtual void Initialize() { }

        public virtual void LoadContent(ContentManager content) { }

        public virtual void UnloadContent() { }

        public virtual void Update(GameTime gTime)
        {
            rootEntity.Update(gTime);
        }

        public virtual void Draw(GameTime gTime)
        {
            rootEntity.Draw(gTime);
        }

        /// <summary>
        /// When the event OnSceneChanged is fired (from the source (Scenes Manager)
        /// call this method.
        /// </summary>
        /// <param name="source">event owner (Scenes Manager)</param>
        /// <param name="args">Scenes args...</param>
        public void OnSceneChanged(object source, SceneEventArgs args)
        {
            args.Scene.Initialize();
            args.Scene.LoadContent(AssetManager.Instance.GetContentManager);
            Console.WriteLine($"[{args.Scene.Name}]: Initialized.\n" +
                              $"[{args.Scene.Name}]: Loaded.\n" +
                              $"[{args.Scene.Name}]: Performing draw update loop.");
            ScenesManager.Instance.SceneChanged -= args.Scene.OnSceneChanged;
        }
    }
}