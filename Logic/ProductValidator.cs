using FirstProject.Local.Models;
using FirstProject.Local.Repository;
using FluentValidation;

namespace FirstProject.Local.Logic;

public class ProductValidator : AbstractValidator<ProductModel>
{
    public ProductValidator(IFirstProjectRepository repo)
    {
        RuleFor(p => p).MustAsync(async (productModel, cancellation) => 
        {
            if (productModel.CategoryId == 0) return true;

            var category = await repo.GetCategoryByIdAsync(productModel.CategoryId);

            if (category?.Name != "Category 2") return true;

            return productModel.Price <= 200.00M;
        }).WithMessage("Price canot be more than 200.00 for Category 2");
    }

    
}
