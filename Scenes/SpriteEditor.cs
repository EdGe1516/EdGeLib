using System;
using System.Linq;
using Microsoft.Xna.Framework;
using EdGeLib.Components;
using EdGeLib.GUI;
using EdGeLib.Sprites;

namespace EdGeLib.Scenes
{
    public class SpriteEditor : Scene
    {
        int spriteSize;
        int adjustedSpriteSize;
        bool spriteSelectMode;

        Label labelTitle;
        TextureDisplay textureDisplay;
        Grid grid;
        PixelSprite spriteSelectionBox;
        Label labelTexture;
        DropDown dropDownTextureSelect;
        Label labelSpriteKey;
        DropDown dropDownSpriteKeySelect;
        Label labelSpriteSize;
        NumericUpDown numericUpDownTileSize;
        CheckBox checkBoxShowGrid;
        Icon iconSpritePreview;
        Button buttonSave;

        public SpriteEditor(EdGeLibGame game)
            :base(game)
        {
        }

        protected override void LoadContent()
        {
            spriteSize = 16;
            spriteSelectMode = false;

            //initializing controls
            labelTitle = new Label("SPRITE EDITOR", true);
            AddEntity(labelTitle, SortingLayer.HUD);

            labelSpriteKey = new Label("SPRITE KEY:", true);
            AddEntity(labelSpriteKey, SortingLayer.HUD);

            dropDownSpriteKeySelect = new DropDown(SpriteManager.Names);
            dropDownSpriteKeySelect.SelectedItemChanged += DropDownSpriteKeySelect_SelectedItemChanged;
            AddEntity(dropDownSpriteKeySelect, SortingLayer.HUD);

            labelTexture = new Label("TEXTURE:", true);
            AddEntity(labelTexture, SortingLayer.HUD);

            dropDownTextureSelect = new DropDown(TextureManager.Names);
            dropDownTextureSelect.SelectedItemChanged += DropDownTextureSelect_SelectedItemChanged;
            AddEntity(dropDownTextureSelect, SortingLayer.HUD);

            textureDisplay = new TextureDisplay(
                (ScreenWidth - (GUIPadding * 2)) / 2,
                ScreenHeight / 2,
                true);
            adjustedSpriteSize = (int)(spriteSize * textureDisplay.Camera.Zoom);
            spriteSelectionBox = new PixelSprite(adjustedSpriteSize, adjustedSpriteSize)
            {
                Color = Color.LightGreen * .5f
            };
            textureDisplay.Graphics.Sprites.Add(spriteSelectionBox);
            grid = new Grid(textureDisplay.Camera)
            {
                CellSize = adjustedSpriteSize
            };
            textureDisplay.Family.AddChild(grid, textureDisplay.ContentRectangle.Offset);
            textureDisplay.Input.Click += TextureDisplay_Click;
            textureDisplay.Camera.PositionChanged += TextureDisplay_Camera_PositionChanged;
            AddEntity(textureDisplay, SortingLayer.HUD);
            AddEntity(grid, SortingLayer.HUD);

            iconSpritePreview = new Icon(64, 64, true);
            iconSpritePreview.Input.Click += IconSpritePreview_Click;
            AddEntity(iconSpritePreview, SortingLayer.HUD);

            labelSpriteSize = new Label("SPRITE SIZE:", true);
            AddEntity(labelSpriteSize, SortingLayer.HUD);

            numericUpDownTileSize = new NumericUpDown(spriteSize);
            numericUpDownTileSize.NumberChanged += NumericUpDownTileSize_NumberChanged;
            AddEntity(numericUpDownTileSize, SortingLayer.HUD);

            checkBoxShowGrid = new CheckBox("SHOW GRID", 180);
            checkBoxShowGrid.CheckedChanged += CheckBoxShowGrid_CheckedChanged;
            AddEntity(checkBoxShowGrid, SortingLayer.HUD);

            buttonSave = new Button("SAVE");
            buttonSave.Input.Click += ButtonSave_Click;
            AddEntity(buttonSave, SortingLayer.HUD);

            dropDownSpriteKeySelect.SelectedItem = SpriteManager.Names[0];

            //dock controls
            labelTitle.Family.AddDock(textureDisplay, DockDirection.Down, GUIPadding);
            textureDisplay.Family.AddDock(labelSpriteKey, DockDirection.Right, GUIPadding);
            labelSpriteKey.Family.AddDock(dropDownSpriteKeySelect, DockDirection.Right, GUIPadding);
            labelSpriteKey.Family.AddDock(labelTexture, DockDirection.Down, GUIPadding);
            labelTexture.Family.AddDock(dropDownTextureSelect, DockDirection.Right, GUIPadding);
            labelTexture.Family.AddDock(labelSpriteSize, DockDirection.Down, GUIPadding);
            labelSpriteSize.Family.AddDock(numericUpDownTileSize, DockDirection.Right, GUIPadding);
            numericUpDownTileSize.Family.AddDock(checkBoxShowGrid, DockDirection.Right, GUIPadding);
            labelSpriteSize.Family.AddDock(iconSpritePreview, DockDirection.Down, GUIPadding);
            iconSpritePreview.Family.AddDock(buttonSave, DockDirection.Down, GUIPadding);
            labelTitle.Graphics.Position = new Vector2(GUIPadding, GUIPadding);

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

        private void ButtonSave_Click(object sender, Systems.MouseInputEventArgs e)
        {
            SpriteManager.Write();
        }

        private void TextureDisplay_Camera_PositionChanged(object sender, EventArgs e)
        {
            UpdateSpriteSelectionBox();
        }

        private void IconSpritePreview_Click(object sender, Systems.MouseInputEventArgs e)
        {
            spriteSelectMode = !spriteSelectMode;
        }

        private void TextureDisplay_Click(object sender, Systems.MouseInputEventArgs e)
        {
            if (spriteSelectMode)
            {
                Vector2 position = e.MousePosition - (textureDisplay.Graphics.Position + textureDisplay.ContentRectangle.Offset);
                Vector2 location = textureDisplay.Camera.ScreenToSource(position);
                int x = (int)(location.X / spriteSize);
                int y = (int)(location.Y / spriteSize);
                Sprite sprite = SpriteManager.Templates[dropDownSpriteKeySelect.SelectedItem];
                sprite.TextureName = dropDownTextureSelect.SelectedItem;
                sprite.SourceRectangle = new Rectangle(
                    x * spriteSize,
                    y * spriteSize,
                    spriteSize,
                    spriteSize);
                iconSpritePreview.Sprite.TextureName = sprite.TextureName;
                iconSpritePreview.Sprite.SourceRectangle = sprite.SourceRectangle;

                UpdateSpriteSelectionBox();

                spriteSelectMode = false;
            }
        }

        private void DropDownSpriteKeySelect_SelectedItemChanged(object sender, EventArgs e)
        {
            Sprite sprite = SpriteManager.Templates[dropDownSpriteKeySelect.SelectedItem];
            iconSpritePreview.Sprite.TextureName = sprite.TextureName;
            iconSpritePreview.Sprite.SourceRectangle = sprite.SourceRectangle;
            if (sprite.TextureName != "Pixel")
            {
                dropDownTextureSelect.SelectedItem = sprite.TextureName;
            }
            else
            {
                textureDisplay.SetTexture(dropDownTextureSelect.SelectedItem);
                spriteSelectionBox.Visible = false;
            }
        }

        private void NumericUpDownTileSize_NumberChanged(object sender, EventArgs e)
        {
            //textureDisplay.SpriteSize = numericUpDownTileSize.Number;
        }

        private void CheckBoxShowGrid_CheckedChanged(object sender, EventArgs e)
        {
            grid.Graphics.Visible = !grid.Graphics.Visible;
        }

        private void DropDownTextureSelect_SelectedItemChanged(object sender, EventArgs e)
        {
            textureDisplay.SetTexture(dropDownTextureSelect.SelectedItem);
            UpdateSpriteSelectionBox();
        }
        //checks to see if spriteselectionbox should be shown at all, where and crops
        private void UpdateSpriteSelectionBox()
        {
            if (iconSpritePreview.Sprite.TextureName != dropDownTextureSelect.SelectedItem)
            {
                spriteSelectionBox.Visible = false;
            }
            else
            {
                Vector2 offset = textureDisplay.Camera.SourceToScreen(
                    new Vector2(
                        iconSpritePreview.Sprite.SourceRectangle.X,
                        iconSpritePreview.Sprite.SourceRectangle.Y)) +
                    textureDisplay.ContentRectangle.Offset;
                Rectangle destinationRectangle = new Rectangle(
                    (int)(textureDisplay.Graphics.Position.X + offset.X),
                    (int)(textureDisplay.Graphics.Position.Y + offset.Y),
                    adjustedSpriteSize,
                    adjustedSpriteSize);
                if (textureDisplay.Camera.AdjustedViewPort.Contains(destinationRectangle))
                {
                    spriteSelectionBox.Offset = offset;
                    spriteSelectionBox.Width = adjustedSpriteSize;
                    spriteSelectionBox.Height = adjustedSpriteSize;
                    spriteSelectionBox.Visible = true;
                }
                else
                {
                    if (textureDisplay.Camera.AdjustedViewPort.Intersects(destinationRectangle))
                    {
                        Rectangle cropped = Rectangle.Intersect(textureDisplay.Camera.AdjustedViewPort, destinationRectangle);
                        offset += new Vector2(cropped.X - destinationRectangle.X, cropped.Y - destinationRectangle.Y);
                        spriteSelectionBox.Offset = offset;
                        spriteSelectionBox.Width = cropped.Width;
                        spriteSelectionBox.Height = cropped.Height;
                        spriteSelectionBox.Visible = true;
                    }
                    else
                    {
                        spriteSelectionBox.Visible = false;
                    }
                }
            }
        }
    }
}
