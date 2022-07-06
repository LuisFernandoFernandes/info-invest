using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trade.application.generics.models
{
    [BsonIgnoreExtraElements]
    public class GenericModel : IGenericModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID", Order = 1)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public void Dispose() { }

        public object Minify(Func<object> minify = null)
        {
            if (minify != null) { return minify; }
            return this;
        }
    }
}