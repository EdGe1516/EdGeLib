using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using EdGeLib.Scenes;

namespace EdGeLib.Engine
{
    public class Map : Scene
    {
        public Camera Camera { get; set; }
        public int TilesWide { get; set; }
        public int TilesHigh { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }

        private readonly CartographySystem cartographySystem;

        public Map(EdGeLibGame game, int tilesWide, int tilesHigh, int tileWidth, int tileHeight)
            :base(game)
        {
            TilesWide = tilesWide;
            TilesHigh = tilesHigh;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Color color;
            for (int x = 0; x < tilesWide; x++)
            {
                for (int y = 0; y < tilesHigh; y++)
                {
                    if ((x + y) % 2 == 0)
                    {
                        color = Color.Orchid;
                    }
                    else
                    {
                        color = Color.PaleGreen;
                    }
                    Tile t = new Tile(new Point(x, y));
                    t.Terrain.Color = color;
                    AddEntity(t, SortingLayer.Background);
                }
            }
            Camera = new Camera();
            Camera.SetViewSize(ScreenWidth, ScreenHeight);
            Camera.SetSourceSize(tilesWide * TileWidth, tilesHigh * TileHeight);
            cartographySystem = new CartographySystem(this);
        }

        public override void Update(GameTime gameTime)
        {
            float scrollSpeed = 200f;
            Vector2 scrollCamera = Vector2.Zero;
            if (KeyboardInputSystem.KeyboardState.IsKeyDown(Keys.A))
            {
                scrollCamera.X = -1f;
            }
            else if (KeyboardInputSystem.KeyboardState.IsKeyDown(Keys.D))
            {
                scrollCamera.X = 1f;
            }
            if (KeyboardInputSystem.KeyboardState.IsKeyDown(Keys.W))
            {
                scrollCamera.Y = -1f;
            }
            else if (KeyboardInputSystem.KeyboardState.IsKeyDown(Keys.S))
            {
                scrollCamera.Y = 1f;
            }
            if (scrollCamera != Vector2.Zero)
            {
                scrollCamera.Normalize();
                scrollCamera = scrollCamera * scrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Camera.Position += scrollCamera;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            cartographySystem.Go(gameTime);

            base.Draw(gameTime);
        }

        /*
        public void Update(GameTime gameTime)
        {
            float scrollSpeed = 400f;
            Vector2 scrollCamera = Vector2.Zero;
            if (Scene.KeyboardState.IsKeyDown(Keys.A))
            {
                scrollCamera.X = -1f;
            }
            else if (Scene.KeyboardState.IsKeyDown(Keys.D))
            {
                scrollCamera.X = 1f;
            }
            if (Scene.KeyboardState.IsKeyDown(Keys.W))
            {
                scrollCamera.Y = -1f;
            }
            else if (Scene.KeyboardState.IsKeyDown(Keys.S))
            {
                scrollCamera.Y = 1f;
            }
            if (scrollCamera != Vector2.Zero)
            {
                scrollCamera.Normalize();
                scrollCamera = scrollCamera * scrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Camera.Position += scrollCamera;
            }
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int tileX0, tileX1, tileY0, tileY1;
            if (Tiles.GetLength(0) <= TilesAcrossVisible)
            {
                tileX0 = 0;
                tileX1 = Tiles.GetLength(0) - 1;
            }
            else
            {
                tileX0 = MathHelper.Max((int)(Camera.Position.X / TileDrawWidth), 0);
                tileX1 = MathHelper.Min(tileX0 + TilesAcrossVisible + 1, Tiles.GetLength(0) - 1);
            }
            if (Tiles.GetLength(1) <= TilesDownVisible)
            {
                tileY0 = 0;
                tileY1 = Tiles.GetLength(1) - 1;
            }
            else
            {
                tileY0 = MathHelper.Max((int)(Camera.Position.Y / TileDrawHeight), 0);
                tileY1 = MathHelper.Min(tileY0 + TilesDownVisible + 1, Tiles.GetLength(1) - 1);
            }
            for (int tileX = tileX0; tileX <= tileX1; tileX++)
            {
                for (int tileY = tileY0; tileY <= tileY1; tileY++)
                {
                    Tiles[tileX, tileY].Draw(spriteBatch);
                }
            }
        }
        */
    }
}
