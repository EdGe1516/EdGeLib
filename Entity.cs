using EdGeLib.Components;
using EdGeLib.Scenes;

namespace EdGeLib
{
    public abstract class Entity
    {
        public int ID { get; private set; }
        public Graphics Graphics { get; set; }
        public Input Input { get; set; }
        public Family Family { get; set; }

        public Entity()
        {
            ID = EntityManager.GetID();
            Graphics = new Graphics(this);
            Input = new Input(this);
            Family = new Family(this);
        }
    }
}
