using System;
using Microsoft.Xna.Framework;

namespace EdGeLib.Engine
{
    public class Camera
    {
        //relates to source/world
        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = new Vector2(
                    MathHelper.Clamp(value.X, 0, SourceWidth - AdjustedSourceWidth),
                    MathHelper.Clamp(value.Y, 0, SourceHeight - AdjustedSourceHeight));
                PositionChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler PositionChanged;

        //size of source/world
        public int SourceWidth { get; private set; }
        public int SourceHeight { get; private set; }
        //adjust if source/world is smaller than camera size
        public int AdjustedSourceWidth { get; private set; }
        public int AdjustedSourceHeight { get; private set; }
        public Rectangle AdjustedSourceRectangle
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    AdjustedSourceWidth,
                    AdjustedSourceHeight);
            }
        }
        //relates to screen
        //size of desired camera viewport ouput
        public Vector2 ViewPortPosition { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Rectangle ViewPort
        {
            get
            {
                return new Rectangle(
                    (int)ViewPortPosition.X,
                    (int)ViewPortPosition.Y,
                    Width,
                    Height);
            }
        }
        //adjusted  size of viewport
        public int AdjustedWidth { get; private set; }
        public int AdjustedHeight { get; private set; }
        public Rectangle AdjustedViewPort
        {
            get
            {
                return new Rectangle(
                    (int)ViewPortPosition.X,
                    (int)ViewPortPosition.Y,
                    AdjustedWidth,
                    AdjustedHeight);
            }
        }
        public event EventHandler SizeChanged;

        public float Zoom { get; set; }

        public Camera()
        {
            Width = 0;
            Height = 0;
            position = Vector2.Zero;
            Zoom = 3.0f;
        }

        public void SetSourceSize(int width, int height)
        {
            SourceWidth = width;
            SourceHeight = height;
            AdjustSize();
        }

        public void SetViewSize(int width, int height)
        {
            Width = width;
            Height = height;
            AdjustSize();
        }

        //could be an issue - dividing by zoom could be inaccurate
        private void AdjustSize()
        {
            //check if source is smaller than view
            AdjustedWidth = Width;
            AdjustedSourceWidth = (int)(AdjustedWidth / Zoom);
            if (SourceWidth < AdjustedSourceWidth)
            {
                //center horizontal possibly?
                AdjustedSourceWidth = SourceWidth;
                AdjustedWidth = (int)(AdjustedSourceWidth * Zoom);
            }

            AdjustedHeight = Height;
            AdjustedSourceHeight = (int)(AdjustedHeight / Zoom);
            if (SourceHeight < AdjustedSourceHeight)
            {
                //center vertical possibly?
                AdjustedSourceHeight = SourceHeight;
                AdjustedHeight = (int)(AdjustedSourceHeight * Zoom);
            }
            SizeChanged?.Invoke(this, EventArgs.Empty);
        }
        //potential problem here dividing by zoom. needs to be clamped to source size
        public Vector2 ScreenToSource(Vector2 screenPosition)
        {
            return (screenPosition / Zoom) + Position;
        }

        public Vector2 SourceToScreen(Vector2 sourcePosition)
        {
            return (sourcePosition - Position) * Zoom;
        }

        public bool IsInView(Entity e)
        {
            if (e.Graphics.Bounds.Right < AdjustedViewPort.Left ||
                e.Graphics.Bounds.Bottom < AdjustedViewPort.Top ||
                e.Graphics.Bounds.Top > AdjustedViewPort.Bottom ||
                e.Graphics.Bounds.Left > AdjustedViewPort.Right)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
