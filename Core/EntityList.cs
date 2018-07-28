using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MGJamSummer2018.Core
{
    public class EntityList : IBasicObject
    {
        protected List<Entity> entities;
        protected Entity owner;
        public EntityList(Entity _owner = null)
        { entities = new List<Entity>(); owner = _owner; }

        public List<Entity> Entities { get => entities; }
        public void Add(Entity e)
        {
            e.Parent = owner;
            entities.Add(e);
            entities.Sort((x, y) => x.DrawingLayer.CompareTo(y.DrawingLayer));
        }
        public void Remove(Entity e) { entities.Remove(e); e.Parent = null; }

        public Entity Search(string _name)
        {
            foreach(Entity e in entities)
            {
                if (e.Name == _name)
                    return e;
                else
                {
                    if (e.Children != null)
                        e.Children.Search(_name);
                }
            }
            return null;
        }

        public void Draw(GameTime gTime)
        {
            for (int i = entities.Count - 1; i >= 0; i--)
                entities[i].Draw(gTime);
        }

        public void Update(GameTime gTime)
        {
            for (int i = entities.Count - 1; i >= 0; i--)
                entities[i].Update(gTime);
        }

        public void Reset()
        {
            for (int i = entities.Count - 1; i >= 0; i--)
                entities[i].Reset();
        }

        public void Clear() => entities.Clear();

    }
}
