namespace EdGeLib.Components
{
    public abstract class Component
    {
        protected readonly Entity Entity;

        public Component() { }

        public Component(Entity entity)
        {
            Entity = entity;
        }
    }
}
