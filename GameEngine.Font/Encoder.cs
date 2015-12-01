using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Security.Cryptography;

namespace GameEngine.Font
{
    /// <summary>
    /// Converts regular registered font to a gameengine-font-file.
    /// </summary>
    public sealed class Encoder
    {
        string chars;
        Dictionary<char, Bitmap> charGraphics;

        public Encoder(string[] args)
        {
            if (args.Length == 0)
                return;

            charGraphics = new Dictionary<char, Bitmap>();

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
                        if (++i < args.Length)
                        {
                            int newSize = 10;
                            if (int.TryParse(args[i], out newSize))
                                font.Size = newSize;
                        }
                        break;

                    case "-c":
                        if (++i < args.Length)
                        {
                            chars = args[i];
                        }
                        break;
                }
            }

            for (int i = 0; i < chars.Length; i++)
                CreateSpriteFont(font, chars[i]);

            using (BinaryWriter writer = new BinaryWriter(File.Create(font.Fontname + ".gef")))
            {
                BuildFont(writer);
            }
        }

        /// <summary>
        /// Builds the characters generated from the creator and writes them into a binarystream.
        /// </summary>
        /// <param name="writer">BinaryWriter for writing the file.</param>
        private void BuildFont(BinaryWriter writer)
        {
            UnicodeEncoding uniEncoding = new UnicodeEncoding();

            //Number of characters
            writer.Write(chars.Length);

            foreach (char key in charGraphics.Keys)
            {
                //The current char that is used as the key for the grafic
                writer.Write(key);

                long ByteSize;
                string md5Hash;
                using (MemoryStream ms = new MemoryStream())
                {
                    charGraphics[key].Save(ms, ImageFormat.Png);
                    ByteSize = ms.Length;

                    //Generate md5 hash of the image
                    MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
                    Byte[] hash = provider.ComputeHash(ms);

                    StringBuilder hashBuilder = new StringBuilder();

                    for (int i = 0; i < hash.Length; i++)
                    {
                        hashBuilder.Append(hash[i].ToString("X2"));
                    }

                    md5Hash = hashBuilder.ToString();
                }

                //Size of the byte-array of the image
                writer.Write(ByteSize);

                using (MemoryStream mStream = new MemoryStream())
                {
                    charGraphics[key].Save(mStream, ImageFormat.Png);
                    //writes the image into the stream
                    writer.Write(mStream.ToArray());
                }

                //write the md5-hash of the image into the stream for verification
                writer.Write(md5Hash);
            }
        }

        /// <summary>
        /// Creates a bitmap of the given character with the fonttemplate as settingsparameter and adds them to the dictionary.
        /// </summary>
        /// <param name="template">The fonttemplate used to represent the required font.</param>
        /// <param name="character">The character to encode.</param>
        private void CreateSpriteFont(FontTemplate template, char character)
        {
            if (charGraphics.ContainsKey(character))
                return;

            Bitmap objBmpImage = new Bitmap(1, 1);

            // Create the Font object for the image text drawing.
            System.Drawing.Font objFont = template.CreateFont();

            // Create a graphics object to measure the text's width and height.
            Graphics objGraphics = Graphics.FromImage(objBmpImage);

            // This is where the bitmap size is determined.
            SizeF measure = objGraphics.MeasureString(character.ToString(), objFont);

            int charWidth = (int)measure.Width;
            int charHeight = (int)measure.Height;

            // Create the bmpImage again with the correct size for the text and font.
            objBmpImage = new Bitmap(objBmpImage, new Size(charWidth, charHeight));

            // Add the colors to the new bitmap.
            objGraphics = Graphics.FromImage(objBmpImage);

            // Set Background color
            objGraphics.Clear(Color.Transparent);
            objGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            objGraphics.DrawString(character.ToString(), objFont, new SolidBrush(Color.FromArgb(255, 255, 255)), 0, 0);
            objGraphics.Flush();

            charGraphics.Add(character, objBmpImage);
        }
    }
}
