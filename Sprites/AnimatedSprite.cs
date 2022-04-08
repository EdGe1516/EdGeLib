using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace EdGeLib.Sprites
{
    public class AnimatedSprite : ISprite, IAnimatedSprite
    {
        public List<Sprite> Sprites;

        public bool Visible { get; set; }
        public int RenderOrder { get; set; }
        public Vector2 Offset { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color Color { get; set; }
        public bool IsAnimating { get; set; }

        public AnimatedSprite()
        {

        }

        public void UpdateAnimation(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
