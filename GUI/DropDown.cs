using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using EdGeLib.Systems;

namespace EdGeLib.GUI
{
    class DropDown : Control
    {
        public Button Button { get; set; }
        public Menu Menu { get; set; }

        private string selectedItem;
        public string SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                Button.TextSprite.Text = selectedItem;
                SelectedItemChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler SelectedItemChanged;     

        public DropDown(List<string> items)
        {
            Menu = new Menu(items, true);
            Menu.Graphics.Visible = false;
            Menu.Graphics.VisibleChanged += Menu_VisibleChanged;
            Menu.Select += Menu_Select;

            Button = new Button(items[0], Menu.Graphics.Width, false);
            Button.Input.Click += Button_Click;

            Family.AddChild(Menu, new Vector2(0, Button.Graphics.Height));
            Family.AddChild(Button, Vector2.Zero);

            SelectedItem = Button.TextSprite.Text;
            Graphics.Width = Button.Graphics.Width;
            Graphics.Height = Button.Graphics.Height;

            Input.MouseEnabled = true;
            Input.MouseExit += Input_MouseExit;
        }

        private void Input_MouseExit(object sender, MouseInputEventArgs e)
        {
            Menu.Graphics.Visible = false;
        }

        private void Button_Click(object sender, MouseInputEventArgs e)
        {
            Menu.Graphics.Visible = !Menu.Graphics.Visible;
        }

        private void Menu_Select(object sender, MenuEventArgs e)
        {
            SelectedItem = e.SelectedItem;
            Menu.Graphics.Visible = false;
        }

        private void Menu_VisibleChanged(object sender, EventArgs e)
        {
            if (Menu.Graphics.Visible)
            {
                Graphics.Width = Menu.Graphics.Width;
                Graphics.Height = Menu.Graphics.Height + Button.Graphics.Height;
            }
            else
            {
                Graphics.Width = Button.Graphics.Width;
                Graphics.Height = Button.Graphics.Height;
            }
        }
    }
}
