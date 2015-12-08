using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Core
{
    public static class Settings
    {
        static Settings()
        {
            assets = new List<Asset>();
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
    }
}
