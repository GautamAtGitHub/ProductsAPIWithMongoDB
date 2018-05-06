using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Rating
{
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("type")]
    public string Type { get; set; }
    [BsonElement("value")]
    [BsonRepresentation(BsonType.Decimal128, AllowTruncation=true)]
    public decimal Value { get; set; }
}
