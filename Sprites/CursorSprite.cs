using Microsoft.Xna.Framework;

namespace EdGeLib.Sprites
{
    public class CursorSprite : PixelSprite, IAnimatedSprite
    {
        public readonly Vector2 DefaultOffset;
        public bool IsAnimating { get; set; }

        public CursorSprite()
        {
            Visible = false;
            RenderOrder = 3;
            DefaultOffset = new Vector2(8, 7);
            Offset = DefaultOffset;
            Width = 2;
            Height = 18;
            Color = Color.White;
            IsAnimating = false;
        }

        float elapsed = 0f;
        readonly float blinkRate = .5f;
        public void UpdateAnimation(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsed >= blinkRate)
            {
                Visible = !Visible;
                elapsed = 0f;
            }
        }
    }
}
