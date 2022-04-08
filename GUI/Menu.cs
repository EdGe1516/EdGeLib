using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using EdGeLib.Components;
using EdGeLib.Systems;

namespace EdGeLib.GUI
{
    public class MenuEventArgs : EventArgs
    {
        public string SelectedItem { get; set; }

        public MenuEventArgs(string selectedItem)
        {
            SelectedItem = selectedItem;
        }
    }
    public class Menu : Control
    {
        public event EventHandler<MenuEventArgs> Select;

        public Menu(List<string> items, bool background)
        {
            int width = 0;
            int height = 0;
            for (int i = 0; i < items.Count; i++)
            { 
                Label l = new Label(items[i], false);
                if (l.Graphics.Width > width)
                {
                    width = l.Graphics.Width;
                }
                l.Input.MouseEnabled = true;
                Family.AddChild(l, new Vector2(0, i * l.Graphics.Height));
                height += l.Graphics.Height;
            }
            Graphics.Width = width;
            Graphics.Height = height;
            if (background)
            {
                Graphics.Width = Graphics.Width + Padding * 2;
                Graphics.Height = Graphics.Height + Padding * 2;
                Background.Visible = true;
                Background.Width = Graphics.Width;
                Background.Height = Graphics.Height;
                for (int i = 0; i < Family.Children.Count; i++)
                {
                    Family.ChangeDock(Family.Children[i], new Vector2(
                        Padding,
                        Padding + i * Family.Children[i].Graphics.Height));
                }
            }
            Input.MouseEnabled = true;
            Input.Click += Input_Click;
        }

        private void Input_Click(object sender, MouseInputEventArgs e)
        {
            foreach (Entity child in Family.Children)
            {
                Label l = (Label)child;
                if (l.Graphics.Bounds.Contains(e.MousePosition))
                {
                    MenuEventArgs m = new MenuEventArgs(l.TextSprite.Text);
                    OnSelect(m);
                }
            }
        }

        private void OnSelect(MenuEventArgs m)
        {
            Select?.Invoke(this, m);
        }
    }
}
