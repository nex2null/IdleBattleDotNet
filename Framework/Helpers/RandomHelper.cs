using System;
namespace Framework.Helpers
{
    public static class RandomHelper
    {
        // Properties
        private static Random _random = new Random();

        /// <summary>
        /// Gets a random number between min and max
        /// </summary>
        public static int GetRandomInt(int min, int max)
        {
            return _random.Next(min, max);
        }

        /// <summary>
        /// Gets a random bool (coin flip)
        /// </summary>
        public static bool CoinFlip()
        {
            return RandomHelper.GetRandomInt(0, 1) == 0;
        }
    }
}