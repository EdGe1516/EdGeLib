using Microsoft.Xna.Framework;

namespace EdGeLib.Sprites
{
    public class Sprite : ISprite
    {
        public bool Visible { get; set; }
        public int RenderOrder { get; set; }
        public Vector2 Offset { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }

        public string TextureName { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public float Scale { get; set; }

        public Sprite()
        {
            Visible = true;
            RenderOrder = 0;
            Offset = Vector2.Zero;
            Width = 16;
            Height = 16;
            Color = Color.White;
            TextureName = "Pixel";
            SourceRectangle = new Rectangle(1,1,1,1);
            Scale = 1.0f;
        }

        public Sprite(SpriteData spriteData)
        {
            Visible = true;
            RenderOrder = 0;
            Offset = Vector2.Zero;
            Width = spriteData.SourceWidth;
            Height = spriteData.SourceHeight;
            Color = Color.White;
            TextureName = spriteData.TextureName;
            SourceRectangle = new Rectangle(
                spriteData.SourceX,
                spriteData.SourceY,
                spriteData.SourceWidth,
                spriteData.SourceHeight);
            Scale = 1.0f;
        }

        public SpriteData GetSpriteData()
        {
            return new SpriteData(this);
        }
    }
}
