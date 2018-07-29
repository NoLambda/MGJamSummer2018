using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGJamSummer2018.Entities
{
    public abstract class Entity : IBasicObject
    {
        protected Entity parent;
        protected EntityList children;
        protected Vector2 localPos, vel;
        protected string name;
        protected uint layer;
        protected bool visible = true;

        public Entity(string _name, Entity _parent, uint _layer = 0)
        {
            children = new EntityList(this);
            name = _name;
            parent = _parent;
        }

        public virtual void Reset() { children.Reset();}
        public virtual void Update(GameTime gTime)
        {
            if(!visible) { return; }
            children.Update(gTime);
            localPos += vel * (float)gTime.ElapsedGameTime.TotalSeconds;
        }
        public virtual void Draw(GameTime gTime)
        {
            if(!visible) { return; }
            children.Draw(gTime);
        }

        public bool IsVisible { get => visible; set => visible = value; }

        public virtual Vector2 Position { get => parent == null ? LocalPosition : parent.Position + LocalPosition; }
        public virtual Vector2 LocalPosition { get => localPos; set => localPos = value; }
        public virtual Vector2 Velocity { get => vel; set => vel = value; }

        public string Name { get => name; }
        public uint DrawingLayer { get => layer; set => layer = value; }
        public virtual Rectangle CollisionBox { get => new Rectangle((int)Position.X, (int)Position.Y, 0, 0); }

        public virtual Entity Parent { get => parent; set => parent = value; }
        public EntityList Children { get => children; }
        public void AddChild(Entity e) => children.Add(e);
        public void ClearChildren() => children.Clear();
    }

    public class EntityRoot : Entity
    {
        public EntityRoot() : base("ROOT", null, 0){ }

        public Entity RootSearch(string _name)
        {
            foreach (Entity e in children.Entities)
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

        public override Entity Parent { get => null; }
    }
}
