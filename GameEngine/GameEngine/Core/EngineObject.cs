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

        protected bool active;
        public event EventHandler Activated;
        public event EventHandler Deactivated;

        /// <summary>
        /// EngineObjects that are not active will not be updated. They can be accessed, but the won't be recogniced. If components are not active, they will not work.
        /// </summary>
        public bool Active { get { return this.active; } set { if (this.active == value) return; this.active = value; OnActiveChanged(); } }

        protected void OnActiveChanged()
        {
            if (this.active)
            {
                if (Activated != null)
                    Activated(this, new EventArgs());
            }
            else
            {
                if (Deactivated != null)
                    Deactivated(this, new EventArgs());
            }
        }

        public EngineObject()
        {
            Register();
            this.active = true;
        }

        /// <summary>
        /// This method can be used to instantiate GameObjects at a given position and with a given rotation.
        /// The GameObject can have components, these will be copied.
        /// </summary>
        /// <param name="obj">The object to instantiate.</param>
        /// <param name="position">The position of the object.</param>
        /// <param name="rotation">The rotation of the object.</param>
        /// <returns>Returns the new GameObject</returns>
        public static GameObject Instantiate(GameObject obj, Vector2 position, float rotation)
        {
            GameObject newObj = new GameObject(obj);
            newObj.Transform.LocalPosition = position;
            newObj.Transform.LocalRotation = rotation;
            return newObj;
        }

        /// <summary>
        /// Removes the given object from the memory.
        /// Also removes the object from the gameobjects-list in Settings.
        /// </summary>
        /// <param name="obj">The object to remove.</param>
        public static void Destroy(EngineObject obj)
        {
            if (obj is GameObject)
            {
                ((GameObject)obj).Reset();
                Settings.RemoveGameObject((GameObject)obj);
            }

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
        
        /// <summary>
        /// Used for registering the EngineObject in the engine. This is used to make the objects differ, even if all other fields are equal.
        /// </summary>
        protected virtual void Register()
        {
            this.instanceID = instanceDepth++;
        }
    }
}
