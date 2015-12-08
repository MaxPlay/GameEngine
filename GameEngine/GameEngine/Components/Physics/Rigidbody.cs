using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Core;
using Box2D.XNA;

namespace GameEngine.Components.Physics
{
    class Rigidbody : Component
    {
        

        public Rigidbody()
            : base(null)
        {

        }

        public Rigidbody(GameObject gameObject)
            : base(gameObject)
        {

        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
