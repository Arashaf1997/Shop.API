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
        public UnitOfWork(IProductsRepository products)
        {
            Products = products;
        }
        public UnitOfWork (IUsersRepository users)
        {
            Users = users;
        }

        public ICategoriesRepository Categories { get; }

        public ITagsRepository Tags { get; }

        public IOrdersRepository Orders { get; }

        public IUsersRepository Users { get; }

        public ICommentsRepository Comments { get; }

        public IBonusesRepository Bonuses { get; }

        public IProductsRepository Products { get; }

        public ICategoriesPropertiesRepository CategoriesProperties => throw new NotImplementedException();

        public IColorsRepository Colors => throw new NotImplementedException();

        public IPropertyValues PropertyValues => throw new NotImplementedException();
    }
}
