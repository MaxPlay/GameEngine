using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace GameEngine.Font
{
    public class BitmapEncoder
    {
        /// <summary>
        /// The image that is used as the source for the spritefont.
        /// </summary>
        public Bitmap Source;
        /// <summary>
        /// The character-Elements representing the chars and positions on the source image.
        /// </summary>
        public List<CharElement> Chars;

        public BitmapEncoder()
        {
            Source = new Bitmap(1, 1);
            Chars = new List<CharElement>();
        }

        /// <summary>
        /// Encodes the bitmapfont into a new file with the given name.
        /// </summary>
        /// <param name="fontname">The name of the file.</param>
        public void Encode(string fontname)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Create(fontname + ".gef")))
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
            writer.Write(Chars.Count);

            for (int i = 0; i < Chars.Count; i++)
            {
                //The current char that is used as the key for the grafic
                writer.Write(Chars[i].Name);

                long ByteSize;
                string md5Hash;
                using (MemoryStream ms = new MemoryStream())
                {
                    this.Copy(Source, Chars[i].ToRectangle()).Save(ms, ImageFormat.Png);
                    ByteSize = ms.Length;

                    //Generate md5 hash of the image
                    MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
                    Byte[] hash = provider.ComputeHash(ms);

                    StringBuilder hashBuilder = new StringBuilder();

                    for (int j = 0; j < hash.Length; j++)
                    {
                        hashBuilder.Append(hash[j].ToString("X2"));
                    }

                    md5Hash = hashBuilder.ToString();
                }

                //Size of the byte-array of the image
                writer.Write(ByteSize);

                using (MemoryStream mStream = new MemoryStream())
                {
                    this.Copy(Source, Chars[i].ToRectangle()).Save(mStream, ImageFormat.Png);
                    //writes the image into the stream
                    writer.Write(mStream.ToArray());
                }

                //write the md5-hash of the image into the stream for verification
                writer.Write(md5Hash);
            }
        }

        private Bitmap Copy(Bitmap srcBitmap, System.Drawing.Rectangle section)
        {
            // Create the new bitmap and associated graphics object
            Bitmap bmp = new Bitmap(section.Width, section.Height,PixelFormat.Format32bppArgb);
            bmp.SetResolution(srcBitmap.HorizontalResolution, srcBitmap.VerticalResolution);
            Graphics g = Graphics.FromImage(bmp);

            g.Clear(Color.Transparent);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw the specified section of the source bitmap to the new one
            g.DrawImage(srcBitmap, 0, 0, section, GraphicsUnit.Pixel);

            // Clean up
            g.Dispose();

            // Return the bitmap
            return bmp;
        }
    }
}
