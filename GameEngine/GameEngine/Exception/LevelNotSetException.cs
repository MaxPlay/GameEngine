using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Exception
{
    public class LevelNotSetException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the System.IO.FileNotFoundException class with
        /// its message string set to a system-supplied message and its HRESULT set to
        /// COR_E_FILENOTFOUND.
        /// </summary>
        public LevelNotSetException()
            : base("Level.main is not set, yet a component tries to access it.")
        { }
        /// <summary>
        /// Initializes a new instance of the GameEngine.Exception.LevelNotSetException class with
        /// the specified serialization and context information.
        /// </summary>
        /// <param name="info">An object that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">An object that contains contextual information about the source or destination.</param>
        protected LevelNotSetException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }
}
