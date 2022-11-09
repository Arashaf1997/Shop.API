using Dependencies.Models;
using System.Collections.Generic;
using System.Linq;

namespace Application.Interfaces
{
    public interface ITagsRepository : IGenericRepository<Tag>
    {
        //IQueryable<Tag> GetFilteredTags(string searchText);
        //void CheckTagsToCreate(string tagTitle);
        //List<Tag> GetListOfTags(List<string> tags);
    }
}
