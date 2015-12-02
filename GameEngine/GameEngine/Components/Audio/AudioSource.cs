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
        SoundEffectInstance audio;
        AudioChannels sourceType;

        float range;
        public float Range { get { return range; } set { this.range = value; } }
        bool active;
        public bool Active { get { return this.active; } set { this.active = value; } }
        float volume;
        public float Volume { get { return this.volume; } set { this.volume = Math.Abs(value); } }

        public AudioChannels SourceType { get { return this.sourceType; } }
        public SoundEffectInstance Audio { get { return this.audio; } set { this.audio = value; } }

        public bool Looped { get { return this.audio.IsLooped; } set { if(this.audio != null) this.audio.IsLooped = value; } }

        public AudioSource()
            : base(null)
        {

        }

        public AudioSource(GameObject gameObject)
            : base(gameObject)
        {
            Reset();
        }

        public override void Reset()
        {
            this.range = 1;
            this.active = true;
            this.volume = 1;
            this.Looped = false;
            this.sourceType = AudioChannels.Stereo;
        }

        public void AddSound(AudioFile audiofile)
        {
            this.audio = audiofile.SoundEffect.CreateInstance();
        }
    }
}
