using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace EdGeLib.Components
{
    public enum DockDirection
    {
        Down,
        Right,
    }
    public class Family : Component
    {
        public Entity Parent { get; private set; }
        public List<Entity> Children { get; private set; }
        //can entity be used as a key
        public Dictionary<Entity, Vector2> Docks { get; private set; }

        public Family(Entity entity)
            : base(entity)
        {
            Parent = null;
            Children = new List<Entity>();
            Docks = new Dictionary<Entity, Vector2>();
            Entity.Graphics.PositionChanged += Graphics_PositionChanged;
            Entity.Graphics.VisibleChanged += Graphics_VisibleChanged;
        }

        public List<Entity> Dependants()
        {
            List<Entity> dependants = new List<Entity>();
            if (Children.Count > 0)
            {
                List<Entity> parents = new List<Entity>
                {
                    Entity
                };
                while (parents.Count > 0)
                {
                    Entity parent = parents[0];
                    foreach (Entity child in parent.Family.Children)
                    {
                        dependants.Add(child);
                        if (child.Family.Children.Count > 0)
                        {
                            parents.Add(child);
                        }
                    }
                    parents.Remove(parent);
                }
            }
            return dependants;
        }

        public void AddChild(Entity child, Vector2 offset)
        {
            child.Family.Parent = Entity;
            Children.Add(child);
            Docks.Add(child, offset);
        }

        public void AddDock(Entity dock, DockDirection dockDirection, int distance)
        {
            Vector2 offset = Vector2.Zero;
            switch (dockDirection)
            {
                case DockDirection.Down:
                    offset.Y = distance + Entity.Graphics.Height;
                    break;
                case DockDirection.Right:
                    offset.X = distance + Entity.Graphics.Width;
                    break;
            }
            Docks.Add(dock, offset);
        }

        public void ChangeDock(Entity dock, Vector2 offset)
        {
            Docks[dock] = offset;
            Graphics_PositionChanged(Entity.Graphics, EventArgs.Empty);
        }

        private void Graphics_PositionChanged(object sender, System.EventArgs e)
        {
            if (Docks.Count > 0)
            {
                foreach (Entity dock in Docks.Keys)
                {
                    dock.Graphics.Position = Entity.Graphics.Position + Docks[dock];
                }
            }
        }

        private void Graphics_VisibleChanged(object sender, System.EventArgs e)
        {
            if (Children.Count > 0)
            {
                foreach (Entity child in Children)
                {
                    child.Graphics.Visible = Entity.Graphics.Visible;
                }
            }
        }
    }
}
