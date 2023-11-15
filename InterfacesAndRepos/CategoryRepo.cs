using Microsoft.EntityFrameworkCore;
using Serene.DataAccess.Data;
using Serene.Models;

namespace Serene.InterfacesAndRepos
{
    public class CategoryRepo : ICategory
    {
        private readonly ApplicationDbContext context;

        public CategoryRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Category> Create(Category model)
        {
            await context.Categories.AddAsync(model);
            await context.SaveChangesAsync();
            return model;
        }

        public async Task<Category> Delete(int categoryId)
        {
            var category = await context.Categories.FindAsync(categoryId);
            if(category != null)
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return category;
            }
#pragma warning disable CS8603 // Possible null reference return.
            return null;
#pragma warning restore CS8603 // Possible null reference return.
        }

#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        public async Task<Category?> Get(int categoryId)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        {
            var data = await context.Categories.FirstOrDefaultAsync(u=>u.CategoryId == categoryId);
            return data == null ? null : data;
        }

        public async Task<List<Category>> GetAll()
        {
            var data = await context.Categories.ToListAsync();
            return data;
        }

        public async Task<Category> Update(Category category)
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync();
            return category;
        }
    }
}
