using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Pipeline
{
    class ImporterContext : ContentImporterContext
    {
        ContentBuildLogger logger;

        public ImporterContext()
            : base()
        {
            logger = new CBLogger();
        }

        public override void AddDependency(string filename)
        {

        }

        public override string IntermediateDirectory
        {
            get { return string.Empty; }
        }

        public override ContentBuildLogger Logger
        {
            get { return logger; }
        }

        public override string OutputDirectory
        {
            get { return string.Empty; }
        }
    }
}
