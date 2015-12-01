using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Xna.Framework.Color;

namespace GameEngine.Font
{
    public sealed class Decoder
    {
        public Decoder()
        {

        }
        /// <summary>
        /// Decodes the font to a char-texture dictionary. The font is represented by a BinaryReader.
        /// </summary>
        /// <param name="reader">BinaryReader of the active GEF-File</param>
        /// <returns>Dictionary of the chars and the textures.</returns>
        public Dictionary<char, Texture2D> Convert(BinaryReader reader, GraphicsDevice device)
        {
            Dictionary<char, Texture2D> fontdictionary = new Dictionary<char, Texture2D>();

            // Charactercount is needed to know how many chars are in the file
            int charactercount = reader.ReadInt32();

            for (int i = 0; i < charactercount; i++)
            {
                //The current char that will be represented by a texture
                char currentChar = reader.ReadChar();

                //The size of the image byte-array as int64
                long imageSize = reader.ReadInt64();
                Bitmap bitmap;

                //The image as byte-Array
                byte[] imageBytes = reader.ReadBytes((int)imageSize);

                string md5Hash;
                using (MemoryStream memStream = new MemoryStream())
                {
                    //Get the image from the stream
                    memStream.Write(imageBytes, 0, (int)imageSize);
                    memStream.Seek(0, SeekOrigin.Begin);
                    bitmap = (Bitmap)Bitmap.FromStream(memStream);

                    /***
                     * I know that this is not appropriate, but let me explain why I did this:
                     * 
                     * The System.Drawing.Bitmap.FromStream()-Method requires the stream to stay open. Otherwise the bitmap
                     * will be destroyed immediatly. To prevent this, I keep the stream open until the Texture2D-Object is
                     * generated. Closing the stream at this point will result in a CGI+-Error.
                     * 
                     * If you find a neat, logic and not-hacking solution for this problem, feel free to write the code, test
                     * it and do a merge-request.
                     * 
                     * Cheers.
                     ***/
                    using (MemoryStream ms = new MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Png);

                        //Generate the md5 hash of the image for verification
                        MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
                        Byte[] hash = provider.ComputeHash(ms);

                        StringBuilder hashBuilder = new StringBuilder();

                        for (int j = 0; j < hash.Length; j++)
                        {
                            hashBuilder.Append(hash[j].ToString("X2"));
                        }

                        md5Hash = hashBuilder.ToString();
                    }

                    //Read the md5 from stream and check it with the previously generated md5.
                    //If the hashs are equal, the reading worked. Otherwise the engine throws
                    //an exception.
                    string md5Check = reader.ReadString();
                    if (!md5Hash.Equals(md5Check))
                        throw new CorruptedFontFileException("File of type Font is corrupted. Error testing the checksum of a character.");
                    
                    //Generate texture from bitmap
                    Texture2D tex = GetTexture2DFromBitmap(bitmap, device);

                    //and add it to the dictionary.
                    fontdictionary.Add(currentChar, tex);
                }
            }

            return fontdictionary;
        }

        /// <summary>
        /// Takes a GDI+ Bitmap and turns it into an XNA-Texture2D.
        /// </summary>
        /// <param name="bitmap">The bitmap to convert.</param>
        /// <param name="device">The GraphicsDevice we want to decode to.</param>
        /// <returns></returns>
        private Texture2D GetTexture2DFromBitmap(Bitmap bitmap, GraphicsDevice device)
        {
            Texture2D tex = new Texture2D(device, bitmap.Width, bitmap.Height, false, SurfaceFormat.Color);

            BitmapData data = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

            int bufferSize = data.Height * data.Stride;

            //create data buffer 
            byte[] bytes = new byte[bufferSize];

            // copy bitmap data into buffer
            Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);

            // copy our buffer to the texture
            tex.SetData(bytes);

            // unlock the bitmap data
            bitmap.UnlockBits(data);

            return tex;
        }
    }
}
