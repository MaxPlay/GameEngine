using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Core
{
    public abstract class Asset
    {
        private static long AssetIDDepth;
        protected bool loaded;

        protected string name;
        protected string filename;
        public long AssetID { get; private set; }
        
        public string Name { get { return name; } }
        public string Filename { get { return filename; } }
        public bool LoadingComplete { get { return loaded; } }

        public Asset(string name, string filename)
        {
            this.AssetID = AssetIDDepth++;
            this.name = name;
            this.filename = filename;
        }

        public abstract void Load();
    }
}