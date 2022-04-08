using Microsoft.Xna.Framework;

namespace EdGeLib.Sprites
{
    public interface ISprite
    {
        bool Visible { get; set; }
        int RenderOrder { get; set; }
        Vector2 Offset { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        Color Color { get; set; }
    }
}
