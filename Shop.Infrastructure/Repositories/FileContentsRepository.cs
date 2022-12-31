using Application.Interfaces;
using Dapper;
using Dependencies.Models;
using Microsoft.Extensions.Configuration;
using Shop.Application;
using Shop.Application.Dtos.CategoryDtos;
using Shop.Application.Dtos.FileContentDtos;
using Shop.Application.Dtos.ProductDtos;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class FileContentsRepository : IFileContentsRepository
    {
        private readonly IConfiguration _configuration;
        public FileContentsRepository(IConfiguration configuration)
        {
            // Injecting Iconfiguration to the contructor of the product repository
            _configuration = configuration;
        }

        public async Task<int> AddAsync(FileContent entity)
        {
            entity.InsertTime = DateTime.Now;
            entity.EditTime = null;

            // Basic SQL statement to insert a product into the products table
            var sql = "INSERT INTO dbo.FileContents (FileContentName,InsertTime,EditTime) VALUES (@FileContentName,@InsertTime,@EditTime)";

            // Sing the Dapper Connection string we open a connection to the database
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();

                // Pass the product object and the SQL statement into the Execute function (async)
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM dbo.ProductFileContents WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public Stream GetById(int id, ImageSizeType imageSize)
        {
            try
            {
                GetFileContentDto result = new GetFileContentDto();
                var sql = "SELECT * FROM dbo.FileContent WHERE Id = @Id";
                using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
                var fileContent = connection.QuerySingleOrDefault<FileContent>(sql, new { Id = id });

                if (string.IsNullOrWhiteSpace(fileContent.FilePath))
                    return null;
                if (imageSize == ImageSizeType.Small)
                {
                    return System.IO.File.OpenRead(fileContent.FilePath + "\\" + fileContent.GuidName + "_2" + "." + fileContent.FileExtension); ;
                }
                if (imageSize == ImageSizeType.Medium)
                {
                    return System.IO.File.OpenRead(fileContent.FilePath + "\\" + fileContent.GuidName + "_1" + "." + fileContent.FileExtension);
                }
                else
                    return System.IO.File.OpenRead(fileContent.FilePath + "\\" + fileContent.GuidName + "." + fileContent.FileExtension);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> UpdateAsync(FileContent entity)
        {
            var sql = "UPDATE dbo.ProductFileContents SET FileContentName = @FileContentName, EditTime = GETDATE() WHERE Id = @Id";
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { });
                return result;
            }
        }

        public async Task<IReadOnlyList<GetFileContentDto>> GetAllAsync()
        {
            var sql = "SELECT Id,FileContentName FROM dbo.FileContents";

            // Sing the Dapper Connection string we open a connection to the database
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection")))
            {
                connection.Open();

                // Pass the product object and the SQL statement into the Execute function (async)
                var result = await connection.QueryAsync<GetFileContentDto>(sql);
                return result.ToList();
            }
        }

        public async Task<int> Add(AddFileContentDto addFileContentDto)
        {
            string filePath = Directory.GetCurrentDirectory() + "\\StaticFiles";
            Guid guidName = Guid.NewGuid();

            var sql = @$"INSERT INTO dbo.FileContent(GuidName,RealFileName,Length,MimeType,FileExtension,FilePath,InsertTime,EditTime)
                                OUTPUT Inserted.Id
                                VALUES
                                (@GuidName,@RealFileName,@Length,@MimeType,@FileExtension,@FilePath,GETDATE(),NULL)";
            var fileExtension = addFileContentDto.form.FileName.Split('.')[addFileContentDto.form.FileName.Split('.').Length - 1];
            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.QueryFirstOrDefaultAsync<int>(sql, new
            {
                RealFileName = addFileContentDto.form.FileName,
                GuidName = guidName,
                Length = addFileContentDto.form.Length,
                MimeType = addFileContentDto.form.ContentType,
                FileExtension = fileExtension,
                FilePath = filePath
            });
            if (result > 0)
            {
                try
                {
                    if (!System.IO.Directory.Exists(filePath))
                        System.IO.Directory.CreateDirectory(filePath);
                    string fullFileName = filePath + "\\" + guidName.ToString() + "." + fileExtension;
                    using (var stream = new FileStream(fullFileName, FileMode.Create))
                    {
                        addFileContentDto.form.CopyTo(stream);
                    }
                    List<string> imageMimeTypes = new List<string>() { "image/png", "image/jpeg", "image/bmp", "image/tiff", "image/gif" };//jpg jpeg jpe png bmp gif
                    /// Resize Images
                    if (imageMimeTypes.Contains(addFileContentDto.form.ContentType))
                    {
                        ResizeImage(ImageSizeType.Medium, fullFileName
                            , filePath + "\\" + guidName.ToString() + "_1" + "." + fileExtension, addFileContentDto.form.ContentType);

                        ResizeImage(ImageSizeType.Small, fullFileName
                           , filePath + "\\" + guidName.ToString() + "_2" + "." + fileExtension, addFileContentDto.form.ContentType);
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    return 0;
                }

            }
            else
            {
                return 0;
            }
        }

        public async Task<int> Update(UpdateFileContentDto updateFileContentDto)
        {
            var sql = "UPDATE dbo.ProductFileContents SET FileContentPercent = @FileContentPercent, Price = @Price, Description = @Description , StartDate = @StartDate, EndDate = @EndDate, EditTime = GETDATE() WHERE Id = @Id";
            using var connection = new SqlConnection(_configuration.GetConnectionString("DapperConnection"));
            var result = await connection.ExecuteAsync(sql, updateFileContentDto);
            return result;
        }

        public static void ResizeImage(ImageSizeType SizeType, string InputPath, string OutputName, string mimeType)
        {
            int size = 150;
            int quality = 75;
            switch (SizeType)
            {
                case ImageSizeType.Medium:
                    size = 400;
                    break;
                case ImageSizeType.Small:
                    size = 150;
                    break;
            }

            using (var image = new Bitmap(System.Drawing.Image.FromFile(InputPath)))
            {
                int width, height;
                if (image.Width > image.Height)
                {
                    width = size;
                    height = Convert.ToInt32(image.Height * size / (double)image.Width);
                }
                else
                {
                    width = Convert.ToInt32(image.Width * size / (double)image.Height);
                    height = size;
                }
                if (width > image.Width || height > image.Height)
                {
                    width = image.Width;
                    height = image.Height;
                }
                var resized = new Bitmap(width, height);
                using (var graphics = Graphics.FromImage(resized))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.DrawImage(image, 0, 0, width, height);
                    using (var output = File.Open(OutputName, FileMode.Create))
                    {
                        var qualityParamId = System.Drawing.Imaging.Encoder.Quality;
                        var encoderParameters = new EncoderParameters(1);
                        encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);
                        ImageCodecInfo codec1 = ImageCodecInfo.GetImageDecoders()
                            .FirstOrDefault(codec => codec.MimeType == mimeType);
                        resized.Save(output, codec1, encoderParameters);
                    }
                }
            }
        }

        public Task<FileContent> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
