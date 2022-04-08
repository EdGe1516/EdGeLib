using Microsoft.Xna.Framework;
using EdGeLib.Components;
using EdGeLib.Engine;
using EdGeLib.Sprites;
using EdGeLib.Systems;

namespace EdGeLib.GUI
{
    public class TextureDisplay : Control
    {
        public Camera Camera { get; private set; }
        public Sprite Texture { get; private set; }

        public TextureDisplay(int width, int height, bool background)
        {
            Camera = new Camera();

            Texture = new Sprite()
            {
                RenderOrder = 2,
            };
            Graphics.Sprites.Add(Texture);

            Graphics.Width = width;
            Graphics.Height = height;
            ContentRectangle.Visible = true;
            ContentRectangle.Width = width;
            ContentRectangle.Height = height;
            if (background)
            {
                Background.Visible = true;
                Background.Width = width;
                Background.Height = height;
                ContentRectangle.Offset = new Vector2(Padding, Padding);
                ContentRectangle.Width = width - (Padding * 2);
                ContentRectangle.Height = height - (Padding * 2);
                Texture.Offset = ContentRectangle.Offset;
            }

            Camera.SetViewSize(ContentRectangle.Width, ContentRectangle.Height);

            Input.MouseEnabled = true;
            Input.MouseDownBegin += Input_MouseDownBegin;
            Input.Drag += Input_Drag;
            Graphics.PositionChanged += Graphics_PositionChanged;
        }

        private void Graphics_PositionChanged(object sender, System.EventArgs e)
        {
            Camera.ViewPortPosition = Graphics.Position + ContentRectangle.Offset;
        }

        private Vector2 mouseDownPosition;
        private void Input_MouseDownBegin(object sender, MouseInputEventArgs m)
        {
            mouseDownPosition = Camera.Position;
        }

        private void Input_Drag(object sender, MouseInputEventArgs m)
        {
            Vector2 scroll = (m.MouseDownMousePosition - m.MousePosition) / Camera.Zoom;
            Camera.Position = mouseDownPosition + scroll;
            Texture.SourceRectangle = Camera.AdjustedSourceRectangle;
        }

        public void SetTexture(string textureName)
        {
            Texture.TextureName = textureName;
            Camera.Position = Vector2.Zero;
            Camera.SetSourceSize(
                TextureManager.Textures[textureName].Width,
                TextureManager.Textures[textureName].Height);
            Texture.SourceRectangle = Camera.AdjustedSourceRectangle;
            Texture.Width = Camera.AdjustedWidth;
            Texture.Height = Camera.AdjustedHeight;
        }
    }
}
