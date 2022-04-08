using System;
using Microsoft.Xna.Framework;
using EdGeLib.Engine;
using EdGeLib.Sprites;

namespace EdGeLib.GUI
{
    public class Grid : Entity
    {
        private readonly Camera camera;

        private int cellSize;
        public int CellSize
        {
            get { return cellSize; }
            set
            {
                if (cellSize != value)
                {
                    cellSize = value;
                    UpdateSprites();
                }
            }
        }

        public Grid(Camera camera)
        {
            Graphics.Visible = false;
            this.camera = camera;
            camera.PositionChanged += Camera_PositionChanged;
            camera.SizeChanged += Camera_SizeChanged;
        }

        private void Camera_SizeChanged(object sender, EventArgs e)
        {
            Graphics.Width = camera.AdjustedWidth;
            Graphics.Height = camera.AdjustedHeight;
            UpdateSprites();
        }

        private void Camera_PositionChanged(object sender, EventArgs e)
        {
            UpdateSprites();
        }

        private void AddGridLine(int width, int height, Vector2 offset)
        {
            PixelSprite line = new PixelSprite
            {
                Width = width,
                Height = height,
                Offset = offset,
                Color = Color.White
            };
            Graphics.Sprites.Add(line);
        }

        //how to handle camera zoom?
        public void UpdateSprites()
        {
            Graphics.Sprites.Clear();
            int lineSize = 1;
            Point horizontal = new Point(Graphics.Width, lineSize);
            Point vertical = new Point(lineSize, Graphics.Height);
            //add top and left borders
            AddGridLine(horizontal.X, horizontal.Y, Vector2.Zero);
            AddGridLine(vertical.X, vertical.Y, Vector2.Zero);
            //add bottom and right borders
            AddGridLine(horizontal.X, horizontal.Y, new Vector2(0, Graphics.Height - lineSize));
            AddGridLine(vertical.X, vertical.Y, new Vector2(Graphics.Width - lineSize, 0));
            //add verticals
            int numVerticals = (Graphics.Width / CellSize) + 1;
            int offsetX = -(int)(camera.Position.X * camera.Zoom % CellSize);
            for (int x = 1; x <= numVerticals; x++)
            {
                int gridLineX = offsetX + (x * CellSize);
                if (gridLineX < Graphics.Width - lineSize)
                {
                    AddGridLine(vertical.X, vertical.Y, new Vector2(gridLineX, 0));
                }
            }
            //add horizontals
            int numHorizontals = (Graphics.Height / CellSize) + 1;
            int offsetY = -(int)(camera.Position.Y * camera.Zoom % CellSize);
            for (int y = 1; y <= numHorizontals; y++)
            {
                int gridLineY = offsetY + (y * CellSize);
                if (gridLineY < Graphics.Height - lineSize)
                {
                    AddGridLine(horizontal.X, horizontal.Y, new Vector2(0, offsetY + (y * CellSize)));
                }
            }
        }
    }
}
