using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple=true)]
    public sealed class RequireComponentAttribute : Attribute
    {
        private Type requiredType;
        public Type RequiredType { get { return requiredType; } }

        public RequireComponentAttribute(Type requireComponent)
        {
            this.requiredType = requireComponent;
        }
    }
}
