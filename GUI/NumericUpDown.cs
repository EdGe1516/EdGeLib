using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using EdGeLib.Scenes;

namespace EdGeLib.GUI
{
    public class NumericUpDown : Control
    {
        public int Min { get; set; }
        public int Max { get; set; }
        public int Increment { get; set; }

        private int number;
        public int Number
        {
            get { return number; }
            set
            {
                number = MathHelper.Clamp(value, Min, Max);
                display.TextSprite.Text = number.ToString();
                NumberChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler NumberChanged;

        readonly Button up;
        readonly Button down;
        readonly Label display;

        public NumericUpDown(int initialNumber)
        {
            display = new Label("", 64, true);
            up = new Button("+", 32, true);
            down = new Button("-", 32, true);

            Family.AddChild(down, Vector2.Zero);
            Family.AddChild(display, new Vector2(down.Graphics.Width, 0));
            Family.AddChild(up, new Vector2(down.Graphics.Width + display.Graphics.Width, 0));

            Graphics.Width = display.Graphics.Width + up.Graphics.Width + down.Graphics.Width;
            Graphics.Height = display.Graphics.Height;

            up.Input.Click += Up_Click;
            down.Input.Click += Down_Click;

            Min = 8;
            Max = 128;
            Increment = 2;
            number = initialNumber;
            display.TextSprite.Text = number.ToString();
        }

        private void Up_Click(object sender, Systems.MouseInputEventArgs e)
        {
            Number += Increment;
        }

        private void Down_Click(object sender, Systems.MouseInputEventArgs e)
        {
            Number -= Increment;
        }
    }
}
