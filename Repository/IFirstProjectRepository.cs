using FirstProject.Local.Data;

namespace FirstProject.Local.Repository;

public interface IFirstProjectRepository
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int productId);
    Task<Product> AddProductsAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task RemoveProductAsync(int productIdToRemove);
    Task<List<Category>> GetAllCategoriesAsync();

    Task<Category?> GetCategoryByIdAsync(int categoryId);
}
