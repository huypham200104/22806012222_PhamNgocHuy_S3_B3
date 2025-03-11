using _22806012222_PhamNgocHuy_S3_B3.Models;

namespace _22806012222_PhamNgocHuy_S3_B3.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}
