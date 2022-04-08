namespace EdGeLib.Sprites
{
    public class SpriteData
    {
        public string TextureName { get; set; }
        public int SourceX { get; set; }
        public int SourceY { get; set; }
        public int SourceWidth { get; set; }
        public int SourceHeight { get; set; }

        public SpriteData()
        {
            TextureName = "";
            SourceX = 0;
            SourceY = 0;
            SourceWidth = 0;
            SourceHeight = 0;
        }

        public SpriteData(Sprite sprite)
        {
            TextureName = sprite.TextureName;
            SourceX = sprite.SourceRectangle.X;
            SourceY = sprite.SourceRectangle.Y;
            SourceWidth = sprite.SourceRectangle.Width;
            SourceHeight = sprite.SourceRectangle.Height;
        }
    }
}
