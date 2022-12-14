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
            service.AddTransient<IDiscountsRepository, DiscountsRepository>();
            service.AddTransient<IBrandsRepository, BrandsRepository>();
            service.AddTransient<IFileContentsRepository, FileContentsRepository>();
            service.AddTransient<ICommentsRepository, CommentsRepository>();
            service.AddTransient<IBlogRepository, BlogRepository>();
            service.AddTransient<IBlogCategoryRepository, BlogCategoryRepository>();
            service.AddTransient<ICartRepository, CartRepository>();
            service.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}