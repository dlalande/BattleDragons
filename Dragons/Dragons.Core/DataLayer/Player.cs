using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Dragons.Core
{
    /// <summary>
    /// Represents the player information.
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Player
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="playerId">Id pf player. Use a GUID or equivalent.</param>
        /// <param name="name">Name of player. Optional</param>
        public Player(string playerId, string name = null)
        {
            _id = playerId;
            PlayerId = playerId;
            Name = name;
        }

        /// <summary>
        /// Used internally to have an immutable field for <see cref="GetHashCode">GetHashCode</see>.
        /// </summary>
        private readonly string _id;

        /// <summary>
        /// Id of player. Use a GUID or equivalent.
        /// </summary>
        [BsonId]
        [BsonRequired]
        [Required]
        public string PlayerId { get; set; }

        /// <summary>
        /// Name of player. 
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Returns true if the given player's PlayerId matches.
        /// </summary>
        /// <param name="other">Player to test.</param>
        /// <returns>Returns true if the given player's PlayerId matches.</returns>
        protected bool Equals(Player other)
        {
            return string.Equals(PlayerId, other.PlayerId, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Returns true if the given object is a player and the PlayerId matches.
        /// </summary>
        /// <param name="obj">Object to test.</param>
        /// <returns>Returns true if the given object is a player and the PlayerId matches.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Player other && Equals(other);
        }

        /// <summary>
        ///  A hash code for the current object.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return StringComparer.InvariantCultureIgnoreCase.GetHashCode(_id);
        }
    }
}
