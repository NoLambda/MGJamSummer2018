using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MGJamSummer2018.Core
{
    public class Scene
    {
        public string Name { get; }

        public Scene(string name)
        {
            Name = name;
        }

        public virtual void Initialize() { }

        public virtual void LoadContent(ContentManager content) { }

        public virtual void UnloadContent() { }

        public virtual void Update(GameTime time) { }

        public virtual void Draw(SpriteBatch batch) { }

        public void OnSceneChanged(object source, SceneEventArgs args)
        {
            args.Scene.Initialize();
            args.Scene.LoadContent(ScenesManager.Instance.ContentManager);
            Console.WriteLine($"[{args.Scene.Name}]: Initialized.\n" +
                              $"[{args.Scene.Name}]: Loaded.\n" +
                              $"[{args.Scene.Name}]: Performing draw update loop.");
            ScenesManager.Instance.SceneChanged -= args.Scene.OnSceneChanged;
        }
    }
}