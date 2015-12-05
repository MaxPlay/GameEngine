﻿using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Pipeline
{
    class ProcessorContext : ContentProcessorContext
    {
        private CBLogger logger;
        private OpaqueDataDictionary parameters;

        public override void AddDependency(string filename)
        {

        }

        public override void AddOutputFile(string filename)
        {

        }

        public override TOutput BuildAndLoadAsset<TInput, TOutput>(ExternalReference<TInput> sourceAsset, string processorName, OpaqueDataDictionary processorParameters, string importerName)
        {
            throw new NotImplementedException();
        }

        public override ExternalReference<TOutput> BuildAsset<TInput, TOutput>(ExternalReference<TInput> sourceAsset, string processorName, OpaqueDataDictionary processorParameters, string importerName, string assetName)
        {
            throw new NotImplementedException();
        }

        public override string BuildConfiguration
        {
            get { return string.Empty; }
        }

        public override TOutput Convert<TInput, TOutput>(TInput input, string processorName, OpaqueDataDictionary processorParameters)
        {
            throw new NotImplementedException();
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

        public override string OutputFilename
        {
            get { return string.Empty; }
        }

        public override OpaqueDataDictionary Parameters
        {
            get { return parameters; }
        }

        public override TargetPlatform TargetPlatform
        {
            get { return TargetPlatform.Windows; }
        }

        public override GraphicsProfile TargetProfile
        {
            get { return GraphicsProfile.HiDef; }
        }

        public ProcessorContext()
            : base()
        {
            this.logger = new CBLogger();
            this.parameters = new OpaqueDataDictionary();
        }
    }
}
