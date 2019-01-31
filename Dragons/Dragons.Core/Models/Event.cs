using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dragons.Core.Types;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Dragons.Core.Models
{
    /// <summary>
    /// Represents a typed event that occurred during the course of the game.
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Event
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Event()
        {
            Pieces = new List<Piece>();
        }

        /// <summary>
        /// Id of player who the event is about.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public Player Player { get; set; }

        /// <summary>
        /// Type of event that occurred.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public EventType Type { get; set; }
        
        /// <summary>
        /// List of pieces involved in the event.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public List<Piece> Pieces { get; set; }

        /// <summary>
        /// Amount of mana related to the event.
        /// </summary>
        [BsonElement]
        [BsonRequired]
        [Required]
        public int Mana { get; set; }

        /// <summary>
        /// Spell used in the attack events.
        /// </summary>
        [BsonElement]
        public Spell Spell { get; set; }

        /// <summary>
        /// Returns pretty-printed string
        /// </summary>
        /// <returns>Returns pretty-printed string</returns>
        public override string ToString()
        {
            return $"{Type} event for {Player.Name} {Spell?.ToString() ?? string.Empty} [mana:{Mana}].{Environment.NewLine}{string.Join($",{Environment.NewLine}", Pieces)}";
        }
    }
}
