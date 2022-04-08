using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EdGeLib
{
    public static class FontManager
    {
        public static SpriteFont Visitor12 { get; private set; }
        public static SpriteFont Visitor18 { get; private set; }
        public static SpriteFont Visitor24 { get; private set; }
        public static SpriteFont YosterIsland12 { get; private set; }
        public static SpriteFont YosterIsland18 { get; private set; }

        public static void Initialize(Game game)
        {
            Visitor12 = game.Content.Load<SpriteFont>(@"Fonts\Visitor12");
            Visitor18 = game.Content.Load<SpriteFont>(@"Fonts\Visitor18");
            Visitor24 = game.Content.Load<SpriteFont>(@"Fonts\Visitor24");
            YosterIsland12 = game.Content.Load<SpriteFont>(@"Fonts\YosterIsland12");
            YosterIsland18 = game.Content.Load<SpriteFont>(@"Fonts\YosterIsland18");
        }
    }
}
