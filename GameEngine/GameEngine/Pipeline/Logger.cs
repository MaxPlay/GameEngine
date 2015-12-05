using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GameEngine.Core;

namespace GameEngine.Pipeline
{
    class CBLogger : ContentBuildLogger
    {
        public CBLogger()
            : base()
        {

        }

        public override void LogImportantMessage(string message, params object[] messageArgs)
        {
            Debug.LogImportant(message, messageArgs);
        }

        public override void LogMessage(string message, params object[] messageArgs)
        {
            Debug.Log(message, messageArgs);
        }

        public override void LogWarning(string helpLink, ContentIdentity contentIdentity, string message, params object[] messageArgs)
        {
            Debug.LogWarning(message, messageArgs);
            Debug.LogWarning(contentIdentity.SourceFilename);
            Debug.LogWarning(contentIdentity.SourceTool);
            Debug.LogWarning(contentIdentity.FragmentIdentifier);
            Debug.LogWarning(helpLink);
        }
    }
}
