using Domain.Models;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        Product AddProduct(Product entity);
        void DeleteProductById(Guid Id);
        Product GetProductById(Guid Id);
        Product UpdateProduct(Product entity);
        List<Product> GetAllProducts();
    }
}
