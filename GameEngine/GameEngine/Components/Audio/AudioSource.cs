using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using GameEngine.Assets;
using GameEngine.Core;

namespace GameEngine.Components.Audio
{
    class AudioSource : Component
    {
        AudioFile audioFile;
        AudioChannels sourceType;

        float range;
        public float Range { get { return range; } set { this.range = value; } }

        public AudioSource(GameObject gameObject)
            : base(gameObject)
        {

        }

        public override void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
