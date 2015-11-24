
using Microsoft.Xna.Framework;
namespace GameEngine.Core
{
    /// <summary>
    /// Easy access to random numbers without instanciating the System.Random class.
    /// </summary>
    public static class Random
    {
        static System.Random random;

        static Random()
        {
            random = new System.Random();
        }
        /// <summary>
        /// Returns a nonnegative random number less than the specified maximum.
        /// </summary>
        /// <returns>Random integer</returns>
        public static int RandomInt()
        {
            return random.Next();
        }
        /// <summary>
        /// Returns a nonnegative random number less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue
        ///     must be greater than or equal to zero.</param>
        /// <returns>A 32-bit signed integer greater than or equal to zero, and less than maxValue;
        ///     that is, the range of return values ordinarily includes zero but not maxValue.
        ///     However, if maxValue equals zero, maxValue is returned.</returns>
        public static int RandomInt(int maxValue)
        {
            return random.Next(maxValue);
        }
        /// <summary>
        /// Returns a nonnegative random number less than the specified maximum.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be
        ///    greater than or equal to minValue.</param>
        /// <returns>A 32-bit signed integer greater than or equal to zero, and less than maxValue;
        ///     that is, the range of return values ordinarily includes zero but not maxValue.
        ///     However, if maxValue equals zero, maxValue is returned.</returns>
        public static int RandomInt(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }
        /// <summary>
        /// Returns a nonnegative random number between 0.0 and 1.0.
        /// </summary>
        /// <returns>A float greater than or equal to zero, and less than maxValue;
        ///     that is, the range of return values ordinarily includes zero but not maxValue.
        ///     However, if maxValue equals zero, maxValue is returned.</returns>
        public static float RandomFloat()
        {
            return (float)random.NextDouble();
        }
        /// <summary>
        /// Returns a nonnegative random number less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be
        ///     greater than or equal to zero.</param>
        /// <returns>A float greater than or equal to zero, and less than maxValue;
        ///     that is, the range of return values ordinarily includes zero but not maxValue.
        ///     However, if maxValue equals zero, maxValue is returned.</returns>
        public static float RandomFloat(float maxValue)
        {
            return (float)random.NextDouble() * maxValue;
        }
        /// <summary>
        /// Returns a nonnegative random number less than the specified maximum.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be
        ///     greater than or equal to minValue.</param>
        /// <returns>A float greater than or equal to zero, and less than maxValue;
        ///     that is, the range of return values ordinarily includes zero but not maxValue.
        ///     However, if maxValue equals zero, maxValue is returned.</returns>
        public static float RandomFloat(float minValue, float maxValue)
        {
            return (float)(random.NextDouble() - minValue) * (maxValue + minValue);
        }
        /// <summary>
        /// Returns a random Color.
        /// </summary>
        /// <returns>A Color generated using three random floats.</returns>
        public static Color RandomColor()
        {
            return new Color(RandomFloat(), RandomFloat(), RandomFloat());
        }
        /// <summary>
        /// Returns a random Color.
        /// </summary>
        /// <param name="excludeR">Excludes the red value from generating.</param>
        /// <param name="excludeG">Excludes the green value from generating.</param>
        /// <param name="excludeB">Excludes the blue value from generating.</param>
        /// <returns>A Color generated using three random floats.</returns>
        public static Color RandomColor(bool excludeR, bool excludeG, bool excludeB)
        {
            return new Color(!excludeR ? RandomFloat() : 0, !excludeG ? RandomFloat() : 0, !excludeB ? RandomFloat() : 0);
        }
    }
}
