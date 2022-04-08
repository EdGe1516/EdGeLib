using Microsoft.Xna.Framework;
using EdGeLib.Components;
using EdGeLib.Sprites;

namespace EdGeLib.GUI
{
    public abstract class Control : Entity
    {
        public ISprite Background { get; set; }
        public PixelSprite ContentRectangle { get; set; }
        public int Padding { get; set; }

        public Control()
        {
            Background = new PixelSprite()
            {
                Visible = false,
                Has3DEffect = true
            };
            Graphics.Sprites.Add(Background);

            ContentRectangle = new PixelSprite()
            {
                Visible = false,
                RenderOrder = 1,
                Color = Color.DarkSlateGray
            };
            Graphics.Sprites.Add(ContentRectangle);

            Padding = 8;
        }
    }
}
