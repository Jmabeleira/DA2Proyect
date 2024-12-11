using Domain.Models;

namespace Domain.Interfaces
{
    public interface IProductService
    {
        Product AddProduct(Product product);
        Product GetProductById(Guid id);
        IEnumerable<Product> GetAllProducts(ParametersFilter parametersFilter);
        Product UpdateProduct(Product product);
        bool DeleteProductById(Guid Id);
    }
}
