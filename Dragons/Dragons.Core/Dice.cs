using System;

namespace Dragons.Core
{
    /// <summary>
    /// Class that allows for random numbers.
    /// </summary>
    public static class Dice
    {
        private static Random random = new Random();

        /// <summary>
        /// Returns a random number from 0 to the given size - 1.
        /// </summary>
        /// <param name="size">Size of dice.</param>
        /// <returns>Returns a random number from 0 to the given size.</returns>
        public static int Roll(int size)
        {
            return random.Next(size);
        }
    }
}
