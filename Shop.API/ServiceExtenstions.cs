using Application.Interfaces;
using Infrastructure.Repositories;

namespace Shop.API
{
    public static class ServiceExtensions
    {
        public static void AddApplication(this IServiceCollection service)
        {
            service.AddTransient<IProductsRepository, ProductsRepository>();
            service.AddTransient<IUsersRepository, UsersRepository>();
            service.AddTransient<ICategoriesRepository, CategoriesRepository>();
            service.AddTransient<ICategoriesPropertiesRepository, CategoriesPropertiesRepository>();
            service.AddTransient<IPropertyValuesRepository, PropertyValuesRepository>();
            service.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}