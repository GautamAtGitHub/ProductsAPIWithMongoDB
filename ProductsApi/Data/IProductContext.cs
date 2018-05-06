using MongoDB.Driver;

public interface IProductContext
{
    IMongoCollection<Product> Products { get; }
}