using GameEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GameEngine.Extension;
using Microsoft.Xna.Framework.Audio;

namespace GameEngine.Assets
{
    class AudioFile : Asset
    {
        SoundEffect soundEffect;
        string encodername;

        public SoundEffect SoundEffect { get { return this.soundEffect; } }

        public AudioFile() : base(string.Empty, string.Empty) { }

        public AudioFile(string name, string filename)
            : base(name, filename)
        {
            soundEffect = null;
            encodername = string.Empty;
        }

        public override void Load()
        {
            string extension = Path.GetExtension(this.filename);

            switch (extension)
            {
                case ".wav":
                    LoadWaveFromFile();
                    break;
                case ".ogg":
                    LoadOggFromFile();
                    break;
                default:
                    throw new Exception.FileNotSupportedException("File not supported.", this.filename);
            }
        }

        private void LoadWaveFromFile()
        {
            _correctTheFileLength("Audio/" + this.filename);

            using (Stream stream = new FileStream("Audio/" + this.filename, FileMode.Open))
            {
                soundEffect = SoundEffect.FromStream(stream);
            }
        }

        private void LoadOggFromFile()
        {
            soundEffect = OggSharp.OggToWave.LoadOggAsSoundEffect("Audio/" + this.filename);
        }

        /// <summary>
        /// Corrects wave filelengths.
        /// </summary>
        /// <remarks>Copied from http://stackoverflow.com/questions/29549734/why-does-soundeffect-fromstream-throw-invalidoperationexception-when-reading</remarks>
        /// <param name="filename"></param>
        private void _correctTheFileLength(string filename)
        {
            byte[] wav = File.ReadAllBytes(filename);

            string riff = Encoding.ASCII.GetString(wav.SubArray(0, 4));
            if (riff != "RIFF" || wav.Length < 8) //check for RIFF tag and length
                return;

            int reportedLength = wav[4] + wav[5] * 256 + wav[6] * 65536 + wav[7] * 16777216;
            int actualLength = wav.Length - 8;
            if (reportedLength != actualLength)
            {
                wav[4] = (byte)(actualLength & 0xFF);
                wav[5] = (byte)((actualLength >> 8) & 0xFF);
                wav[6] = (byte)((actualLength >> 16) & 0xFF);
                wav[7] = (byte)((actualLength >> 24) & 0xFF);
                File.WriteAllBytes(filename, wav);
            }
        }

        public static Asset Create(string filename, string name)
        {
            return Settings.AquireAsset<AudioFile>(filename, name);
        }
    }
}
