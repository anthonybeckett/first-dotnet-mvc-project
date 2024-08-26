using FirstProject.Local.Models;
using FirstProject.Local.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FirstProject.Local.Logic;

public class ProductLogic : IProductLogic
{
    private IFirstProjectRepository _repo;
    private readonly IValidator<ProductModel> _validator;

    public ProductLogic(IFirstProjectRepository repo, IValidator<ProductModel> validator)
    {
        _repo = repo;
        _validator = validator;
    }
    public async Task AddNewProduct(ProductModel productToAdd)
    {
        await _validator.ValidateAndThrowAsync(productToAdd);
        var productToSave = productToAdd.ToProduct();
        await _repo.AddProductsAsync(productToSave);
    }

    public async Task<List<ProductModel>> GetAllProducts()
    {
        var products = await _repo.GetAllProductsAsync();

        return products.Select(ProductModel.FromProduct).ToList();
    }

    public async Task<ProductModel?> GetProductById(int id)
    {
        var product = await _repo.GetProductByIdAsync(id);
        return product == null ? null : ProductModel.FromProduct(product);
    }

    public async Task<ProductModel> InitialiseProductModel()
    {
        return new ProductModel { AvailableCategories = await GetAvailableCategoriesFromDb() };
    }

    private async Task<List<SelectListItem>> GetAvailableCategoriesFromDb()
    {
        var categories = await _repo.GetAllCategoriesAsync();
        var returnList = new List<SelectListItem> { new("None", "") };
        var availableCategoryList = categories.Select(categories => new SelectListItem(categories.Name, categories.Id.ToString())).ToList();

        returnList.AddRange(availableCategoryList);

        return returnList;
    }

    public async Task RemoveProduct(int id)
    {
        await _repo.RemoveProductAsync(id);
    }

    public async Task UpdateProduct(ProductModel productToUpdate)
    {
        var productToSave = productToUpdate.ToProduct();

        await _repo.UpdateProductAsync(productToSave);
    }

    public async Task GetAvailableCategories(ProductModel productModel)
    {
        productModel.AvailableCategories = await GetAvailableCategoriesFromDb();
    }
}
