using Application.Interfaces;
using Dependencies.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class TagsRepository : ITagsRepository
    {
        private readonly IConfiguration _configuration;

        public TagsRepository(IConfiguration configuration)
        {
            // Injecting Iconfiguration to the contructor of the product repository
            _configuration = configuration;
        }

        public Task<int> AddAsync(Tag entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Tag>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Tag> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Tag entity)
        {
            throw new NotImplementedException();
        }

        //public IQueryable<Tag> GetFilteredTags(string searchText)
        //{
        //    return Context.Tags.Where(t => t.Title.Contains(searchText));
        //}

        //public List<Tag> GetListOfTags(List<string> tags)
        //{
        //    List<Tag> list = new List<Tag>();
        //    foreach (var tagTitle in tags)
        //    {
        //        list.Add(Context.Tags.Where(t => t.Title == tagTitle).FirstOrDefault());
        //    }
        //    return list;
        //}
    }
}
