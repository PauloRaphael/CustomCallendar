using DataRepository.Data;
using DataRepository.Entities;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
namespace DataRepository.Services
{
    public class CategoryService
    {
        private readonly CustomCalendarDBContext _context;

        public CategoryService(CustomCalendarDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task<Category?> GetCategoryAsync(int? id)
        {
            return await _context.Category.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task InsertAsync(Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
        }
        public bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
    }
}
