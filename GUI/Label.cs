using System;
using Microsoft.Xna.Framework;
using EdGeLib.Components;
using EdGeLib.Sprites;

namespace EdGeLib.GUI
{
    public class Label : Control
    {
        public TextSprite TextSprite { get; set; }

        public bool CenterText { get; set; }
        public bool AutoSize { get; set; }
        protected Vector2 TextOffset { get; set; }
        protected int BackgroundHeight { get; set; }

        public Label(string text, bool background)
        {
            TextSprite = new TextSprite
            {
                RenderOrder = 2
            };
            TextSprite.TextChanged += TextSprite_TextChanged;
            Graphics.Sprites.Add(TextSprite);

            CenterText = false;
            AutoSize = false;
            if (background)
            {
                AutoSize = true;
                Background.Visible = true;
            }
            TextOffset = new Vector2(8, 4);
            BackgroundHeight = 32;
            Padding = 6;
          
            TextSprite.Text = text;
        }

        public Label(string text, int width, bool centerText)
        {
            TextSprite = new TextSprite
            {
                RenderOrder = 2
            };
            TextSprite.TextChanged += TextSprite_TextChanged;
            Graphics.Sprites.Add(TextSprite);

            Graphics.Width = width;
            Background.Visible = true;
            CenterText = centerText;
            AutoSize = false;
            TextOffset = new Vector2(8, 4);
            BackgroundHeight = 32;
            Padding = 6;

            TextSprite.Text = text;
        }

        private void TextSprite_TextChanged(object sender, EventArgs e)
        {
            if (Background.Visible)
            {
                TextSprite.Offset = TextOffset;
                Graphics.Height = BackgroundHeight;
                Background.Width = Graphics.Width;
                Background.Height = BackgroundHeight;
                ContentRectangle.Offset = new Vector2(Padding, Padding);
                ContentRectangle.Width = Graphics.Width - Padding * 2;
                ContentRectangle.Height = Background.Height - Padding * 2;
                if (AutoSize)
                {
                    Graphics.Width = TextSprite.Width + (int)(TextSprite.Offset.X * 2);
                    Background.Width = Graphics.Width;
                    ContentRectangle.Width = Background.Width - Padding * 2;
                }
                if (CenterText)
                {
                    TextSprite.Offset = new Vector2(
                        (Graphics.Width - TextSprite.Width) / 2,
                        TextSprite.Offset.Y);
                }
            }
            else
            {
                Graphics.Width = TextSprite.Width;
                Graphics.Height = TextSprite.Height;
            }
        }
    }
}
