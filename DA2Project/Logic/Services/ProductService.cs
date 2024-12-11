using Domain.Interfaces;
using Domain.Models;
using Domain.Models.Exceptions;
using Logic.Exceptions;

namespace Logic.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRrepository;

        public ProductService(IProductRepository productRrepository)
        {
            _productRrepository = productRrepository;
        }
        public Product AddProduct(Product product)
        {
            try
            {
                var returnProduct = _productRrepository.AddProduct(product);
                return returnProduct;
            }
            catch (DANotUniqueException ex)
            {
                throw new AlreadyExistsException("Product already exists", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }

        public bool DeleteProductById(Guid Id)
        {
            try
            {
                _productRrepository.DeleteProductById(Id);
                return true;
            }
            catch (DANoInstanceException ex)
            {
                throw new NotFoundException("There is no such Product", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }

        public Product GetProductById(Guid id)
        {
            try
            {
                return _productRrepository.GetProductById(id);
            }
            catch (DANoInstanceException ex)
            {
                throw new NotFoundException("There is no such Product", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }

        public IEnumerable<Product> GetAllProducts(ParametersFilter parametersFilter)
        {
            try
            {
                IEnumerable<Product> products = _productRrepository.GetAllProducts();
                List<Func<Product, bool>> filters = parametersFilter.GetQueryFilters();

                if (filters.Any())
                {
                    products = products.Where(p => filters.All(filter => filter(p))).ToList();
                }

                return products;
            }
            catch (DANoInstanceException ex)
            {
                throw new NoContentException("There are no Products", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }

        public Product UpdateProduct(Product product)
        {
            try
            {
                return _productRrepository.UpdateProduct(product);
            }
            catch (DANoInstanceException ex)
            {
                throw new NotFoundException("There is no such Product", ex);
            }
            catch (Exception ex)
            {
                throw new UnhandledLogicException(ex);
            }
        }
    }
}
