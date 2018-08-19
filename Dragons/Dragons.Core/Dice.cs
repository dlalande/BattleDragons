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
        /// Returns a random number from 0 to the given maximum - 1.
        /// </summary>
        /// <param name="maximum">Maximum number on dice.</param>
        /// <returns>Returns a random number from 0 to the given size.</returns>
        public static int Roll(int maximum)
        {
            return random.Next(maximum);
        }
    }
}
