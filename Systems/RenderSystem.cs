using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EdGeLib.Scenes;
using EdGeLib.Sprites;

namespace EdGeLib.Systems
{
    public class RenderSystem : EntityComponentSystem
    {
        private readonly SpriteBatch spriteBatch;

        public RenderSystem(Scene scene)
            :base(scene)
        {
            spriteBatch = new SpriteBatch(Scene.Game.GraphicsDevice);
        }

        public override void Go(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
            for (int i = Scene.Entities.Length; i-- > 0;)
            {
                foreach (Entity e in Scene.Entities[i])
                {
                    if ((e.Graphics.Sprites.Count > 0 || e.Family.Children.Count > 0) &&
                        e.Graphics.Visible)
                    {
                        DrawSprites(e, gameTime);
                    }
                }
            }
            spriteBatch.End();
        }

        public void DrawSprites(Entity entity, GameTime gameTime)
        {
            //change to foreach loop. remove renderorder property. draws in order sprites are added
            for (int i = 0; i < entity.Graphics.Sprites.Count; i++)
            {
                ISprite sprite = entity.Graphics.Sprites[i];
                if (sprite is IAnimatedSprite animatedSprite && animatedSprite.IsAnimating)
                {
                    animatedSprite.UpdateAnimation(gameTime);
                }
                if (sprite.Visible)
                {
                    if (sprite is Sprite)
                    {
                        DrawSprite(entity, sprite as Sprite);
                    }
                    else if (sprite is TextSprite)
                    {
                        DrawTextSprite(entity, sprite as TextSprite);
                    }
                    else if (sprite is PixelSprite)
                    {
                        DrawPixelSprite(entity, sprite as PixelSprite);
                    }
                }
            }
        }

        public void DrawSprite(Entity entity, Sprite sprite)
        {
            spriteBatch.Draw(
                    TextureManager.Textures[sprite.TextureName],
                    new Rectangle(
                        (int)(entity.Graphics.Position.X + sprite.Offset.X),
                        (int)(entity.Graphics.Position.Y + sprite.Offset.Y),
                        (int)(sprite.Width * sprite.Scale),
                        (int)(sprite.Height * sprite.Scale)),
                    sprite.SourceRectangle,
                    sprite.Color);
        }

        public void DrawTextSprite(Entity entity, TextSprite textSprite)
        {
            spriteBatch.DrawString(
                textSprite.Font,
                textSprite.Text,
                new Vector2(
                    entity.Graphics.Position.X + textSprite.Offset.X,
                    entity.Graphics.Position.Y + textSprite.Offset.Y),
                textSprite.Color);
        }

        public void DrawPixelSprite(Entity entity, PixelSprite pixelSprite)
        {
            Rectangle destinationRectangle = new Rectangle(
                (int)(entity.Graphics.Position.X + pixelSprite.Offset.X),
                (int)(entity.Graphics.Position.Y + pixelSprite.Offset.Y),
                (int)(pixelSprite.Width),
                (int)(pixelSprite.Height));
            spriteBatch.Draw(
                TextureManager.Textures["Pixel"],
                destinationRectangle,
                pixelSprite.Color);
            if (pixelSprite.Has3DEffect)
            {
                int bezel = 4;
                Color highlight = Color.WhiteSmoke;
                Color shadow = Color.DarkSlateGray;
                Rectangle topLeft = new Rectangle(
                    destinationRectangle.X, destinationRectangle.Y,
                    bezel, bezel);
                Rectangle top = new Rectangle(
                    destinationRectangle.X + bezel, destinationRectangle.Y,
                    destinationRectangle.Width - bezel, bezel);
                Rectangle left = new Rectangle(
                    destinationRectangle.X, destinationRectangle.Y + bezel,
                    bezel, destinationRectangle.Height - (bezel * 2));
                Rectangle right = new Rectangle(
                    destinationRectangle.Right - bezel, destinationRectangle.Y + bezel,
                    bezel, left.Height);
                Rectangle bottom = new Rectangle(
                    destinationRectangle.X, left.Bottom,
                    top.Width, bezel);
                Rectangle bottomRight = new Rectangle(
                    bottom.Right, left.Bottom,
                    bezel, bezel);
                spriteBatch.Draw(
                    TextureManager.Textures["Pixel"],
                    topLeft,
                    highlight * .75f);
                spriteBatch.Draw(
                    TextureManager.Textures["Pixel"],
                    top,
                    highlight * .50f);
                spriteBatch.Draw(
                    TextureManager.Textures["Pixel"],
                    left,
                    highlight * .50f);
                spriteBatch.Draw(
                    TextureManager.Textures["Pixel"],
                    right,
                    shadow * .50f);
                spriteBatch.Draw(
                    TextureManager.Textures["Pixel"],
                    bottom,
                    shadow * .50f);
                spriteBatch.Draw(
                    TextureManager.Textures["Pixel"],
                    bottomRight,
                    shadow * .90f);
            }
        }
    }
}
