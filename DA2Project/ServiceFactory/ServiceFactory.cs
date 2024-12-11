using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Logic.Services;
using DataAccess.Repositories;
using DataAccess.Managers;
using Dtos.Interfaces;
using DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;


namespace ServiceFactory
{
    public static class ServiceFactory
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<DbContext, ContextDb>(o => o.UseSqlServer(connectionString));
            serviceCollection.AddScoped<IContextDb, ContextDb>();
            serviceCollection.AddScoped<IMemoryContext, MemoryContext>();
            serviceCollection.AddScoped<IReflectionManager, ReflectionManager>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<ICartRepository, CartRepository>();
            serviceCollection.AddScoped<IOrderRepository, OrderRepository>();
            serviceCollection.AddScoped<IProductRepository, ProductRepository>();
            serviceCollection.AddScoped<IPromoRepository, PromoRepository>();
            serviceCollection.AddScoped<IPurchaseService, PurchaseService>();
            serviceCollection.AddScoped<IAuthManager, AuthManager>();
            serviceCollection.AddScoped<IAuthService, AuthService>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IOrderService, OrderService>();
            serviceCollection.AddScoped<IProductService, ProductService>();
            serviceCollection.AddScoped<ICartService, CartService>();
            serviceCollection.AddScoped<IPromoService, PromoService>();
            return serviceCollection;
        }
    }
}