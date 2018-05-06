using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product> GetProducts(string name);
    Task<Product> GetProductsById(int id);
    Task<Product> GetProductByMaxPrice();
    Task<Product> GetProductByMinPrice();
    Task<IEnumerable<Product>> GetProductByFantastic(bool value, int type, string name);
    Task<Product> GetProductByMaxRating();
    Task<Product> GetProductByMinRating();
    Task Create(Product pProduct);
    Task<bool> Update(Product pProduct);
    Task<bool> Delete(int id);
}