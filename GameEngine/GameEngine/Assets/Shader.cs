using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Core;
using GameEngine.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine.Assets
{
    public class Shader : Asset
    {
        private Effect HLSL_Shader;

        public Effect CompiledHLSL_Shader { get { return this.HLSL_Shader; } }

        public Shader(string name, string filename)
            : base(name, filename)
        {
            Load();
        }

        public override void Load()
        {
            using (StreamReader reader = new StreamReader("Shaders/" + filename))
            {
                EffectContent effectSource = new EffectContent();

                effectSource.Name = name;
                effectSource.Identity = new ContentIdentity(filename);
                effectSource.EffectCode = reader.ReadToEnd();

                EffectProcessor processor = new EffectProcessor();
                CompiledEffectContent compiledEffect = processor.Process(effectSource, new ProcessorContext());

                HLSL_Shader = new Effect(Bootstrap.graphics.GraphicsDevice, compiledEffect.GetEffectCode());
            }
        }
    }
}
