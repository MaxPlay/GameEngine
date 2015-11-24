using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Core
{
    public abstract class Asset
    {
        private static long AssetIDDepth;

        protected string name;
        protected string filename;
        public long AssetID { get; private set; }
        
        protected string Name { get { return name; } }
        protected string Filename { get { return filename; } }

        public Asset(string name, string filename)
        {
            this.AssetID = AssetIDDepth++;
            this.name = name;
            this.filename = filename;
        }

        public abstract void Load();
    }
}