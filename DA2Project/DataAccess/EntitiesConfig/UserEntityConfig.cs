using Domain.Models;
using Dtos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.EntitiesConfig
{
    public static class UserEntityConfig
    {
        public static void SetUserEntityConfig(EntityTypeBuilder<UserDto> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(u => u.UserRole)
                    .HasColumnName("UserRole")
                    .HasConversion(
                        v => string.Join(",", v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Enum.Parse<UserRole>).ToList()
                    );
            entityBuilder.HasOne(u => u.Address) 
                .WithOne(a => a.User)
                .HasForeignKey<AddressDto>(a => a.UserId);
        }
    }
}