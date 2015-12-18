using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using GameEngine.Core;
using Microsoft.Xna.Framework.Input;

namespace GameEngine.Pipeline
{
    public class InputManager
    {
        private string filename;
        private Dictionary<string, Keys> inputs;

        /// <summary>
        /// The filename of the file for the config. This requires the fileextension.
        /// </summary>
        /// <remarks>Default is "input.xml".</remarks>
        public string Filename { get { return this.filename; } set { this.filename = value; } }

        public InputManager()
            : this("input.xml")
        { }

        public InputManager(string Filename)
        {
            this.filename = Filename;
            this.LoadInputConfig();
        }

        /// <summary>
        /// Checks if an entry is in the inputs-list.
        /// </summary>
        /// <remarks>The entry is not case sensitive.</remarks>
        /// <param name="entry">The entry to check.</param>
        /// <returns>True if the entry is registered</returns>
        public bool ContainsInputEntry(string entry)
        {
            return inputs.ContainsKey(entry.ToLower());
        }

        /// <summary>
        /// Removes an input entry from the inputs-list.
        /// </summary>
        /// <remarks>The entry is not case sensitive.</remarks>
        /// <param name="entry">The entry to remove.</param>
        /// <returns>True if entry was removed successfully.</returns>
        public bool RemoveInputEntry(string entry)
        {
            return inputs.Remove(entry.ToLower());
        }

        /// <summary>
        /// Adds a new input to the inputmanager.
        /// </summary>
        /// <remarks>The entry is not case sensitive.</remarks>
        /// <param name="entry">The entry to add.</param>
        /// <param name="value">The Key to add.</param>
        /// <returns>True if entry could be added. False if the entry was already in the list.</returns>
        public bool AddInputEntry(string entry, Keys value)
        {
            if (inputs.ContainsKey(entry.ToLower()))
                return false;

            inputs.Add(entry.ToLower(), value);
            return true;
        }

        /// <summary>
        /// Overwrites the given entry. If the entry is not in the list, the method will return false.
        /// </summary>
        /// <remarks>The entry is not case sensitive.</remarks>
        /// <param name="entry">The entry to add.</param>
        /// <param name="value">The Key to add.</param>
        /// <returns>True if entry could be overwritten.</returns>
        public bool OverwriteInputEntry(string entry, Keys value)
        {
            if (inputs.ContainsKey(entry.ToLower()))
            {
                inputs[entry.ToLower()] = value;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Loads the config from the given file.
        /// </summary>
        public void LoadInputConfig()
        {
            if (System.IO.File.Exists(filename))
                throw new FileNotFoundException("File does not exist.", filename);

            Dictionary<string, Keys> ConfigDictionary = new Dictionary<string, Keys>();

            using (XmlReader reader = XmlReader.Create(filename))
            {
                string key;
                Keys value;

                while (reader.Read())
                {
                    if (reader.Name == "Input")
                    {
                        key = reader.GetAttribute("name");
                        reader.Read();
                        if (Enum.TryParse<Keys>(reader.Value, out value))
                            ConfigDictionary.Add(key.ToLower(), value);
                    }
                }
            }

            inputs = ConfigDictionary;
        }

        /// <summary>
        /// Saves the config to the given file.
        /// </summary>
        public void SaveInputConfig()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "    ";

            using (XmlWriter writer = XmlWriter.Create(filename, settings))
            {
                writer.WriteStartElement("Inputs");
                foreach (string key in this.inputs.Keys)
                {
                    writer.WriteStartElement("Input");
                    writer.WriteAttributeString("name", key.ToLower());
                    writer.WriteValue(this.inputs[key].ToString());
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }
    }
}
