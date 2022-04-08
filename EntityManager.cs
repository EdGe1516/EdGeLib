using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdGeLib
{
    public static class EntityManager
    {
        public static int Count { get; private set; }

        public static void Initialize()
        {
            Count = 0;
        }

        public static int GetID()
        {
            int id = Count;
            Count++;
            return id;
        }
    }
}
