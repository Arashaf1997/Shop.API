using Dependencies.Models;
using Shop.Application;
using Shop.Application.Dtos.FileContentDtos;

namespace Application.Interfaces
{
    public interface IFileContentsRepository : IGenericRepository<FileContent>
    {
        public Task<IReadOnlyList<GetFileContentDto>> GetAllAsync();
        public Task<int> Add(AddFileContentDto addFileContentDto);
        public Task<int> Update(UpdateFileContentDto updateFileContentDto);
        Stream GetById(int id, ImageSizeType imageSize);
    }
}