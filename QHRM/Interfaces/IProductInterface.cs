using QHRM.Models;

namespace QHRM.Interfaces
{
    public interface IProductInterface
    {
        Task<IEnumerable<Products>> GetAllProducts();
        Task<Products> GetProductById(int id);
        Task<string> AddProduct(Products product);
        Task<bool> UpdateProduct(int id,Products product);
        Task<bool> DeleteProduct(int id);
    }
}
