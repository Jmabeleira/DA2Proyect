using DataAccess.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Dtos.Interfaces;
using Dtos.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly IContextDb _dbContext;

        public ProductRepository(IContextDb productContext)
        {
            _dbContext = productContext;
        }
        public Product AddProduct(Product entity)
        {
            try
            {
                if (!Exist(entity.Id))
                {
                    var product = _dbContext.Products.Add(ProductHelper.FromProductToProductDto(entity));
                    _dbContext.SaveChanges();
                    return entity;
                }
                else
                {
                    throw new InstanceNotUniqueException("Product already exists");
                }
            }
            catch (InstanceNotUniqueException inuEx)
            {
                throw inuEx;
            }
            catch (Exception ex)
            {
                throw new UnhandledDataAccessException(ex);
            }
        }

        public void DeleteProductById(Guid Id)
        {
            try
            {
                if (Exist(Id))
                {
                    var product = _dbContext.Products.FirstOrDefault(x => x.Id == Id);
                    _dbContext.Products.Remove(product);
                    _dbContext.SaveChanges();
                }
                else
                {
                    throw new NoInstanceException("Product not found");
                }
            }
            catch (NoInstanceException niEx)
            {
                throw niEx;
            }
            catch (Exception ex)
            {
                throw new UnhandledDataAccessException(ex);
            }
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                var products = _dbContext.Products.Include(x => x.BrandDto).Include(x => x.ColorDto).Include(x => x.CategoryDto);
                return products.Select(x => ProductHelper.FromProductDtoToProduct(x)).ToList();
            }
            catch (NoInstanceException niEx)
            {
                throw niEx;
            }
            catch (Exception ex)
            {
                throw new UnhandledDataAccessException(ex);
            }
        }

        public Product GetProductById(Guid Id)
        {
            try
            {
                if (Exist(Id))
                {
                    var prodDto = GetAllProducts().FirstOrDefault(x => x.Id == Id);
                    return prodDto;
                }
                else
                {
                    throw new NoInstanceException("Product not found");
                }
            }
            catch (NoInstanceException niEx)
            {
                throw niEx;
            }
            catch (Exception ex)
            {
                throw new UnhandledDataAccessException(ex);
            }
        }

        public Product UpdateProduct(Product entity)
        {
            try
            {
                if (Exist(entity.Id))
                {
                    var existingProduct = _dbContext.Products.Include(x => x.BrandDto).Include(x => x.ColorDto).Include(x => x.CategoryDto).FirstOrDefault(p => p.Id == entity.Id);

                    var productDto = ProductHelper.FromProductToProductDto(entity);

                    if (existingProduct?.BrandDto != null)
                    {
                        existingProduct.BrandDto.Name = productDto.BrandDto.Name;
                    }
                    if (existingProduct?.CategoryDto != null)
                    {
                        existingProduct.CategoryDto.Name = productDto.CategoryDto.Name;
                    }
                    if (existingProduct?.ColorDto != null)
                    {
                        existingProduct.ColorDto.Colors = productDto.ColorDto.Colors;
                    }

                    existingProduct.Name = productDto.Name;
                    existingProduct.Price = productDto.Price;
                    existingProduct.Description = productDto.Description;
                    existingProduct.Stock = productDto.Stock;
                    existingProduct.IsPromotional = productDto.IsPromotional;

                    _dbContext.SaveChanges();

                    return GetProductById(productDto.Id);
                }
                else
                {
                    throw new NoInstanceException("Product not found");

                }
            }
            catch (NoInstanceException niEx)
            {
                throw niEx;
            }
            catch (Exception ex)
            {
                throw new UnhandledDataAccessException(ex);
            }
        }

        private bool Exist(Guid id)
        {
            return GetAllProducts().Any(x => x.Id == id);
        }
    }
}
