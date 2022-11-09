using Application.Interfaces;
using Dependencies.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class BonusesRepository : IBonusesRepository
    {
        private readonly IConfiguration _configuration;
        public BonusesRepository(IConfiguration configuration)
        {
            // Injecting Iconfiguration to the contructor of the product repository
            _configuration = configuration;
        }

        public Task<int> AddAsync(Bonus entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Bonus>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Bonus> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Bonus entity)
        {
            throw new NotImplementedException();
        }


        //public bool AddBonus(int productId, int bonus)
        //{
        //    if (!Context.Bonuses.Any(b => b.UserId.Equals(2) && b.ProductId.Equals(productId)))
        //    {
        //        Context.Bonuses.Add(new Bonus { ProductId = productId, Stars = bonus, UserId = 2, InsertTime = DateTime.Now });
        //        Context.SaveChanges();
        //        return true;
        //    }
        //    else
        //        return false;
        //}
    }
}
