using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EdGeLib.Sprites
{
    public class TextSprite : ISprite
    {
        public bool Visible { get; set; }
        public int RenderOrder { get; set; }
        public Vector2 Offset { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }

        public SpriteFont Font { get; set; }

        private string text;
        public string Text
        {
            get { return text; }
            set
            {
                if (text != value)
                {
                    text = value;
                    Vector2 size = Font.MeasureString(text);
                    Width = (int)size.X;
                    Height = (int)size.Y;
                    TextChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        public event EventHandler TextChanged;

        public TextSprite()
        {
            Visible = true;
            RenderOrder = 0;
            Offset = Vector2.Zero;
            Width = 0;
            Height = 0;
            Color = Color.White;
            Font = FontManager.Visitor18;
            text = "INIT";
        }
    }
}
