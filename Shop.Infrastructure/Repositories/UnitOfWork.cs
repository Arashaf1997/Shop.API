using Application.Interfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        //public UnitOfWork(ICategoriesRepository categories, ITagsRepository tags, IOrdersRepository orders, IUsersRepository users, ICommentsRepository comments, IBonusesRepository bonuses, IProductsRepository products)
        //{
        //    Categories = categories;
        //    Tags = tags;
        //    Orders = orders;
        //    Users = users;
        //    Comments = comments;
        //    Bonuses = bonuses;
        //    Products = products;
        //}
        public UnitOfWork(IProductsRepository products,
            IUsersRepository users,
            ICategoriesRepository categories, 
            ICategoriesPropertiesRepository categoriesProperties,
            IPropertyValuesRepository propertyValues,
            IDiscountsRepository discounts,
            IBrandsRepository brands,
            IFileContentsRepository fileContents,
            ICommentsRepository comments)
        {
            Products = products;
            Users = users;
            Categories = categories;
            CategoriesProperties = categoriesProperties;
            PropertyValues = propertyValues;
            Discounts = discounts;
            Brands = brands;
            FileContents = fileContents;
            Comments = comments;
        }


        public ICategoriesRepository Categories { get; }
        public ITagsRepository Tags { get; }
        public IOrdersRepository Orders { get; }
        public IUsersRepository Users { get; }
        public ICommentsRepository Comments { get; }
        public IBonusesRepository Bonuses { get; }
        public IProductsRepository Products { get; }
        public ICategoriesPropertiesRepository CategoriesProperties { get; }
        public IColorsRepository Colors => throw new NotImplementedException();
        public IPropertyValuesRepository PropertyValues { get; }
        public IDiscountsRepository Discounts { get; }
        public IFileContentsRepository FileContents { get; }
        public IBrandsRepository Brands { get; }

    }
}
