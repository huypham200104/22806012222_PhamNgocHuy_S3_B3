using _22806012222_PhamNgocHuy_S3_B3.Models;

namespace _22806012222_PhamNgocHuy_S3_B3.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
    }
}
