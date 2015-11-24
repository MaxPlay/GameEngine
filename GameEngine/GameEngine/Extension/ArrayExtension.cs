using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Extension
{
    /// <summary>
    /// Extends Arrays with new methods.
    /// </summary>
    public static class ArrayExtension
    {
        /// <summary>
        /// Returns a partial array from a full array.
        /// </summary>
        /// <typeparam name="T">Type of the array</typeparam>
        /// <param name="data">The array that calls the method.</param>
        /// <param name="index">The index to start from, beginning with 0.</param>
        /// <param name="length">The length of the subarray.</param>
        /// <returns>Returns the subarray of the given type.</returns>
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        /// <summary>
        /// Deepclones the array and returns the part of the full array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns>Returns the subarray of the given type.</returns>
        public static T[] SubArrayDeepClone<T>(this T[] data, int index, int length)
        {
            T[] arrCopy = new T[length];
            Array.Copy(data, index, arrCopy, 0, length);
            using (MemoryStream ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, arrCopy);
                ms.Position = 0;
                return (T[])bf.Deserialize(ms);
            }
        }
    }
}
