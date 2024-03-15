using Blog.API.Data;
using Blog.API.Models.Domain;
using Blog.API.Repository;
using Microsoft.EntityFrameworkCore;

namespace Blog.API.Repositorise
{
    public class SQLCategoryRepository : ICategoryRepository
    {
        private readonly BlogDBContext dBContext;

        public SQLCategoryRepository(BlogDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await dBContext.Categories.AddAsync(category);
            await dBContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCategory = await dBContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);

            if (existingCategory == null)
            {
                return null;
            }

            dBContext.Categories.Remove(existingCategory);
            await dBContext.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categories = await dBContext.Categories.ToListAsync();
            return categories;
        }

        public  async Task<Category?> GetByIdAsync(Guid id)
        {
            return await dBContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
        }

        public async Task<Category?> UpdateAsync(Guid id, Category category)
        {
            var existingCategory = await dBContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);

            if (existingCategory == null)
            {
                return null;
            }

            existingCategory.Name = category.Name;

            await dBContext.SaveChangesAsync();

            return existingCategory;
        }
    }
}
