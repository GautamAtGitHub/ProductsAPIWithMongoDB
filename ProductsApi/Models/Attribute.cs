using MongoDB.Bson.Serialization.Attributes;

public class Attribute
{
    [BsonElement("fantastic")]
    public Fantastic fantastic { get; set; }
    [BsonElement("rating")]
    public Rating rating { get; set; }
}