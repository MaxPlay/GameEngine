using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Core
{
    public abstract class EngineObject
    {
        private static long instanceDepth;
        protected long instanceID { get; private set; }

        public EngineObject()
        {
            Register();
        }

        public static GameObject Instantiate(GameObject obj, Vector2 position, float rotation)
        {
            GameObject newObj = new GameObject(obj);
            newObj.Transform.LocalPosition = position;
            newObj.Transform.LocalRotation = rotation;
            return newObj;
        }

        public static void Destroy(EngineObject obj)
        {
            if (obj is GameObject)
                ((GameObject)obj).Reset();

            if (obj is Component)
                ((Component)obj).GameObject.RemoveComponent((Component)obj);

            obj = null;
            GC.Collect();
        }

        public abstract Component AddComponent(Component component);
        public Component AddComponent<T>() where T : Component, new()
        {
            return this.AddComponent(new T());
        }
        public abstract T GetComponent<T>() where T : Component;
        public abstract Component[] GetComponents();

        protected virtual void Register()
        {
            this.instanceID = instanceDepth++;
        }
    }
}
