using FirstProject.Local.Models;

namespace FirstProject.Local.Logic;

public interface IProductLogic
{
    Task<List<ProductModel>> GetAllProducts();
    Task<ProductModel?> GetProductById(int id);
    Task AddNewProduct(ProductModel productToAdd);
    Task RemoveProduct(int id);
    Task UpdateProduct(ProductModel productToUpdate);
    Task<ProductModel> InitialiseProductModel();
    Task GetAvailableCategories(ProductModel productModel);
}
