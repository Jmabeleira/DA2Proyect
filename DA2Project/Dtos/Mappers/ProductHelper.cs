using Domain.Models;
using Dtos.Models;

namespace Dtos.Mappers
{
    public static class ProductHelper
    {
        public static Product FromProductDtoToProduct(ProductDto pDto)
        {
            return new Product
            {
                Id = pDto.Id,
                Name = pDto.Name,
                Price = pDto.Price,
                Description = pDto.Description,
                Brand = pDto.BrandDto.Name,
                Colors = pDto.ColorDto.Colors.ToList(),
                Category = pDto.CategoryDto.Name,
                Stock = pDto.Stock,
                IsPromotional = pDto.IsPromotional
            };
        }

        public static ProductDto FromProductToProductDto(Product p)
        {
            return new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                BrandDto = new BrandDto { Name = p.Brand },
                ColorDto = new ColorDto { Colors = p.Colors.ToList() },
                CategoryDto = new CategoryDto { Name = p.Category },
                Stock = p.Stock,
                IsPromotional = p.IsPromotional
            };
        }
    }
}
