using GameEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Components.Audio
{
    class AudioListener : Component
    {


        public AudioListener(GameObject gameObject)
            : base(gameObject)
        {

        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
