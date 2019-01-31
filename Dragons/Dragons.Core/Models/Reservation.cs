using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace Dragons.Core.Models
{
    /// <summary>
    /// Represents a player's game reservation while waiting for another player to join the game.
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Reservation
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Reservation()
        {
            Created = DateTime.UtcNow;
        }
       
        /// <summary>
        /// Player holding the reservation while waiting for another player to join.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public Player Player { get; set; }
        
        /// <summary>
        /// Date and time in UTC the reservation was created.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public DateTime Created { get; set; }
    }
}
