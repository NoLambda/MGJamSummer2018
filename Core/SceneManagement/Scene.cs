using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MGJamSummer2018.Core
{
    public class Scene
    {
        public string Name { get; }
        protected EntityRoot rootEntity;

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