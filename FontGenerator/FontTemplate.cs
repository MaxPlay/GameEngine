using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FontGenerator
{
    class FontTemplate
    {
        public string Fontname;
        public bool Bold;
        public bool Italic;
        public bool Underline;
        public int Size;

        public FontTemplate(string name)
        {
            this.Fontname = name;
            this.Bold = false;
            this.Italic = false;
            this.Underline = false;
            this.Size = 10;
        }

        public Font CreateFont()
        {
            FontStyle style = Bold ? FontStyle.Bold : FontStyle.Regular;
            style = Italic ? style | FontStyle.Italic : style;
            style = Underline ? style | FontStyle.Underline : style;

            return new Font(Fontname, Size, FontStyle.Bold, GraphicsUnit.Pixel);
        }
    }
}
