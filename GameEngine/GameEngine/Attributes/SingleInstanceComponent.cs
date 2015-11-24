using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    class SingleInstanceComponentAttribute : Attribute
    {
        public SingleInstanceComponentAttribute() { }
    }
}
