using System;

namespace Application.Interfaces
{
    public interface IUnitOfWork 
    {
        IProductsRepository Products { get; }
        ICategoriesRepository Categories { get; }
        ITagsRepository Tags { get; }
        IOrdersRepository Orders { get; }
        IUsersRepository Users { get; }
        ICommentsRepository Comments { get; }
        IBonusesRepository Bonuses { get; }
        ICategoriesPropertiesRepository CategoriesProperties { get; }
        IColorsRepository Colors { get; }
        IPropertyValues PropertyValues { get; }
    }
}
