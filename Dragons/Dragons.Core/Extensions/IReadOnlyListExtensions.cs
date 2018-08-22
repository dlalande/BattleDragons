using System;
using System.Collections.Generic;

namespace Dragons.Core
{
    /// <summary>
    /// Class containing extension methods for <see cref="System.Collections.Generic.IReadOnlyList{T}">IReadOnlyLists</see>.
    /// </summary>
    public static class IReadOnlyListExtensions
    {
        /// <summary>
        /// Returns a randomly selected item from the list.
        /// </summary>
        /// <typeparam name="T">Type of random item.</typeparam>
        /// <param name="list">List of items to get random one from.</param>
        /// <returns>Returns a randomly selected item from the list.</returns>
        public static T Random<T>(this IReadOnlyList<T> list)
        {
            return list[Dice.Roll(list.Count)];
        }

        /// <summary>
        /// Returns a randomly selected pair of distinct items from the list.
        /// </summary>
        /// <typeparam name="T">Type of random item.</typeparam>
        /// <param name="list">List of items to get random pair from.</param>
        /// <returns>Returns a randomly selected pair of distinct items from the list.</returns>
        public static Tuple<T,T> RandomPair<T>(this IReadOnlyList<T> list)
        {
            var index1 = Dice.Roll(list.Count);
            var index2 = index1;
            while (index1.Equals(index2))
                index2 = Dice.Roll(list.Count);
            return new Tuple<T, T>(list[index1], list[index2]);
        }
    }
}
