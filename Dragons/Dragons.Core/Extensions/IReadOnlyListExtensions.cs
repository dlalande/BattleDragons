using Dragons.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dragons.Core
{
    /// <summary>
    /// Class containing extension methods for <see cref="System.Collections.Generic.IReadOnlyList{T}">IReadOnlyLists</see>.
    /// </summary>
    public static class IReadOnlyListExtensions
    {
        /// <summary>
        /// Returns the dragons that are not dead.
        /// </summary>
        /// <typeparam name="T">Type of Dragon.</typeparam>
        /// <param name="list">List of dragons.</param>
        /// <returns>Returns the dragons that are not dead.</returns>
        public static IReadOnlyList<T> Alive<T>(this IReadOnlyList<T> list) where T : Dragon 
        {
            return list.Where<T>(i => !i.IsDead).ToList();
        }

        /// <summary>
        /// Returns the spells that cost no more than the given mana.
        /// </summary>
        /// <typeparam name="T">Type of spell.</typeparam>
        /// <param name="list">List of spells.</param>
        /// <param name="mana">Mana to determine cost.</param>
        /// <returns>Returns the items that cost no more than the given mana.</returns>
        public static IReadOnlyList<T> Costing<T>(this IReadOnlyList<T> list, int mana) where T : Spell 
        {
            return list.Where<T>(i => i.ManaCost <= mana).ToList();
        }

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
