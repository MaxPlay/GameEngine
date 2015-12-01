using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Font
{
    public class CorruptedFontFileException : System.Exception
    {
        private string fileName;

        /// <summary>
        /// The filename of the file that was loaded. Check the log.
        /// </summary>
        public string FileName { get { return fileName; } set { this.fileName = value; } }

        /// <summary>
        /// Initializes a new instance of the GameEngine.Exception.FileNotSupportedException class with
        /// its message string set to a system-supplied message and its HRESULT set to
        /// COR_E_FILENOTFOUND.
        /// </summary>
        public CorruptedFontFileException()
            : base()
        { fileName = string.Empty; }

        /// <summary>
        /// Initializes a new instance of the System.IO.FileNotFoundException class with
        /// its message string set to message and its HRESULT set to COR_E_FILENOTFOUND.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood
        /// by humans. The caller of this constructor is required to ensure that this
        /// string has been localized for the current system culture.</param>
        public CorruptedFontFileException(string message)
            : base(message)
        { fileName = string.Empty; }


        /// <summary>
        /// Initializes a new instance of the System.IO.FileNotFoundException class with
        /// its message string set to message and its HRESULT set to COR_E_FILENOTFOUND.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood
        /// by humans. The caller of this constructor is required to ensure that this
        /// string has been localized for the current system culture.</param>
        /// <param name="filename">The filename that is described by the exception. If the
        /// entry is empty, a default log will be saved.</param>
        public CorruptedFontFileException(string message, string filename)
            : base(message)
        { FileName = filename; }

        /// <summary>
        /// Initializes a new instance of the System.IO.FileNotFoundException class with
        /// a specified error message and a reference to the inner exception that is
        /// the cause of this exception.
        /// </summary>
        /// <param name="message">A description of the error. The content of message is intended to be understood
        /// by humans. The caller of this constructor is required to ensure that this
        /// string has been localized for the current system culture.</param>
        /// <param name="innerException">The exception that is the cause of the current exception. If the innerException
        /// parameter is not null, the current exception is raised in a catch block that
        /// handles the inner exception.</param>
        public CorruptedFontFileException(string message, System.Exception innerException)
            : base(message, innerException)
        { fileName = string.Empty; }

        /// <summary>
        /// Initializes a new instance of the System.IO.FileNotFoundException class with
        /// the specified serialization and context information.
        /// </summary>
        /// <param name="info">An object that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">An object that contains contextual information about the source or destination.</param>
        protected CorruptedFontFileException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { fileName = string.Empty; }
    }
}
