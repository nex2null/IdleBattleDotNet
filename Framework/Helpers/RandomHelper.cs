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
            return _random.Next(min, max + 1);
        }

        /// <summary>
        /// Gets a random decimal between two numbers
        /// </summary>
        public static decimal GetRandomDecimal(int min, int max)
        {
            var next = _random.NextDouble();
            return Convert.ToDecimal(min + (next * (max - min)));
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