using Microsoft.Xna.Framework;

namespace EdGeLib.Sprites
{
    public class PixelSprite : ISprite
    {
        public bool Visible { get; set; }
        public int RenderOrder { get; set; }
        public Vector2 Offset { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }

        public bool Has3DEffect { get; set; }

        public PixelSprite()
        {
            Visible = true;
            RenderOrder = 0;
            Offset = Vector2.Zero;
            Width = 0;
            Height = 0;
            Color = Color.HotPink;
            Has3DEffect = false;
        }

        public PixelSprite(int width, int height)
        {
            Visible = true;
            RenderOrder = 0;
            Offset = Vector2.Zero;
            Width = width;
            Height = height;
            Color = Color.HotPink;
            Has3DEffect = false;
        }
    }
}
