using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using EdGeLib.GUI;

namespace EdGeLib.Scenes
{
    public class SandBox : Scene
    {
        Label labelWithBackground;
        Button button;
        CheckBox checkBox;
        Menu menu;
        TextureDisplay textureDisplay;
        DropDown dropDown;

        public SandBox(EdGeLibGame game)
            :base(game)
        {   
        }

        protected override void LoadContent()
        {
            Label label = new Label("label", false);
            AddEntity(label, SortingLayer.HUD);

            labelWithBackground = new Label("labelWithBackground", true);
            labelWithBackground.Graphics.Position = new Vector2(50, 50);
            labelWithBackground.Input.MouseEnabled = true;
            labelWithBackground.Input.CanMoveOnDrag = true;
            AddEntity(labelWithBackground, SortingLayer.HUD);

            button = new Button("button");
            button.Graphics.Position = new Vector2(100, 100);
            button.Input.Click += Button_Click;
            AddEntity(button, SortingLayer.HUD);

            checkBox = new CheckBox("checkBox", 200);
            checkBox.Graphics.Position = new Vector2(150, 150);
            AddEntity(checkBox, SortingLayer.HUD);

            List<string> menuItems = new List<string>(new string[] { "test","menu","items" } );
            menu = new Menu(menuItems, true);
            menu.Graphics.Position = new Vector2(200, 200);
            menu.Select += Menu_Select;
            AddEntity(menu, SortingLayer.HUD);

            textureDisplay = new TextureDisplay(320, 300, true);
            textureDisplay.SetTexture(TextureManager.Names[3]);
            textureDisplay.Graphics.Position = new Vector2(menu.Graphics.Bounds.Right, menu.Graphics.Y);
            AddEntity(textureDisplay, SortingLayer.HUD);

            List<string> dropDownItems = new List<string>(new string[] { "drop", "down", "test", "long word string" } );
            dropDown = new DropDown(dropDownItems);
            dropDown.Graphics.Position = new Vector2(textureDisplay.Graphics.Bounds.Right + 16, textureDisplay.Graphics.Y);
            AddEntity(dropDown, SortingLayer.HUD);

            TextBox textBox = new TextBox(200);
            textBox.Graphics.Position = new Vector2(400, 50);
            AddEntity(textBox, SortingLayer.HUD);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        private void Menu_Select(object sender, MenuEventArgs e)
        {
            labelWithBackground.TextSprite.Text = e.SelectedItem;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            labelWithBackground.Graphics.Visible = !labelWithBackground.Graphics.Visible;
        }
    }
}
