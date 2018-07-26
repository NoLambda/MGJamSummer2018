using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGJamSummer2018.Core
{
    public abstract class Entity : IBasicObject
    {
        protected Entity parent;
        protected EntityList children;
        protected Vector2 pos, vel;
        protected string name;
        protected uint layer;

        public Entity(string _name, Entity _parent = null, uint _layer = 0)
        {
            children = new EntityList(this);
            name = _name;
            parent = _parent;
        }

        public virtual void Reset() { children.Reset();}
        public virtual void Update(GameTime gTime)
        {
            children.Update(gTime);
            pos = vel * (float)gTime.ElapsedGameTime.TotalSeconds;
        }
        public virtual void Draw(GameTime gTime) { children.Draw(gTime); }

        public Entity Parent { get => (parent ?? this); set => parent = value; }
        public EntityList Children { get => children; }
        
        public virtual Vector2 Position { get => parent == null ? LocalPosition : parent.Position + LocalPosition; }
        public virtual Vector2 LocalPosition { get => pos; set => pos = value; }
        public virtual Vector2 Velocity { get => vel; set => vel = value; }

        public string Name { get => name; }
        public uint DrawingLayer { get => layer; set => layer = value; }
        public virtual Rectangle CollisionBox { get => new Rectangle((int)Position.X, (int)Position.Y, 0, 0); }
    }
}
