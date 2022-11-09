
using Dependencies.Models;
using Shop.Application.Dtos.ColorDtos;

namespace Application.Interfaces
{
    public interface IColorsRepository : IGenericRepository<Color>
    {
        public Task<IReadOnlyList<GetColorDto>> GetAllAsync();

    }
}