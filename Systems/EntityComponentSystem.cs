using Microsoft.Xna.Framework;
using EdGeLib.Scenes;

namespace EdGeLib.Systems
{
    public abstract class EntityComponentSystem
    {
        protected Scene Scene { get; private set; }

        public EntityComponentSystem(Scene scene)
        {
            Scene = scene;
        }

        public virtual void Go(GameTime gameTime)
        {
        }
    }
}
