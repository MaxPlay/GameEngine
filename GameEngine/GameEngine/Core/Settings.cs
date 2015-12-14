using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GameEngine.Core
{
    /// <summary>
    /// This static class is used to save all the assets and the current runtime settings in the memory.
    /// </summary>
    public static class Settings
    {
        static Settings()
        {
            assets = new List<Asset>();
            locations = new Dictionary<Type, string>();
        }

        /// <summary>
        /// Returns the relative path for the requested assets.
        /// </summary>
        /// <param name="type">The requested type.</param>
        /// <returns>Returns the entry for the given type, if the type is not registered, an empty string will be returned.</returns>
        public static string GetLocation(Type type)
        {
            if (locations.ContainsKey(type))
                return locations[type];
            return string.Empty;
        }

        /// <summary>
        /// This represents a single Unit in Pixel.
        /// This is used by the renderer and the physics (and, yea, well, everything that require these).
        /// 
        /// Internally it is basically just a multiplier for the transform-position value.
        /// The UI-Renderer ignores this (by dividing the value again) without a zero-check, because a zero would set every position to the origin of the world.
        /// 
        /// So, why do we do this? The physics-engine Box2D advised me that a single pixel for a "1"-Unit would be terrible for calculations. Easy as that.
        /// </summary>
        public static float Unit;

        /// <summary>
        /// This is a list of assets.
        /// All assets in this list are currently loaded. If a new asset is required and it is not in the list, it will be loaded, userwise it will be provided.
        /// You can aquire new assets in the AquireAsset(Type, string, string)-method. It requires the AssetName and the filename (including the folder). These are the identifiers of the asset.
        /// By using both as identifier, it is possible to use the same asset with a different name. It will be copied in this list.
        /// You can unload the asset by calling the method UnloadAsset(string, string) or by unloading the level (which calls the same method). 
        /// Don't forget that calling the method at regular runtime will probably result in reference errors.
        /// </summary>
        private static List<Asset> assets;

        /// <summary>
        /// The locations of the assettypes.
        /// The autoloader of the engine will use these directories to load certain assettypes.
        /// </summary>
        private static Dictionary<Type, string> locations;

        /// <summary>
        /// Assigns a new location to the Assets. When this asset is loaded the loader will look into the given folder.
        /// </summary>
        /// <remarks>
        /// Only classes derived from Asset can be added as type.
        /// Types that are added again will be overwritten.
        /// </remarks>
        /// <typeparam name="T">The type the directory is assigned to.</typeparam>
        /// <param name="location">The directory we want to assign.</param>
        /// <returns>Is true when successfully assigned. Should anything go wrong the method will return false.</returns>
        public static bool AssignLocation(Type type, string location)
        {
            if (!typeof(Asset).IsAssignableFrom(type))
                return false;

            if (locations.ContainsKey(type))
            {
                locations.Remove(type);
            }

            locations.Add(type, location);
            return true;
        }

        /// <summary>
        /// A readonly list of all the active assets.
        /// </summary>
        public static Asset[] Assets { get { return assets.ToArray(); } }

        /// <summary>
        /// Loads or (if already loaded) just gets an asset.
        /// </summary>
        /// <typeparam name="T">Must be derived from Asset.</typeparam>
        /// <param name="name">The name of the Asset</param>
        /// <param name="filename">The filename of the Asset</param>
        /// <returns></returns>
        public static Asset AquireAsset<T>(string name, string filename) where T : Asset, new()
        {
            if (assets.Any(a => a.Filename.Equals(filename) && a.Name.Equals(name)))
                return assets.First(a => a.Filename.Equals(filename) && a.Name.Equals(name));

            Asset newAsset = (T)Activator.CreateInstance(typeof(T), name, filename);
            assets.Add(newAsset);
            return newAsset;
        }

        /// <summary>
        /// Removes loaded asset from memory. It returns true if unloading was a success, false when either there where problems in the process or the asset doesn't exist.
        /// </summary>
        /// <remarks>Unloading assets at game-runtime is dangerous and can cause a crash.</remarks>
        /// <param name="name">The name of the Asset</param>
        /// <param name="filename">The filename of the Asset</param>
        /// <returns>False when unloading failed.</returns>
        public static bool UnloadAsset(string name, string filename)
        {
            Asset oldAsset = assets.First(a => a.Filename.Equals(filename) && a.Name.Equals(name));

            if (oldAsset == null)
                return false;

            return assets.Remove(oldAsset);
        }

        /// <summary>
        /// Loads the locations from the Locations.xml.
        /// </summary>
        public static void LoadLocations()
        {
            using (XmlReader reader = XmlReader.Create(File.OpenRead("Resources/Locations.xml")))
            {
                while (reader.Read())
                {
                    if (reader.Name == "Location" && reader.NodeType == XmlNodeType.Element)
                    {
                        Type t = Type.GetType("GameEngine.Assets." + reader.GetAttribute("type"));
                        reader.Read();
                        string s = reader.Value;

                        //I know adding strings with += is not good, because it creates a copy of the string in memory.
                        //But I am completely fine with this here, because after all, we do this ONCE in the whole application.
                        if (s[s.Length - 1] != '/')
                            s += "/";
                        
                        AssignLocation(t, s);
                    }
                }
            }

            GC.Collect();
        }

        public static void LoadDefaultAssets()
        {
            using (XmlReader reader = XmlReader.Create(File.OpenRead("Resources/DefaultResources.xml")))
            {
                while (reader.Read())
                {
                    if (reader.Name == "Resource" && reader.NodeType == XmlNodeType.Element)
                    {
                        string name = reader.GetAttribute("name");
                        string type = reader.GetAttribute("type");
                        reader.Read();
                        string file = reader.Value;

                        Type requestedType = Type.GetType("GameEngine.Assets." + type);

                        if (requestedType == null)
                        {
                            Debug.LogError("Requested Type '{0}' not found.", type);
                            continue;
                        }

                        requestedType.GetMethod("Create").Invoke(null, new string[] { name, file });
                    }
                }
            }

            GC.Collect();
        }
    }
}
