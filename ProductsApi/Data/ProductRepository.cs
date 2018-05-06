using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

public class ProductRepository : IProductRepository
{
    private readonly IProductContext _context;
    public ProductRepository(IProductContext context)
    {
        _context = context;
    }

    //Get all documents from products collection
    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _context.Products.Find(_ => true).ToListAsync();
    }

    //Get matching product by name from products collection
    public async Task<Product> GetProducts(string name)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(m => m.Name, name);
        return await _context.Products.Find(filter).FirstOrDefaultAsync();
    }

    //Get matching product by id from products collection
    public async Task<Product> GetProductsById(int id)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(m => m.Id, id);
        return await _context.Products.Find(filter).FirstOrDefaultAsync();
    }

    //
    public async Task<Product> GetProductByMaxPrice()
    {
        var sort = Builders<Product>.Sort.Descending(p => p.Price);
        return await _context.Products.Find(x => true).Sort(sort).FirstOrDefaultAsync();
    }

    public async Task<Product> GetProductByMinPrice()
    {
        var sort = Builders<Product>.Sort.Ascending(p => p.Price);
        return await _context.Products.Find(x => true).Sort(sort).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByFantastic(bool value, int type, string name)
    {
        Fantastic pFantastic = new Fantastic() { Value = value, Type = type, Name = name };
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.attribute.fantastic, pFantastic);

         return await _context.Products.Find(filter).ToListAsync();
    }

    public async Task<Product> GetProductByMaxRating()
    {
        var sort = Builders<Product>.Sort.Descending(p => p.attribute.rating.Value);
        return await _context.Products.Find(x => true).Sort(sort).FirstOrDefaultAsync();
    }

    public async Task<Product> GetProductByMinRating()
    {
        var sort = Builders<Product>.Sort.Ascending(p =>  p.attribute.rating.Value);
        return await _context.Products.Find(x => true).Sort(sort).FirstOrDefaultAsync();
    }

    public async Task Create(Product pProduct)
    {
        await _context.Products.InsertOneAsync(pProduct);
    }
    public async Task<bool> Update(Product pProduct)
    {
        ReplaceOneResult updateResult = await _context.Products.ReplaceOneAsync(filter: g => g.Id == pProduct.Id, replacement: pProduct);
        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }
    public async Task<bool> Delete(int id)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(m => m.Id, id);
        DeleteResult deleteResult = await _context.Products.DeleteOneAsync(filter);
        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }
}