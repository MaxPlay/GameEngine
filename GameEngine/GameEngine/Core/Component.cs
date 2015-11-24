using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameEngine.Attributes;
using GameEngine.Components;

namespace GameEngine.Core
{
    /// <summary>
    /// Base for every implemented component.
    /// </summary>
    public abstract class Component : EngineObject
    {
        protected GameObject gameObject;
        protected bool initialized;

        public GameObject GameObject { get { return gameObject; } set { if (gameObject == null) gameObject = value; } }
        public Transform Transform { get { return gameObject.Transform; } }

        public Component(GameObject gameObject)
            : base()
        {
            this.gameObject = gameObject;
        }

        public abstract void Reset();

        public override Component AddComponent(Component component)
        {
            this.gameObject.AddComponent(component);
            return component;
        }

        public override T GetComponent<T>()
        {
            return this.gameObject.GetComponent<T>();
        }

        public override Component[] GetComponents()
        {
            return this.gameObject.GetComponents();
        }

        public virtual void Initialize()
        {
            initialized = true;
        }
    }
}