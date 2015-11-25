using GameEngine.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Components.Audio
{
    class AudioListener : Component
    {
        List<AudioSource> sceneSources;

        public AudioListener(GameObject gameObject)
            : base(gameObject)
        {
            this.sceneSources = new List<AudioSource>();
        }

        public override void Reset()
        {
            this.sceneSources.Clear();
        }

        public void PlaySounds()
        {
            for (int i = 0; i < sceneSources.Count; i++)
            {
                if (sceneSources[i].Active)
                {
                    float Rangemodifier = sceneSources[i].Range - Vector2.Distance(sceneSources[i].Transform.Position, Transform.Position);

                    if (Rangemodifier < 0 && sceneSources[i].Range == 0)
                    {
                        sceneSources[i].Audio.Volume = 0;
                        sceneSources[i].Audio.Pitch = 0;
                        sceneSources[i].Audio.Pan = 0;
                        continue;
                    }

                    float outputVolume = Rangemodifier * sceneSources[i].Volume;
                    float panorama = sceneSources[i].Transform.Position.X - Transform.Position.X;

                    panorama /= sceneSources[i].Range;

                    sceneSources[i].Audio.Volume = outputVolume;
                    sceneSources[i].Audio.Pitch = 0;
                    sceneSources[i].Audio.Pan = panorama;
                }
            }
        }

        public void RegisterSource(AudioSource source)
        {
            this.sceneSources.Add(source);
        }

        public bool RemoveSource(AudioSource source)
        {
            return this.sceneSources.Remove(source);
        }
    }
}
