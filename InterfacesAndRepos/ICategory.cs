using Serene.Models;
using Serene.Models;

namespace Serene.InterfacesAndRepos
{
    public interface ICategory
    {
        public Task<Category> Create(Category category);
        public Task<Category> Update(Category category);
        public Task<Category> Delete(int categoryId);
        public Task<Category> Get(int categoryId);
        public Task<List<Category>> GetAll();
    }
}
