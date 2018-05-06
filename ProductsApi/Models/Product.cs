using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Product
{   
    [BsonId]
    public ObjectId _Id { get; set; }
    
    [BsonElement("id")]
    public int Id { get; set; }
    [BsonElement("sku")]
    public string Sku { get; set; }
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("price")]
    [BsonRepresentation(BsonType.Decimal128, AllowTruncation=true)]
    public decimal Price { get; set; }
    [BsonElement("attribute")]
    public Attribute attribute { get; set; } 

}