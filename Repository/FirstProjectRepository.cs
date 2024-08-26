using FirstProject.Local.Data;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace FirstProject.Local.Repository;

public class FirstProjectRepository : IFirstProjectRepository
{
    private ProductContext _context;

    public FirstProjectRepository(ProductContext context)
    {
        _context = context;
    }
    public async Task<Product> AddProductsAsync(Product product)
    {
        _context.Products.Add(product);

        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context
            .Products
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(int categoryId)
    {
        return await _context.Categories.FirstOrDefaultAsync(m => m.Id == categoryId);
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await _context
            .Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(m => m.Id == productId);
    }

    public async Task RemoveProductAsync(int productIdToRemove)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productIdToRemove);

        if (product != null) 
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateProductAsync(Product product)
    {
        try {
            _context.Update(product);

            await _context.SaveChangesAsync();
        } catch (DbUpdateConcurrencyException) {
            if (_context.Products.Any(e => e.Id == product.Id)) 
            {
                throw;
            }
        }
    }
}
