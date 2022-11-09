using Application.Interfaces;
using Infrastructure.Repositories;

namespace Shop.API
{
    public static class ServiceExtensions
    {
        public static void AddApplication(this IServiceCollection service)
        {
            service.AddTransient<IUnitOfWork, UnitOfWork>();
            service.AddTransient<IProductsRepository, ProductsRepository>();
            service.AddTransient<IUsersRepository, UsersRepository>();
        }
    }
}