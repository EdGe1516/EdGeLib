using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using EdGeLib.Scenes;
using EdGeLib.Sprites;
using EdGeLib.Systems;

namespace EdGeLib.Engine
{
    public class CartographySystem : EntityComponentSystem
    {
        Map map;

        public CartographySystem(Map map)
            : base(map)
        {
            this.map = map;
        }

        public override void Go(GameTime gameTime)
        {
            int tileDrawWidth = (int)(map.TileWidth * map.Camera.Zoom);
            int tileDrawHeight = (int)(map.TileHeight * map.Camera.Zoom);
            foreach (Entity e in map.Entities[(int)SortingLayer.Background])
            {
                if (e is Tile t)
                {
                    //add sizechange event to tile class to update all sprites to the same size
                    t.Graphics.Width = tileDrawWidth;
                    t.Graphics.Height = tileDrawHeight;
                    foreach (ISprite sprite in t.Graphics.Sprites)
                    {
                        sprite.Width = tileDrawWidth;
                        sprite.Height = tileDrawHeight;
                    }
                    Vector2 sourcePosition = new Vector2(
                        t.Location.X * map.TileWidth,
                        t.Location.Y * map.TileHeight);
                    t.Graphics.Position = map.Camera.SourceToScreen(sourcePosition);
                    if (map.Camera.IsInView(t))
                    {
                        t.Graphics.Visible = true;
                    }
                    else
                    {
                        t.Graphics.Visible = false;
                    }
                }
            }
        }
    }
}
