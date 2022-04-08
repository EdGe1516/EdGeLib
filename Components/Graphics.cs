using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using EdGeLib.Sprites;

namespace EdGeLib.Components
{
    public class Graphics : Component
    {
        public List<ISprite> Sprites { get; set; }

        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                PositionChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public int X { get { return (int)Position.X; } } 
        public int Y { get { return (int)Position.Y; } }

        public int Width { get; set; }
        public int Height { get; set; }
        public Rectangle Bounds { get { return new Rectangle(X, Y, Width, Height); } }

        private bool visible;
        public bool Visible
        {
            get { return visible; }
            set
            {
                if (visible != value)
                {
                    visible = value;
                    VisibleChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler PositionChanged;
        public event EventHandler VisibleChanged;

        public Graphics(Entity entity)
            :base(entity)
        {
            Sprites = new List<ISprite>();
            position = Vector2.Zero;
            Width = 0;
            Height = 0;
            visible = true;
        }
    }
}
