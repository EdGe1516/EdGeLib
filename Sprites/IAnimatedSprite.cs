using Microsoft.Xna.Framework;

namespace EdGeLib.Sprites
{
    public interface IAnimatedSprite
    {
        bool IsAnimating { get; set; }
        void UpdateAnimation(GameTime gameTime);
    }
}
