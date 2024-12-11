using Dtos.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesConfig
{
    public static class AddressEntityConfig
    {
        public static void SetAddressEntityConfig(EntityTypeBuilder<AddressDto> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
        }
    }
}
