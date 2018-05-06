using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class ProductContext : IProductContext
{
    private readonly IMongoDatabase _db;

    public ProductContext(IOptions<Settings> options)
    {
        var _client = new MongoClient(options.Value.ConnectionString);
        _db = _client.GetDatabase(options.Value.Database);
    }

    public IMongoCollection<Product> Products => _db.GetCollection<Product>("Products");
}

