﻿using GameEngine.Core;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace GameEngine.Assets
{
    public class Material : Asset
    {
        public Shader Shader;

        public Material() : base(string.Empty, string.Empty) { }
        public Material(string name, string filename)
            : base(name, filename)
        {
            Load();
        }

        public override void Load()
        {
            try
            {
                using (XmlReader reader = XmlReader.Create(File.Open(Settings.GetLocation(typeof(Material)) + this.Filename, FileMode.Open)))
                {
                    while (reader.Read())
                    {

                    }
                }
                loaded = true;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public static Asset Create(string filename, string name)
        {
            return Settings.AquireAsset<Material>(filename, name);
        }
    }
}
