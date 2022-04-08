using System.Collections.Generic;
using Microsoft.Xna.Framework;
using EdGeLib.GUI;
using EdGeLib.Systems;

namespace EdGeLib.Scenes
{
    public enum SortingLayer
    {
        HUD,
        Foreground,
        Background,
    }
    public abstract class Scene : DrawableGameComponent
    {
        public List<Entity>[] Entities { get; set; }
        public Entity FocusedEntity { get; set; }
        public List<KeyCommand> KeyCommands { get; set; }

        public readonly MouseInputSystem MouseInputSystem;
        public readonly KeyboardInputSystem KeyboardInputSystem;
        public readonly SkinSystem SkinSystem;
        public readonly RenderSystem RenderSystem;

        public int ScreenWidth { get { return Game.Window.ClientBounds.Width; } }
        public int ScreenHeight { get { return Game.Window.ClientBounds.Height; } }

        public readonly int GUIPadding = 8;

        public Scene(EdGeLibGame game)
            :base(game)
        {
            Entities = new List<Entity>[3];
            Entities[(int)SortingLayer.HUD] = new List<Entity>();
            Entities[(int)SortingLayer.Foreground] = new List<Entity>();
            Entities[(int)SortingLayer.Background] = new List<Entity>();
            FocusedEntity = null;
            KeyCommands = new List<KeyCommand>();
            KeyboardInputSystem = new KeyboardInputSystem(this);
            MouseInputSystem = new MouseInputSystem(this);
            SkinSystem = new SkinSystem(this);
            RenderSystem = new RenderSystem(this);
        }

        public override void Update(GameTime gameTime)
        {
            MouseInputSystem.Go(gameTime);
            KeyboardInputSystem.Go(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SkinSystem.Go(gameTime);
            RenderSystem.Go(gameTime);

            base.Draw(gameTime);
        }

        public void AddEntity(Entity e, SortingLayer sortingLayer)
        {
            Entities[(int)sortingLayer].Add(e);
            if (e.Family.Children.Count > 0)
            {
                foreach (Entity descendant in e.Family.Dependants())
                {
                    Entities[(int)sortingLayer].Add(descendant);
                }
            }
        }
    }
}
