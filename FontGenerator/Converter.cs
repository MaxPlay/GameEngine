using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace FontGenerator
{
    public class Converter
    {
        string chars;

        public Converter(string[] args)
        {
            if (args.Length == 0)
                return;

            chars = @" abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789()<>/\!.,";
            FontTemplate font = new FontTemplate(args[0]);

            for (int i = 1; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-b":
                        font.Bold = true;
                        break;

                    case "-i":
                        font.Italic = true;
                        break;

                    case "-u":
                        font.Underline = true;
                        break;

                    case "-s":
                        if (i++ < args.Length)
                        {
                            int newSize = 10;
                            if (int.TryParse(args[i], out newSize))
                                font.Size = newSize;
                        }
                        break;
                }
            }
            CreateBitmapImage(font).Save("test.bmp", ImageFormat.Bmp);
        }


        private Bitmap CreateBitmapImage(FontTemplate template)
        {
            Bitmap objBmpImage = new Bitmap(1, 1);

            int intWidth = 0;
            int intHeight = 0;

            // Create the Font object for the image text drawing.
            Font objFont = template.CreateFont();

            // Create a graphics object to measure the text's width and height.
            Graphics objGraphics = Graphics.FromImage(objBmpImage);

            // This is where the bitmap size is determined.
            intWidth = (int)objGraphics.MeasureString(chars, objFont).Width;
            intHeight = (int)objGraphics.MeasureString(chars, objFont).Height;

            // Create the bmpImage again with the correct size for the text and font.
            objBmpImage = new Bitmap(objBmpImage, new Size(intWidth, intHeight));

            // Add the colors to the new bitmap.
            objGraphics = Graphics.FromImage(objBmpImage);

            // Set Background color
            objGraphics.Clear(Color.White);
            objGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            objGraphics.DrawString(chars, objFont, new SolidBrush(Color.FromArgb(102, 102, 102)), 0, 0);
            objGraphics.Flush();

            return (objBmpImage);
        }
    }
}
