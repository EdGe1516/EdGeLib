using System;
using Microsoft.Xna.Framework;
using EdGeLib.Systems;
using EdGeLib.Sprites;

namespace EdGeLib.GUI
{
    public class CheckBox : Label
    {
        protected int CheckBoxSize { get; set; }
        public PixelSprite CheckMark { get; set; }

        private bool check;
        public bool Checked
        {
            get { return check; }
            set
            {
                if (value != check)
                {
                    check = value;
                    OnCheckedChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler CheckedChanged;

        public CheckBox(string text, int width)
            :base(text, width, false)
        {
            CheckBoxSize = 20;
            CheckMark = new PixelSprite()
            {
                Visible = false,
                RenderOrder = 3,
                Offset = new Vector2(10, 10),
                Width = CheckBoxSize - 8,
                Height = CheckBoxSize - 8,
                Color = Color.White,
            };
            Graphics.Sprites.Add(CheckMark);

            ContentRectangle.Visible = true;
            ContentRectangle.Offset = new Vector2(Padding, Padding);
            ContentRectangle.Width = CheckBoxSize;
            ContentRectangle.Height = CheckBoxSize;
            TextSprite.Offset = new Vector2(Graphics.Width - (TextSprite.Width + (Padding * 2)), TextOffset.Y);
            check = false;
            Input.MouseEnabled = true;
            Input.Click += Input_Click;
        }

        private void Input_Click(object sender, MouseInputEventArgs e)
        {
            Checked = !Checked;
        }

        public void OnCheckedChanged(EventArgs e)
        {
            if (Checked)
            {
                CheckMark.Visible = true;
            }
            else
            {
                CheckMark.Visible = false;
            }
            CheckedChanged?.Invoke(this, e);
        }
    }
}
