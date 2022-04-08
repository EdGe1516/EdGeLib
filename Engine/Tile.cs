using Microsoft.Xna.Framework;
using EdGeLib.Sprites;

namespace EdGeLib.Engine
{
    public class Tile : Entity
    {
        public Point Location { get; private set; }

        private ISprite terrain;
        public ISprite Terrain
        {
            get { return terrain; }
            set
            {
                terrain = value;
                Graphics.Sprites[0] = value;
            }
        }

        private ISprite feature;
        public ISprite Feature
        {
            get { return feature; }
            set
            {
                feature = value;
                Graphics.Sprites[1] = value;
            }
        }

        public Tile(Point location)
        {
            Location = location;
            Graphics.Sprites.Add(new PixelSprite());
            Graphics.Sprites.Add(new PixelSprite());
            Feature = Graphics.Sprites[0];
            Terrain = Graphics.Sprites[1];
        }
    }
}
