﻿using GameEngine.Core;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace GameEngine.Assets
{
    class ImageMap : Image
    {
        protected Dictionary<int, Rectangle> subimages;

        public Rectangle this[int index]
        {
            get
            {
                if (subimages.Count <= index || index < 0)
                    if (texture.Bounds != null)
                        return texture.Bounds;
                    else
                        return Rectangle.Empty;
                else
                    return subimages[index];
            }
        }

        public ImageMap() : base(string.Empty, string.Empty) { }
        public ImageMap(string name, string filename)
            : base(name, filename)
        {
            subimages = new Dictionary<int, Rectangle>();
        }

        public override void Load()
        {
            try
            {
                using (FileStream stream = new FileStream(Settings.GetLocation(typeof(ImageMap)) + this.filename + ".xml", FileMode.Open))
                {
                    XmlReader reader = XmlReader.Create(stream);

                    Rectangle rect = new Rectangle();
                    int id = 0;

                    while (reader.Read())
                    {

                        if (reader.NodeType == XmlNodeType.Element)
                            switch (reader.Name)
                            {
                                case "x":
                                    reader.Read();
                                    rect.X = int.Parse(reader.Value);
                                    break;
                                case "y":
                                    reader.Read();
                                    rect.Y = int.Parse(reader.Value);
                                    break;
                                case "width":
                                    reader.Read();
                                    rect.Width = int.Parse(reader.Value);
                                    break;
                                case "height":
                                    reader.Read();
                                    rect.Height = int.Parse(reader.Value);
                                    break;
                                case "id":
                                    reader.Read();
                                    id = int.Parse(reader.Value);
                                    break;
                            }

                        if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Area")
                        {
                            subimages.Add(id, rect);
                            rect = new Rectangle();
                        }

                        if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "ImageMap")
                            break;
                    }

                    reader.Close();
                    stream.Close();
                }
                base.Load();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public static new Asset Create(string filename, string name)
        {
            return Settings.AquireAsset<ImageMap>(filename, name);
        }
    }
}
