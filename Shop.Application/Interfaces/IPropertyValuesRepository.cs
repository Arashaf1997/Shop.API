using Dependencies.Models;
using Shop.Application.Dtos.PropertyValueDtos;

namespace Application.Interfaces
{
    public interface IPropertyValuesRepository : IGenericRepository<PropertyValue>
    {
        Task<List<GetPropertyValueDto>> GetAllAsync();
        Task<int> Add(AddPropertyValueDto addPropertyValueDto);
        Task<int> Update(UpdatePropertyValueDto updatePropertyValueDto);
    }
}