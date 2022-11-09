using Dependencies.Models;
using Shop.Application.Dtos.PropertyValueDtos;

namespace Application.Interfaces
{
    public interface IPropertyValues : IGenericRepository<PropertyValue>
    {
        Task<List<GetPropertyValueDto>> GetAllAsync();
    }
}