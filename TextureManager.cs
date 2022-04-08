using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EdGeLib
{
    public static class TextureManager
    {
        public static Dictionary<string, Texture2D> Textures { get; private set; }
        public static List<string> Names { get { return new List<string>(Textures.Keys); } }

        public static void Initialize(Game game)
        {
            Textures = new Dictionary<string, Texture2D>();
            string[] namesWithExtensions = Directory.GetFiles(game.Content.RootDirectory + @"\Textures\");
            foreach (string s in namesWithExtensions)
            {
                string name = Path.GetFileNameWithoutExtension(s);
                Texture2D texture = game.Content.Load<Texture2D>(@"Textures\" + name);
                texture.Name = name;
                Textures.Add(name, texture);
            }
            Texture2D pixel = new Texture2D(game.GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
            Textures.Add("Pixel", pixel);
        }
    }
}
