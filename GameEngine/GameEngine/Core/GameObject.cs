using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using GameEngine.Attributes;
using GameEngine.Components;

namespace GameEngine.Core
{
    [RequireComponent(typeof(Transform))]
    public sealed class GameObject : EngineObject
    {
        private List<Component> components;
        /// <summary>
        /// The transform of the object.
        /// </summary>
        public Transform Transform { get { return GetComponent<Transform>(); } }

        private bool isStatic;
        public bool IsStatic { get { return this.isStatic; } }

        public GameObject()
        {
            components = new List<Component>();
            aquireComponents(typeof(GameObject));

        }

        public GameObject(GameObject obj)
        {
            components = new List<Component>();
            foreach (Component c in obj.components)
                aquireComponents(c.GetType());
        }
        /// <summary>
        /// Adds Component by given Type.
        /// </summary>
        /// <param name="aquiredType">The Type that needs to be aquired.</param>
        private void aquireComponents(Type aquiredType)
        {
            foreach (Attribute a in aquiredType.GetCustomAttributes(true))
            {
                if (a is RequireComponentAttribute)
                    this.AddComponent((Component)Activator.CreateInstance(((RequireComponentAttribute)a).RequiredType, this));
            }
        }

        public void RemoveComponent(Component comp)
        {
            components.Remove(comp);

            foreach (Component c in components)
            {
                if (c is Transform)
                    return;
            }

            components.Add(new Transform(this));
        }

        public void Reset()
        {
            foreach (Component c in components)
                c.Reset();
        }

        public override Component AddComponent(Component component)
        {
            foreach (Attribute a in component.GetType().GetCustomAttributes(true))
            {
                if (a is SingleInstanceComponentAttribute)
                {
                    foreach (Component c in components)
                    {
                        if (c.GetType() == component.GetType())
                            return c;
                    }
                }
            }

            components.Add(component);
            return component;
        }

        public override T GetComponent<T>()
        {
            for (int i = 0; i < this.components.Count; i++)
                if (this.components[i] is T)
                    return (T)this.components[i];

            return null;
        }

        public override Component[] GetComponents()
        {
            return this.components.ToArray();
        }
    }
}
