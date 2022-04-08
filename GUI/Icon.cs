using Microsoft.Xna.Framework;
using EdGeLib.Sprites;

namespace EdGeLib.GUI
{
    public class Icon : Control
    {
        public Sprite Sprite { get; private set; }

        public Icon(int width, int height, bool background)
        {
            Graphics.Width = width;
            Graphics.Height = height;
            ContentRectangle.Visible = true;
            ContentRectangle.Width = width;
            ContentRectangle.Height = height;

            Sprite = new Sprite
            {
                RenderOrder = 2,
                Width = width,
                Height = height
            };
            Graphics.Sprites.Add(Sprite);

            if (background)
            {
                Graphics.Width = Padding * 2 + width;
                Graphics.Height = Padding * 2 + height;
                Background.Visible = true;
                Background.Width = Graphics.Width;
                Background.Height = Graphics.Height;
                ContentRectangle.Offset = new Vector2(Padding, Padding);
                Sprite.Offset = ContentRectangle.Offset;
            }

            Input.MouseEnabled = true;
        }
    }
}
