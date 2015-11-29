using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontGenerator
{
    /// <summary>
    /// The template for the fontencoder to generate the files.
    /// </summary>
    class FontTemplate
    {
        /// <summary>
        /// The name of the font
        /// </summary>
        public string Fontname;
        /// <summary>
        /// Is the font bold?
        /// </summary>
        public bool Bold;
        /// <summary>
        /// Is the font italic?
        /// </summary>
        public bool Italic;
        /// <summary>
        /// Is the font underlined?
        /// </summary>
        public bool Underline;
        /// <summary>
        /// The size of the font
        /// </summary>
        public int Size;

        public FontTemplate(string name)
        {
            this.Fontname = name;
            this.Bold = false;
            this.Italic = false;
            this.Underline = false;
            this.Size = 10;
        }

        /// <summary>
        /// Creates a usable font for the GDI+-Renderer from this template
        /// </summary>
        /// <returns>GDI+-Font for rendering</returns>
        public Font CreateFont()
        {
            FontStyle style = Bold ? FontStyle.Bold : FontStyle.Regular;
            style = Italic ? style | FontStyle.Italic : style;
            style = Underline ? style | FontStyle.Underline : style;

            return new Font(Fontname, Size, FontStyle.Bold, GraphicsUnit.Pixel);
        }
    }
}
